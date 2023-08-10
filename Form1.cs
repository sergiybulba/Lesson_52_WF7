using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;

namespace Lesson_52_WF_7
{
    public partial class Form1 : Form
    {
        private string path;
        private ImageList imageList;
        private int indexIcon = 0;

        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog chooseFolder = new FolderBrowserDialog();
            chooseFolder.ShowDialog();
            path = chooseFolder.SelectedPath;
            if (path == null)
            {
                MessageBox.Show("Directory doesn't selected");
                return;
            }
            this.textBox1.Text = path;
            button2.Enabled = true;
            button3.Enabled = true;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            imageList = new ImageList();
            imageList.Images.Add(new Bitmap("folder.png"));
            imageList.Images.Add(new Bitmap("file.png"));
            treeView1.ImageList = imageList;

            TreeNode roof = new TreeNode(path, 0, 0);
            treeView1.Nodes.Add(roof);
            this.GenerateFromFiles(roof);
            treeView1.ExpandAll();
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void GenerateFromFiles(TreeNode node)
        {
            string[] folders = Directory.GetDirectories(node.Text);
            if (folders.Length > 0)
            {
                foreach (string folder in folders)
                {
                    TreeNode newNode = new TreeNode(folder, 0, 0);
                    node.Nodes.Add(newNode);
                    GenerateFromFiles(newNode);
                }
            }

            string[] files = Directory.GetFiles(node.Text);
            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    TreeNode newNode = new TreeNode(file, 1, 1);
                    node.Nodes.Add(newNode);
                }
            }
            return;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            imageList = new ImageList();
            imageList.Images.Add(new Bitmap("folder.png"));
            treeView1.ImageList = imageList;

            TreeNode roof = new TreeNode(path, 0, 0);
            treeView1.Nodes.Add(roof);
            this.GenerateFromRegistry(roof);
            treeView1.ExpandAll();
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void GenerateFromRegistry(TreeNode node)
        {
            string[] folders = Directory.GetDirectories(node.Text);
            if (folders.Length > 0)
            {
                foreach (string folder in folders)
                {
                    TreeNode newNode = new TreeNode(folder, 0, 0);
                    node.Nodes.Add(newNode);
                    GenerateFromRegistry(newNode);
                }
            }

            string[] files = Directory.GetFiles(node.Text);
            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    Icon icon = Icon.ExtractAssociatedIcon(file);
                    imageList.Images.Add(icon);
                    treeView1.ImageList = imageList;
                    indexIcon++;

                    TreeNode newNode = new TreeNode(file, indexIcon, indexIcon);
                    node.Nodes.Add(newNode);
                }
            }
            return;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            treeView1.Nodes.Clear();
            path = string.Empty;
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
        }
    }
}