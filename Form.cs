using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

public partial class SearchFm : Form
{
    protected override void OnActivated(EventArgs e)
    {
        base.OnActivated(e);
        int num = 2;
        DwmSetWindowAttribute(base.Handle, 2, ref num, 4);

        MARGINS margins = new MARGINS
        {
            cyBottomHeight = 0,
            cxLeftWidth = 0,
            cxRightWidth = 0,
            cyTopHeight = 0
        };
        DwmExtendFrameIntoClientArea(base.Handle, ref margins);
    }
    [DllImport("dwmapi.dll")]
    public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
    [DllImport("dwmapi.dll")]
    public static extern int DwmExtendFrameIntoClientArea(IntPtr hdc, ref MARGINS marInset);
    public struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    public static SearchFm fm;
    public SearchFm()
    {
        InitializeComponent();
        fm = this;
    }
}