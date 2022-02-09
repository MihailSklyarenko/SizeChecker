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

            var directories = _mainViewModel.GetDirectories(node.FullPath);

            foreach (string dirinfo in directories)
            {
                TreeNode dir = new TreeNode(dirinfo);
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

        private async void mainTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            mainListView.Items.Clear();
            var filesWithFolders = await Task.Run( () => _mainViewModel.GetDirectoriesWithFiles(e.Node.FullPath));

            foreach (var folder in filesWithFolders.Folders)
            {
                ListViewItem lvi = new ListViewItem(folder.Name);
                lvi.SubItems.Add(folder.Size.ToString());
                mainListView.Items.Add(lvi);
            }


            foreach (var file in filesWithFolders.Files)
            {
                ListViewItem lvi = new ListViewItem($"{file.Name}.{file.Extension}");
                lvi.Tag = file.FullName;
                lvi.SubItems.Add(file.Size.ToString());
                mainListView.Items.Add(lvi);
            }
        }        

    }
}
