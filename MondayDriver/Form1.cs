using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MondayDriver
{
    public partial class Form1 : Form
    {
        private const string accesDeniedDrives = "You have no permisson to acces the drives!";
        private const string accesDenied = "You have no permisson to this action!";
        const string caption = "Error";
        DriveInfo[] drives;
        DirectoryInfo[] dirs;
        FileInfo[] files;

        public Form1()
        {
            InitializeComponent();
            drives = FileHandler.GetAllDrives();
            FillComboBox();
            string[] drivePath = comboBoxDrives.Text.Split(' ');
            textBoxPath.Text = drivePath[0];
            FillListBox(drivePath[0]);
        }

        private void FillComboBox()
        {
            if (drives != null)
            {
                foreach (var drive in drives)
                {
                    comboBoxDrives.Items.Add(drive.ToString() + " <" + drive.DriveType.ToString() + ">");
                }
                comboBoxDrives.Text = drives[0].Name  + " <" + drives[0].DriveType + ">"; 
            }
            else
            {
                var result = MessageBox.Show(accesDeniedDrives, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Question);
            }
        }

        private void FillListBox(string filePath)
        {
            listBoxFiles.Items.Clear();
            dirs = FileHandler.GetAllDirectories(filePath);
            files = FileHandler.GetAllFiles(filePath);
            if (dirs != null)
            {
                foreach (var dir in dirs)
                {
                    listBoxFiles.Items.Add(dir);
                }
            }
            if (files != null)
            {
                foreach (var file in files)
                {
                    listBoxFiles.Items.Add(file);
                }
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (IsDirectory())
            {
                textBoxPath.Text += dirs[listBoxFiles.SelectedIndex].ToString() + @"\";
                FillListBox(textBoxPath.Text);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            bool isSuccessfull = FileHandler.DeleteFile(GenerateFilePath());
            if (isSuccessfull)
            {
                var result = MessageBox.Show("File deleted", "Succesfull",
                                                                MessageBoxButtons.OK,
                                                                MessageBoxIcon.Question);
            }
            else
            {
                var result = MessageBox.Show(accesDenied, caption,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Question);
            }
        }

        private bool IsDirectory()
        {
            return listBoxFiles.SelectedIndex < dirs.Count();
        }

        private string GenerateFilePath()
        {
            string filePath;
            if (IsDirectory())
            {
                filePath = textBoxPath.Text + dirs[listBoxFiles.SelectedIndex];
            }
            else
            {
                filePath = textBoxPath.Text + files[listBoxFiles.SelectedIndex - files.Count()];
            }

            return filePath;
        }
    }
}
