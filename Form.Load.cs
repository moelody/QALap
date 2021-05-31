using System;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

public partial class SearchFm : Form
{
    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();
    [DllImport("user32.dll")]
    public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int IParam);
    public const int WM_SYSCOMMAND = 0x0112;
    public const int SC_MOVE = 0xF010;
    public const int HTCAPTION = 0x0002;
    protected void MoveForm()
    {
        ReleaseCapture();
        SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
    }
    private void Form1_MouseDown(object sender, MouseEventArgs e)
    {
        MoveForm();
    }
    private void Form1_Load(object sender, EventArgs e)
    {
    }
    private void Form1_Shown(object sender, EventArgs e)
    {
        this.Activate();
    }
}