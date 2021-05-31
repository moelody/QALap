using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class SearchFm
{
    private Timer t;
    private int CurrentIndex = 1;
    private int MoreDelta = 0;
    private bool IsClickEnter = false;
    private string laststr = "laststr";
    private static readonly Action<ToolStrip, int> ScrollInternal
        = (Action<ToolStrip, int>)Delegate.CreateDelegate(typeof(Action<ToolStrip, int>),
            typeof(ToolStrip).GetMethod("ScrollInternal",
                System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Instance));
    private void handleDelta(object sender, MouseEventArgs e)
    {
        handleSelected(e.Delta < 0 ? true : false);
    }
    private void handleSelected(bool dw)
    {
        if (Popup.Items.Count == 0)
            return;
        var firstItem = Popup.Items[0];
        var lastItem = Popup.Items[Popup.Items.Count - 1];
        if (dw && lastItem.Bounds.Bottom <= Popup.Height || !dw && firstItem.Bounds.Top >= 0)
            return;

        int delta = 50;
        if (firstItem.Bounds.Top == -25 || (lastItem.Bounds.Bottom - Height == 25))
            delta = 25;
        delta += MoreDelta;
        MoreDelta = 0;

        ScrollInternal(Popup, dw ? delta : -delta);
    }
    private bool SearchUtils(string name, string text)
    {
        return PinYinHelper.CapsConvert(name).IndexOf(text, StringComparison.CurrentCultureIgnoreCase) != -1
            || PinYinHelper.Convert(name).IndexOf(text, StringComparison.CurrentCultureIgnoreCase) != -1
            || name.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) != -1;
    }
    private void Popup_Update(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(searchText.Text))
        {
            List<ToolStripMenuItem> toolStripScriptsItems = new List<ToolStripMenuItem>();
            List<ToolStripMenuItem> toolStripExpressionsItems = new List<ToolStripMenuItem>();
            List<ToolStripMenuItem> toolStripAepProjectsItems = new List<ToolStripMenuItem>();
            List<ToolStripMenuItem> toolStripPresetsItems = new List<ToolStripMenuItem>();

            //IEnumerable<Everything.Result> results = Everything.Search(String.Format("file:path:{1} \"{0}\"", ScriptsPath.ToString(), searchText.Text));
            //foreach (Everything.Result result in results)
            //{
            //    toolStripScriptsItems.Add(new CustomDropItem(result.Filename, Color.FromArgb(233, 66, 66), Image.FromFile(@"C:\Users\moelody\Downloads\Compressed\shinobux3.png")));
            //}
            string text = searchText.Text;

            foreach (FileInfo fileInfo in ScriptsPath.GetFiles("*.js*", SearchOption.AllDirectories))
            {
                if (SearchUtils(fileInfo.Name, text))
                {
                    toolStripScriptsItems.Add(new CustomDropItem(fileInfo.Name, Color.FromArgb(233, 66, 66), fileInfo.FullName));
                }
            }

            foreach (FileInfo fileInfo in ExpressionsPath.GetFiles("*.exp", SearchOption.AllDirectories))
            {
                if (SearchUtils(fileInfo.Name, text))
                {
                    toolStripExpressionsItems.Add(new CustomDropItem(fileInfo.Name, Color.FromArgb(66, 66, 233), fileInfo.FullName));
                }
            }

            foreach (FileInfo fileInfo in AepProjectPath.GetFiles("*.aep", SearchOption.AllDirectories))
            {
                if (SearchUtils(fileInfo.Name, text))
                {
                    toolStripAepProjectsItems.Add(new CustomDropItem(fileInfo.Name, Color.FromArgb(233, 66, 233), fileInfo.FullName));
                }
            }

            int count = toolStripScriptsItems.Count + toolStripExpressionsItems.Count + toolStripAepProjectsItems.Count;
            Popup.MinimumSize = new Size(540, 75 + 50 * (count > 5 ? 5 : count));
            Popup.Items.Clear();
            Popup.Items.Add(new CustomMenuItem("Scripts", Color.FromArgb(233, 30, 30)));
            Popup.Items.AddRange(toolStripScriptsItems.ToArray());
            Popup.Items.Add(new CustomMenuItem("Expressions", Color.FromArgb(30, 30, 233)));
            Popup.Items.AddRange(toolStripExpressionsItems.ToArray());
            Popup.Items.Add(new CustomMenuItem("Projects", Color.FromArgb(233, 30, 233)));
            Popup.Items.AddRange(toolStripAepProjectsItems.ToArray());

            CurrentIndex = 1;
            while (Popup.Items[CurrentIndex].Size.Height == 25 && CurrentIndex < Popup.Items.Count - 1)
            {
                CurrentIndex = Math.Min(++CurrentIndex, Popup.Items.Count - 1);
            }
            Popup.Items[CurrentIndex].Select();
            Popup.Show(new Point(1, 50));
        } 
        else
        {
            Popup.Items.Clear();
            Popup.Close();
        }
        //t.Stop();
    }
    private void popup_MouseHover(object sender, EventArgs e)
    {
    }
    private void popup_ItemClicked(Object sender, ToolStripItemClickedEventArgs e)
    {
        SetVarValue(e.ClickedItem.ToolTipText);
        fm.Hide();
    }
    private void StartEventAfterXSeconds(int seconds = 100)
    {
        if (t != null)
        {
            t.Stop();
        }
        else
        {
            t = new Timer();
            t.Tick += Popup_Update;
        }

        t.Interval = 1 * seconds;
        t.Start();
    }
    private void searchText_DoubleClick(object sender, EventArgs e)
    {
        searchText.SelectAll();
    }
    private void searchText_TextChanged(object sender, EventArgs e)
    {
        Popup_Update(sender, e);
    }
    private void searchText_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.A)
        {
            searchText.SelectAll();
        }
        // Up Arrow
        if (e.KeyCode == Keys.Up)
        {
            e.Handled = true;
            MoreDelta = 0;
            CurrentIndex = Math.Max(--CurrentIndex, 1);
            if (CurrentIndex != 0 && Popup.Items.Count != 3)
            {
                while (Popup.Items[CurrentIndex].Size.Height == 25 && CurrentIndex != 1)
                {
                    CurrentIndex = Math.Max(--CurrentIndex, 1);
                    MoreDelta += 25;
                }
                while (Popup.Items[CurrentIndex].Size.Height == 25 && CurrentIndex != Popup.Items.Count - 1)
                {
                    CurrentIndex = Math.Min(++CurrentIndex, Popup.Items.Count - 1);
                    MoreDelta -= 25;
                }
                Popup.Items[CurrentIndex].Select();
            }
            if (Popup.Items[CurrentIndex].Bounds.Top < 0)
            {
                handleSelected(false);
            }
        }
        // Dw Arrow
        if (e.KeyCode == Keys.Down)
        {
            e.Handled = true;
            MoreDelta = 0;
            CurrentIndex = Math.Min(++CurrentIndex, Popup.Items.Count - 1);
            if (CurrentIndex != Popup.Items.Count && Popup.Items.Count != 3)
            {
                while (Popup.Items[CurrentIndex].Size.Height == 25 && CurrentIndex != Popup.Items.Count - 1)
                {
                    CurrentIndex = Math.Min(++CurrentIndex, Popup.Items.Count - 1);
                    MoreDelta += 25;
                }
                while (Popup.Items[CurrentIndex].Size.Height == 25 && CurrentIndex != 1)
                {
                    CurrentIndex = Math.Max(--CurrentIndex, 1);
                    MoreDelta -= 25;
                }
                Popup.Items[CurrentIndex].Select();
            }
            if (Popup.Items[CurrentIndex].Bounds.Bottom > Popup.Height)
            {
                handleSelected(true);
            }
        }
    }
    private void searchText_KeyUp(object sender, KeyEventArgs e)
    {
        if (IsClickEnter)
        {
            SetVarValue(Popup.Items[CurrentIndex].ToolTipText);
            fm.Hide();
        }
        IsClickEnter = false;
    }
    private void searchText_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 13)
        {
            e.Handled = true;
            IsClickEnter = true;
        }
        if (e.KeyChar == 27)
        {
            Result();
            fm.Hide();
        }
    }
    private void SearchFm_Deactivate(object sender, EventArgs e)
    {
        Result();
    }
    private void mainButton_Click(object sender, EventArgs e)
    {
        fm.Hide();
    }
    public void Result()
    {
        DialogResult = DialogResult.OK;
    }
}