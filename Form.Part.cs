using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

public partial class SearchFm
{
    public class PanelBox : Panel
    {
        private readonly Color BorderColor = Color.FromArgb(225, 225, 225);
        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                ClientRectangle,
                BorderColor,
                ButtonBorderStyle.Solid
                );
            base.OnPaint(e);
        }
    }

    public class PopupWindow : ToolStripDropDown
    {
        public PopupWindow()
        {
            //Basic setup...
            this.AutoClose = false;
            this.DropShadowEnabled = false;
            this.AllowItemReorder = true;
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            this.TopLevel = false;
            //ShowImageMargin = true;
            //ShowCheckMargin = false;
            this.RenderMode = ToolStripRenderMode.System;
            this.Renderer = new ReRenderer(new CustomProfessionalColorTable());
        }
        protected class ReRenderer : ToolStripProfessionalRenderer
        {

            public ReRenderer(ProfessionalColorTable table) : base(table)
            {
                RoundedEdges = false;
            }
            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                //base.OnRenderToolStripBorder(e);
            }
            protected override void OnRenderImageMargin(System.Windows.Forms.ToolStripRenderEventArgs e)
            {
                //base.OnRenderImageMargin(e);
            }
            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                base.OnRenderItemText(e);
            }
        }
        protected class CustomProfessionalColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            { get { return Color.FromArgb(100, 100, 100); } }

            public override Color MenuBorder
            { get { return Color.Black; } }

            //fill màu item của menu khi mouse enter
            public override Color MenuItemSelectedGradientBegin
            { get { return Color.FromArgb(64, 64, 66); } }

            public override Color MenuItemSelectedGradientEnd
            { get { return Color.FromArgb(64, 64, 66); } }

            // chọn màu viền menu item khi mouse enter
            public override Color MenuItemBorder
            { get { return Color.FromArgb(51, 51, 52); } }

            // fill màu nút item của menu khi dc nhấn
            public override Color MenuItemPressedGradientBegin
            { get { return Color.FromArgb(27, 27, 28); } }

            public override Color MenuItemPressedGradientEnd
            { get { return Color.FromArgb(27, 27, 28); } }

            // fill màu thanh menu strip
            public override Color MenuStripGradientBegin
            { get { return Color.FromArgb(51, 51, 52); } }

            public override Color MenuStripGradientEnd
            { get { return Color.FromArgb(51, 51, 52); } }
        }
    }

    public class CustomToolStripItem : ToolStripMenuItem
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            string text = Text;
            Text = "";
            base.OnPaint(e);
            if (this.Owner != null)
            {
                foreach (Match m in Regex.Matches(Text = text, @"(?<key>[lx])|(?<word>[^lx]+)", RegexOptions.IgnoreCase))
                {
                    if (m.Groups["key"].Success)
                    {
                        Text = m.Value;
                        base.OnPaint(e);
                    }
                    else if (m.Groups["word"].Success)
                    {
                        Text = m.Value;
                        base.OnPaint(e);
                    }
                }

                //ToolStripItemTextRenderEventArgs rea =
                //    new ToolStripItemTextRenderEventArgs(
                //    e.Graphics,
                //    this,
                //    text,
                //    ContentRectangle,
                //    ForeColor,
                //    Font,
                //    TextAlign);
                //this.Owner.Renderer.DrawItemText(rea);
            }

            //e.Graphics.FillRectangle(new SolidBrush(SystemColors.Control), e.ClipRectangle);

            //var pattern = string.Format("({0}|\n)", Keyword);
            //string[] strings = Regex.Split(Text, pattern, IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
            //int lineHeight = TextRenderer.MeasureText(e.Graphics, "A", Font).Height * 3 / 2;
            //var pt = new Point();
            //Size proposedSize = new Size(int.MaxValue, int.MaxValue);
            //var flags = TextFormatFlags.NoPadding;
            //foreach (var str in strings)
            //{
            //    if (str != "\n")
            //    {
            //        var sz = TextRenderer.MeasureText(e.Graphics, str, Font, proposedSize, flags);
            //        var color = str.Equals(keyWord, IgnoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture) ?
            //                KeywordColor : LinkColor;
            //        TextRenderer.DrawText(e.Graphics, str, Font, pt, color, flags);
            //        pt.X += sz.Width;
            //    }
            //    else
            //    {
            //        pt = new Point(0, pt.Y + lineHeight);
            //    }
            //}
        }
    }

    public class FlatButton : Button
    {
        protected override void OnMouseEnter(EventArgs e)
        {
            this.ForeColor = Color.FromArgb(0, 0, 0);
            this.Cursor = Cursors.Hand;
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            this.ForeColor = Color.White;
            this.Cursor = Cursors.Default;
            base.OnMouseLeave(e);
        }
    }

    public delegate void Mydoubleclick(object sender, MouseEventArgs e);
    public class EditTextBox : TextBox
    {
        const int WM_LBUTTONDBLCLK = 0x0203;
        public event Mydoubleclick EditMousedoubleclick;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDBLCLK)
            {
                MouseEventArgs e = new MouseEventArgs(MouseButtons.Left, 2, MousePosition.X, MousePosition.Y, 0);
                if (EditMousedoubleclick != null)
                    EditMousedoubleclick(this, e);
                return;
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}