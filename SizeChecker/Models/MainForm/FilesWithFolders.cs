using System.Collections.Generic;

namespace SizeChecker.Models.MainForm
{
    public class FilesWithFolders
    {
        public List<Element> Folders { get; set; } = new List<Element> ();
        public List<File> Files { get; set; } = new List<File> ();
    }
}
