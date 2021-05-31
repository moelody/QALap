using System.IO;
using System.Windows.Forms;

public partial class SearchFm
{
    private DirectoryInfo ScriptsPath;
    private DirectoryInfo ExpressionsPath;
    private DirectoryInfo AepProjectPath;
    private DirectoryInfo PresetsPath;
    public bool InitStart(string sPath = "", string ePath = "", string aPath = "")
    {
        if (!Directory.Exists(sPath))
        {
            MessageBox.Show(sPath + "脚本路径不存在");
            return false;
        }
        else if (!Directory.Exists(ePath))
        {
            MessageBox.Show(ePath + "表达式路径不存在");
            return false;
        }
        else if (!Directory.Exists(aPath))
        {
            MessageBox.Show(aPath + "工程路径不存在");
            return false;
        }
        else
        {
            ScriptsPath = new DirectoryInfo(sPath);
            ExpressionsPath = new DirectoryInfo(ePath);
            AepProjectPath = new DirectoryInfo(aPath);
            return true;
        }
    }
}