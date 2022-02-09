using SizeChecker.Interfaces.MainForm;
using SizeChecker.ViewModels.MainForm;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SizeChecker
{
    public partial class MainForm : Form
    {
        public IMainViewModel _mainViewModel;
        public MainForm()
        {
            InitializeComponent();
            mainListView.View = View.Details;
            mainListView.GridLines = true;
            mainListView.FullRowSelect = true;
            mainListView.MultiSelect = false;
            mainListView.AllowColumnReorder = true;

            mainListView.Columns.Add("Name", 300);
            mainListView.Columns.Add("Size", 100);

            _mainViewModel = new MainViewModel();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var drives = _mainViewModel.GetAllDrives();

            mainTreeView.Nodes.Clear();
            mainTreeView.BeginUpdate();

            foreach (string currentDrive in drives)
            {
                TreeNode drive = new TreeNode(currentDrive, 0, 0);
                mainTreeView.Nodes.Add(drive);

                GetDirectories(drive);
            }

            mainTreeView.EndUpdate();
        }

        private void GetDirectories(TreeNode node)
        {
            node.Nodes.Clear();

            DirectoryInfo[] directories;
            string fullPath = node.FullPath;
            DirectoryInfo directory = new DirectoryInfo(fullPath);

            try
            {
                directories = directory.GetDirectories();
            }
            catch
            {
                return;
            }

            foreach (DirectoryInfo dirinfo in directories)
            {
                TreeNode dir = new TreeNode(dirinfo.Name);
                node.Nodes.Add(dir);
            }
        }

        private void mainTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            mainTreeView.BeginUpdate();

            foreach (TreeNode node in e.Node.Nodes)
            {
                GetDirectories(node);
            }

            mainTreeView.EndUpdate();
        }

        private void mainTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = e.Node;
            var fullPath = selectedNode.FullPath;

            DirectoryInfo directoryInfo = new DirectoryInfo(fullPath);
            FileInfo[] files;
            DirectoryInfo[] directories;

            try
            {
                files = directoryInfo.GetFiles();
                directories = directoryInfo.GetDirectories();
            }
            catch
            {
                return;
            }

            mainListView.Items.Clear();

            foreach (DirectoryInfo dirInfo in directories)
            {
                ListViewItem lvi = new ListViewItem(dirInfo.Name);

                double size = 0;
                Task.Run(() => GetDirectorySize(fullPath, ref size));
                lvi.SubItems.Add(size.ToString());

                mainListView.Items.Add(lvi);
            }


            foreach (FileInfo fileInfo in files)
            {
                ListViewItem lvi = new ListViewItem(fileInfo.Name);
                lvi.Tag = fileInfo.FullName;
                lvi.Tag = fileInfo.FullName;
                lvi.SubItems.Add(fileInfo.Length.ToString());

                string filenameExtension =
                  Path.GetExtension(fileInfo.Name).ToLower();
                mainListView.Items.Add(lvi);
            }
        }

        private double GetDirectorySize(string currentFolder, ref double catalogSizeInBytes)
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
                    GetDirectorySize(subfolders.FullName, ref catalogSizeInBytes);
                }
                var result = Math.Round((double)(catalogSizeInBytes / 1024 / 1024 / 1024), 2);
                return result;
            }
            catch(Exception)
            {
                return 0;
            }
        }

    }
}
