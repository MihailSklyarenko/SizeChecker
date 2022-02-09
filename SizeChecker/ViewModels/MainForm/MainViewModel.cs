using SizeChecker.Interfaces.MainForm;
using System.IO;

namespace SizeChecker.ViewModels.MainForm
{
    public class MainViewModel : IMainViewModel
    {
        public string[] GetAllDrives()
        {
            return Directory.GetLogicalDrives();
        }
    }
}
