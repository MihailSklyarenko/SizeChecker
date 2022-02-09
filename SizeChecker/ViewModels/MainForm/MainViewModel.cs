using SizeChecker.Interfaces.MainForm;
using SizeChecker.Models.MainForm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SizeChecker.ViewModels.MainForm
{
    public class MainViewModel : IMainViewModel
    {
        public string[] GetAllDrives()
        {
            return Directory.GetLogicalDrives();
        }

        public List<string> GetDirectories(string fullPathToDirectory)
        {
            List<string> result = new List<string>();
            DirectoryInfo[] directories;
            DirectoryInfo directory = new DirectoryInfo(fullPathToDirectory);

            try
            {
                directories = directory.GetDirectories();
            }
            catch
            {
                return new List<string>();
            }

            foreach (DirectoryInfo dirinfo in directories)
            {
                result.Add(dirinfo.Name);
            }

            return result;
        }

        public async Task<double> GetDirectorySize(string currentFolder)
        {
            double size = 0;
            await Task.Run(() => GetDirSize(currentFolder, ref size));
            return size;
        }

        private double GetDirSize(string currentFolder, ref double catalogSizeInBytes)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(currentFolder);
                DirectoryInfo[] directories = directoryInfo.GetDirectories();
                FileInfo[] files = directoryInfo.GetFiles();
                foreach (FileInfo file in files)
                {
                    catalogSizeInBytes = catalogSizeInBytes + file.Length;
                }

                foreach (DirectoryInfo subfolders in directories)
                {
                    GetDirSize(subfolders.FullName, ref catalogSizeInBytes);
                }
                return BytesToGigabytes(catalogSizeInBytes);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private double BytesToGigabytes(double bytes) => Math.Round((double)(bytes / 1024 / 1024 / 1024), 2);
        private double BytesToMegabytes(double bytes) => Math.Round((double)(bytes / 1024 / 1024), 2);

        public FilesWithFolders GetDirectoriesWithFiles(string fullPathToDirectory)
        {
            FilesWithFolders result = new FilesWithFolders();

            DirectoryInfo directoryInfo = new DirectoryInfo(fullPathToDirectory);
            FileInfo[] files;
            DirectoryInfo[] directories;

            try
            {
                files = directoryInfo.GetFiles();
                directories = directoryInfo.GetDirectories();
            }
            catch
            {
                return result;
            }

            foreach (DirectoryInfo dirInfo in directories)
            {
                double size = 0;
                GetDirSize(fullPathToDirectory, ref size);

                result.Folders.Add(new Element() 
                { 
                    Name = dirInfo.Name,                     
                    Size = $"{BytesToGigabytes(size)} GB"
                });
            }
               
            foreach (FileInfo fileInfo in files)
            {
                result.Files.Add(new Models.MainForm.File()
                {
                    Name = fileInfo.Name,
                    FullName = fileInfo.FullName,
                    Size = $"{BytesToMegabytes(fileInfo.Length)} MB",
                    Extension = fileInfo.Extension
                });
            }

            return result;
        }               
    }
}
