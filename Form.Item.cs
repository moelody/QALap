using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

public partial class SearchFm
{
    protected class CustomMenuItem : ToolStripMenuItem
    {
        private Color color;
        public CustomMenuItem(string t, Color c)
        {
            color = c;
            Text = t;
            AutoSize = false;
            Size = new Size(540, 25);
            BackColor = Color.FromArgb(50, 50, 50);
            ForeColor = Color.FromArgb(233, 233, 233);
            Font = new Font("微软雅黑", 12, FontStyle.Regular);
            Padding = Padding.Empty;
            Margin = Padding.Empty;
            TextAlign = ContentAlignment.MiddleLeft;
            ImageAlign = ContentAlignment.MiddleLeft;
            ImageScaling = ToolStripItemImageScaling.None;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(new SolidBrush(color), 14, 7, 10, 10);
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), 36, 1);
        }
    }
    protected class CustomDropItem : ToolStripMenuItem
    {
        private Color HighlightColor;
        private string ImagePath;
        private float nPercent;
        private float nPercentW;
        private float nPercentH;
        public CustomDropItem(string t, Color c, string p)
        {
            HighlightColor = c;
            ToolTipText = p;
            Text = t;
            AutoSize = false;
            Size = new Size(540, 50);
            BackColor = Color.FromArgb(30, 30, 30);
            Font = new Font("微软雅黑", 16, FontStyle.Regular);
            Padding = Padding.Empty;
            Margin = Padding.Empty;
            TextAlign = ContentAlignment.MiddleLeft;
            ImageAlign = ContentAlignment.MiddleLeft;
            ImageScaling = ToolStripItemImageScaling.None;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            if (Selected == true)
            {
                Rectangle rect = new Rectangle(0, 0, (int)e.Graphics.VisibleClipBounds.Width, (int)e.Graphics.VisibleClipBounds.Height);
                //Rectangle rect = new Rectangle(2, 0, (int)e.Graphics.VisibleClipBounds.Width - 4, (int)e.Graphics.VisibleClipBounds.Height - 1);
                using (SolidBrush brush = new SolidBrush(HighlightColor))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
            ImagePath = Regex.Replace(ToolTipText, @"(.+[^\/]+)(\.[^\/]+)$", "$1.png");
            if (File.Exists(ImagePath))
            {
                Image img = Image.FromFile(ImagePath);
                nPercent = (float)32 / (float)img.Height;
                nPercentW = nPercent * img.Width;
                nPercentH = nPercent * img.Height;
                e.Graphics.DrawImage(Image.FromFile(ImagePath), 13, 8, nPercentW, nPercentH);
                nPercentW -= 32;
            }
            else
            {
                e.Graphics.DrawImage(Icon.ExtractAssociatedIcon(ToolTipText).ToBitmap(), 13, 8);
            }
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), 53 + nPercentW, 8);
        }
    }
}