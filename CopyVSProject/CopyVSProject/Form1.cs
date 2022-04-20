//
// Modified from: https://github.com/shenqiangbin/ChangeVSProjectName
//

using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyVSProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            toolStripStatusLabel1.Text = "Version: " + version.ToString();

            string selectedFolder = Properties.Settings.Default.SelectedFolder;

            if (selectedFolder == String.Empty)
            {
                txtBoxSource.Text = @"C:\";
            }
            else
            {
                txtBoxSource.Text = selectedFolder;
            }
        }
        private bool IsBinary(string path)
        {
            bool result = false;

            if (File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                int buffSize = 512;
                byte[] buffer = new byte[buffSize];
                int byteCount = fs.Read(buffer, 0, buffSize);
                fs.Close();
                for (int i = 1; i < byteCount; i++)
                {
                    if (buffer[i] == 0x00 && buffer[i - 1] == 0x00)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
        private bool IsExcluded(string path)
        {
            bool result = false;

            string dirExclude = Regex.Replace(txtBoxExclude.Text, @"\s", "");

            string[] dirs = dirExclude.Split(';');

            foreach (string dir in dirs)
            {
                if ( (path.Contains(@"\" + dir)) && (dir != string.Empty) )
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        private bool IsVerbose()
        {
            if (checkBox1.Checked == true) { return true; } else { return false; }
        }
        private void CopyAll(string SourcePath, string DestinationPath)
        {
            //Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
            {
                WriteVerbose("COPY DIR   : " + dirPath);

                if (IsExcluded(dirPath))
                {
                    WriteVerbose("EXCLUDE DIR: " + dirPath);
                } else
                {
                    WriteVerbose("CREATE DIR : " + dirPath);
                    Directory.CreateDirectory(Path.Combine(DestinationPath, dirPath.Remove(0, SourcePath.Length + 1)));
                }
            }

            //Copy all the files and replace any files with the same name
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories))
            {
                WriteVerbose("COPY FILE  : " + newPath);

                if (IsExcluded(newPath))
                {
                    WriteVerbose("EXCLUDE FIL: " + newPath);
                    continue;
                }
                else
                {
                    File.Copy(newPath, Path.Combine(DestinationPath, newPath.Remove(0, SourcePath.Length + 1)), true);
                }
            }
        }
        private int DoClean(string path)
        {
            int count = 0;
            DirectoryInfo di = new DirectoryInfo(path);
            DirectoryInfo[] directories = di.GetDirectories("*.*", SearchOption.AllDirectories);

            foreach (DirectoryInfo dir in directories)
            {
                WriteVerbose("DIR      : [" + dir.FullName + "]");
                //WriteVerbose("ISEXCLUDED : " + IsExcluded(dir.FullName));

                if (IsExcluded(dir.FullName))
                {
                    if (Directory.Exists(dir.FullName))
                    {
                        count += 1;
                        WriteVerbose("DELETE DIR: " + dir.ToString());
                        dir.Delete(recursive: true);
                        Thread.Sleep(25);
                    }
                }
            }
            return count;
        }
        private int DoZip(string path)
        {
            int count = 0;
            DirectoryInfo di = new DirectoryInfo(path);
            DirectoryInfo[] directories = di.GetDirectories("*.*", SearchOption.AllDirectories);

            int pos = path.LastIndexOf(@"\");
            string rootDir = path.Substring(0, pos + 1);
            int rootDirLen = rootDir.Length;

            string zipFile = path + ".zip";
            File.Delete(zipFile); //no need to check if exists
            ZipArchive zipArchive = ZipFile.Open(zipFile, ZipArchiveMode.Create);

            foreach (FileInfo fi in di.GetFiles("*.*", SearchOption.AllDirectories))
            {
                //WriteLine("FOUND: " + fi.FullName);

                string fn = fi.FullName;

                if (fi.FullName.Contains(@"\.vs\") |
                    fi.FullName.Contains(@"\bin\") |
                    fi.FullName.Contains(@"\obj\") |
                    fi.FullName.Contains(@"\x86\") |
                    fi.FullName.Contains(@"\x64\"))
                {
                    //WriteLine("Skipping: " + fi.FullName);
                    continue;
                }
                else
                {
                    count += 1;

                    string addPath = fi.FullName;
                    string addName = addPath.Substring(rootDirLen);

                    WriteVerbose("");
                    WriteVerbose("addPath : " + addPath);
                    WriteVerbose("addName : " + addName);

                    zipArchive.CreateEntryFromFile(addPath, addName);

                }
            }
            zipArchive.Dispose();
            return count;
        }
        private async void btnClean_Click(object sender, EventArgs e)
        {
            txtRichTextBox.Text = string.Empty;

            string path = txtBoxSource.Text;

            if (!Directory.Exists(path))
            {
                WriteLine("That Project directory doesn't exist!");
                return;
            }
            string message = "Projct Directory:\r\n\n" + path + "\r\n\nAre you sure you want to delete Excluded Dirs?";
            string title = Form1.ActiveForm.Text;
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                WriteLine("Cleaning Path  : " + path);
                int r = await Task.Run(() => DoClean(path));
                WriteLine("Folders Cleaned: " + r);
            }
            else
            {
                WriteLine("Clean Path Aborted.");
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtRichTextBox.Text = string.Empty;
        }
        private void btnCopy_Click(object sender, EventArgs e)
        {

            string sourceDir = txtBoxSource.Text;
            string targetDir = string.Empty;

            txtRichTextBox.Text = string.Empty;

            if (!Directory.Exists(sourceDir))
            {
                WriteLine("Source doesn't exist: " + sourceDir);
                return;
            } else
            {
                folderBrowserDialog.SelectedPath = sourceDir;
            }

            var dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                targetDir = folderBrowserDialog.SelectedPath;

                if (targetDir.EndsWith(@"\"))
                    targetDir = targetDir.TrimEnd(Path.DirectorySeparatorChar);

            } else
            {
                WriteLine("No Target selected.");
                return;
            }

            if ( !Directory.Exists(targetDir) )
            {
                WriteLine("Target doesn't exist: " + targetDir);
                return;
            }

            if (targetDir == txtBoxSource.Text)
            {
                WriteLine("Can't copy a Project to itsself!");
                return;
            }
            string[] files = Directory.GetFiles(targetDir);

            if (files.Length != 0)
            {
                string message = "Target Directory Not Empty:\r\n\n" + targetDir + "\r\n\nDo you want to delete all files?";
                string title = Form1.ActiveForm.Text;
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Directory.Delete(targetDir, true);
                    Directory.CreateDirectory(targetDir);
                    WriteVerbose("CLEAN TARGET DIR: " + targetDir);
                }
                else
                {
                    return;
                }
            }

            WriteLine("Copy From  : " + txtBoxSource.Text + "\r\nCopy To    : " + targetDir);

            CopyAll(sourceDir, targetDir);

            string oldText = Path.GetFileNameWithoutExtension(sourceDir);
            string newText = Path.GetFileNameWithoutExtension(targetDir);
            string dir = targetDir;

            new Thread(() =>
            {
                try
                {
                    FindTextInDirAndReplace(new DirectoryInfo(dir), oldText, newText);
                } catch (Exception ex)
                {
                    WriteLine("ERROR: " +ex.Message);
                }
            }).Start();

            WriteLine("Copy Status: Finished.");
        }
        private void btnExplore_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", txtBoxSource.Text);
        }
        private void btnSelectSource_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = txtBoxSource.Text;

            var dialogResult = folderBrowserDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string temp = folderBrowserDialog.SelectedPath;

                if (temp.EndsWith(@"\"))
                    temp = temp.TrimEnd(Path.DirectorySeparatorChar);

                txtBoxSource.Text = temp;

                Properties.Settings.Default.SelectedFolder = temp;
                Properties.Settings.Default.Save();
            }
        }

        private async void btnZip_Click(object sender, EventArgs e)
        {
            txtRichTextBox.Text = "";

            string path = txtBoxSource.Text;

            //if endswith "\" then remove it
            if (path.EndsWith(@"\"))
            {
                path = path.Substring(0, path.Length - 1);
                txtBoxSource.Text = path;
            }

            if (!Directory.Exists(path))
            {
                WriteLine("The directory does not exist: '" + path);
                return;
            }

            WriteLine("Zip Path : " + path);

            int r = await Task.Run(() => DoZip(path));

            string zipFile = path + ".zip";
            string zipFileRenamed = path + "_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".zip";

            File.Move(zipFile, zipFileRenamed);

            WriteLine("Zip Items: " + r);
            WriteLine("Zip File : " + zipFileRenamed);

        }
        private void FindTextInDirAndReplace(DirectoryInfo dir, string findText, string newText)
        {
            string targetDir = Path.Combine(Path.GetDirectoryName(dir.FullName), newText);

            //    @"D:\Software\DEV\Work\csharp\Learning\ExamplesNew"; //"dir.FullName;

            FileInfo[] files = dir.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                WriteVerbose("FILE   : " + file.FullName);
                WriteVerbose("NAME   : " + file.Name);

                if (IsExcluded(file.FullName))
                {
                    WriteVerbose("EXCLUDE DELETE???: " + file.FullName);
                    //?? File.Delete(item.FullName);
                    continue;

                }

                else if (IsBinary(file.FullName))
                {
                    WriteVerbose("BINARY : " + file.FullName);
                    continue;
                }

                string content = File.ReadAllText(file.FullName);
                var encoding = new UTF8Encoding(true);
                File.WriteAllText(file.FullName, content.Replace(findText, newText), encoding);

                if (file.Name.Contains(findText))
                {
                    var newPath = Path.Combine(Path.GetDirectoryName(file.FullName), file.Name.Replace(findText, newText));

                    WriteVerbose("renameFILE:" + file.FullName + ", to: " + newPath);

                    File.Move(file.FullName, newPath);
                }

            }

            DirectoryInfo[] children = dir.GetDirectories();
            foreach (var item in children)
            {
                if (txtBoxExclude.Text.Contains(item.Name))
                    continue;

                FindTextInDirAndReplace(item, findText, newText);
            }

            WriteVerbose("FINDTEXT    :" + findText);
            WriteVerbose("NEWTEXT     :" + newText);
            WriteVerbose("DIR.FULLNAME:" + dir.FullName);
            WriteVerbose("DIR.NAME    :" + dir.Name);
            WriteVerbose("TARGETDIR   :" + targetDir);

            if ( (dir.Name.Contains(findText)) && (dir.FullName != targetDir) )
            {
                var newPath = Path.Combine(Path.GetDirectoryName(dir.FullName), dir.Name.Replace(findText, newText));
                Directory.Move(dir.FullName, newPath);

                WriteVerbose("renameDIR   :" + dir.FullName + ", to: " + newPath);

            }
        }
        private void WriteLine(string msg)
        {
            txtRichTextBox.AppendText(msg + Environment.NewLine);
        }
        private void WriteVerbose(string msg)
        {
            if (IsVerbose()) { txtRichTextBox.AppendText(msg + Environment.NewLine); }
        }
    }
}
