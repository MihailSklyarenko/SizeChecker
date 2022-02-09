using SizeChecker.Models.MainForm;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SizeChecker.Interfaces.MainForm
{
    public interface IMainViewModel
    {
        string[] GetAllDrives();
        List<string> GetDirectories(string fullPathToDirectory);
        Task<double> GetDirectorySize(string currentFolder);
        FilesWithFolders GetDirectoriesWithFiles(string fullPathToDirectory);
    }
}