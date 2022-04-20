//
//  Test examples
//  Add a new test in Form1.cs: public void EXAMPLE_NewTest()
//  The program will scan the Form1.cs file and add the new test to the listbox
//
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UTIL;

namespace Examples_Tester
{
    public partial class Form1 : Form
    {
        private static string PREFIX = "EXAMPLE_";

        private static bool HIDE_PREFIX = true;

        private static string logFilePath = Assembly.GetExecutingAssembly().GetName().Name + ".log";

        private TextBoxEx tb;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            tb = new TextBoxEx(textBox1);

            LoadListBox();
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
        private void buttonOpenLog_Click(object sender, EventArgs e)
        {
            if (File.Exists(logFilePath))
            {
                Process.Start(logFilePath);

            }
            else
            {
                textBox1.Text += "Log file not found: " + logFilePath + Environment.NewLine;
            }
        }
        private void buttonTest_Click(object sender, EventArgs e)
        {
            tb.Clear();

            if (listBox1.SelectedItems.Count == 0)
            {
                tb.Append("Nothing selected.");
                return;
            }

            string selection = listBox1.SelectedItems[0].ToString();

            toolStripStatusLabel1.Text = "Test: " + selection;

            if (HIDE_PREFIX)
            {
                selection = PREFIX + selection;
            }

            try
            {
                MethodInfo mi = GetType().GetMethod(selection);
                mi.Invoke(this, null);
            }
            catch (Exception ex)
            {
                tb.Append("ERROR ex =" + ex.Message);
            }

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Selected: " + listBox1.SelectedItems[0].ToString();
        }
        private static string GetThisFilePath([CallerFilePath] string path = null)
        {
            return path;
        }
        private void LoadListBox()
        {

            string path = GetThisFilePath();

            List<string> methodNames = new List<string>();

            string[] lines = File.ReadAllLines(path);

            const string methodPattern =
                //@"(public|private|bool|int|string|static|void)\s* ([\w]+)\(.*\)";
                @"(public|void)\s* ([\w]+)\(.*\)";

            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("public void " + PREFIX))
                {

                    Match m = Regex.Match(line, methodPattern, RegexOptions.IgnorePatternWhitespace);

                    if (m.Success)
                    {
                        Group g = m.Groups[m.Groups.Count - 1];

                        if (HIDE_PREFIX)
                        {
                            methodNames.Add(g.Value.Substring(PREFIX.Length));
                        }
                        else
                        {
                            methodNames.Add(g.Value);
                        }
                    }
                }
            }

            methodNames.Sort();

            foreach (var item in methodNames)
                listBox1.Items.Add(item);

        }
        public void EXAMPLE_FileInfo()
        {
            string exePath = Assembly.GetEntryAssembly().Location;
            FileInfo fi = new FileInfo(exePath);

            string fullName = fi.FullName;
            string dir = fi.DirectoryName;
            string name = fi.Name;
            string ext = fi.Extension;
            bool exists = fi.Exists;

            if (fi.Exists)
            {
                long len = fi.Length;
                bool IsReadOnly = fi.IsReadOnly;
                DateTime creationTime = fi.CreationTime;
                DateTime accessTime = fi.LastAccessTime;
                DateTime updatedTime = fi.LastWriteTime;

                tb.Append(String.Format("{0,-15}: {1}", "FullName", fullName));
                tb.Append(String.Format("{0,-15}: {1}", "DirectoryName", dir));
                tb.Append(String.Format("{0,-15}: {1}", "Name", name));
                tb.Append(String.Format("{0,-15}: {1}", "Ext", ext));
                tb.Append(String.Format("{0,-15}: {1}", "Exists", exists));
                tb.Append(String.Format("{0,-15}: {1} bytes", "Length", String.Format("{0:n0}", len)));
                tb.Append(String.Format("{0,-15}: {1}", "IsReadOnly", IsReadOnly));
                tb.Append(String.Format("{0,-15}: {1}", "CreationTime", creationTime));
                tb.Append(String.Format("{0,-15}: {1}", "AccessTime", accessTime));
                tb.Append(String.Format("{0,-15}: {1}", "UpdatedTime", updatedTime));
            }
            else
            {
                tb.Append("File not exist: " + exePath);
            }
        }
        public void EXAMPLE_FilePath()
        {
            string exePath = Assembly.GetEntryAssembly().Location;
            string fname = Path.GetFileName(exePath);
            string dirName = Path.GetDirectoryName(exePath);
            string parent = Directory.GetParent(dirName).ToString();

            tb.Append(String.Format("{0,-15}: {1}", "FullPath", Path.GetFullPath(exePath)));
            tb.Append(String.Format("{0,-15}: {1}", "DirectoryName", dirName));
            tb.Append(String.Format("{0,-15}: {1}", "Parent Dir", parent));
            tb.Append(String.Format("{0,-15}: {1}", "FileName", Path.GetFileName(exePath)));
            tb.Append(String.Format("{0,-15}: {1}", "FileNameNoExt", Path.GetFileNameWithoutExtension(exePath)));
            tb.Append(String.Format("{0,-15}: {1}", "Extension", Path.GetExtension(exePath)));
            tb.Append("");
            tb.Append(String.Format("{0,-15}: {1}", "PathRoot", Path.GetPathRoot(exePath)));
            tb.Append(String.Format("{0,-15}: {1}", "IsPathRooted", Path.IsPathRooted(exePath)));
            tb.Append(String.Format("{0,-15}: {1}", "HasExtension", Path.HasExtension(exePath)));
            tb.Append(String.Format("{0,-15}: {1}", "PathSeparator", Path.PathSeparator));
            tb.Append(String.Format("{0,-15}: {1}", "DirSeparator", Path.DirectorySeparatorChar));
            tb.Append(String.Format("{0,-15}: {1}", "VolSeparator", Path.VolumeSeparatorChar));
            tb.Append("");
        }
        public void EXAMPLE_FilePathCombine()
        {
            string randomFileName = Path.GetRandomFileName();
            string randomFileNameWithDirSep = @"\" + randomFileName;
            string tempPath = Path.GetTempPath();
            //string tempPathNoDirSep = tempPath.Substring(0, tempPath.Length - 1);
            string tempPathNoDirSep = tempPath.TrimEnd(Path.DirectorySeparatorChar);
            string tempPathDoubleDirSep = Path.GetTempPath() + @"\";

            tb.Append(String.Format("{0,-15}: {1}", "Path", tempPathNoDirSep));
            tb.Append(String.Format("{0,-15}: {1}", "FileName", randomFileName));
            tb.Append(String.Format("{0,-15}: {1}", "Combine", Path.Combine(tempPathNoDirSep, randomFileName)));
            tb.Append("");
            tb.Append(String.Format("{0,-15}: {1}", "Path", tempPath));
            tb.Append(String.Format("{0,-15}: {1}", "FileName", randomFileName));
            tb.Append(String.Format("{0,-15}: {1}", "Combine", Path.Combine(tempPath, randomFileName)));
            tb.Append("");
            tb.Append(String.Format("{0,-15}: {1}", "Path", tempPathDoubleDirSep));
            tb.Append(String.Format("{0,-15}: {1}", "FileName", randomFileName));
            tb.Append(String.Format("{0,-15}: {1}", "Combine", Path.Combine(tempPathDoubleDirSep, randomFileName)));
            tb.Append("");
            tb.Append(String.Format("{0,-15}: {1}", "Path", tempPathNoDirSep));
            tb.Append(String.Format("{0,-15}: {1}", "FileName", randomFileNameWithDirSep + ""));
            tb.Append(String.Format("{0,-15}: {1}", "Combine", Path.Combine(tempPathNoDirSep, randomFileNameWithDirSep)));
            tb.Append("");
            tb.Append(String.Format("{0,-15}: {1}", "Path", tempPath));
            tb.Append(String.Format("{0,-15}: {1}", "FileName", randomFileNameWithDirSep + ""));
            tb.Append(String.Format("{0,-15}: {1}", "Combine", Path.Combine(tempPath, randomFileNameWithDirSep)));
            tb.Append("");
            tb.Append("Note Path.Combine adds the Directory Separator if necessary");
            tb.Append("Note it doesn't support a double Dir Sep");
            tb.Append("Note the Path is ignored if the filename has a leading Dir Sep");

        }
        public void EXAMPLE_IconCopy()
        {
            string iconSource = @"..\..\Icons\change.ico";
            string iconTarget = @"..\..\Icons\change_copied.ico";
            string bmpTarget = @"..\..\Icons\change_copied.bmp";

            if (!File.Exists(iconSource))
            {
                tb.Append("ERROR Not Exists: " + iconSource);
            }


            try
            {
                File.Copy(iconSource, iconTarget, true);
                tb.Append("Copied: " + iconSource + ", to: " + iconTarget);
            }
            catch (Exception ex)
            {
                tb.Append("ERROR: " + ex.Message);
            }
            return;

            Icon appIcon = Icon.ExtractAssociatedIcon(iconSource);
            // Check if icon is null or not
            if (appIcon != null)
            {
                // Save the icon or do something else with it..
                appIcon.ToBitmap().Save(bmpTarget);

                Stream IconStream = System.IO.File.OpenWrite(iconTarget);
                appIcon.Save(IconStream);
            }

            tb.Append("Copied: " + iconSource + ", to: " + bmpTarget);
            //tb.Append("Copied: " + iconSource + ", to: " + iconTarget);

        }
        private bool IsBinary(string path)
        {
            bool result = false;

            if (File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                int buffSize = 512;
                byte[] buffer = new byte[buffSize];
                fs.Read(buffer, 0, buffSize);
                for (int i = 1; i < buffSize && i < buffer.Length; i++)
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
        public void EXAMPLE_IsBinary()
        {
            string path = @"..\..\Icons\change.ico";

            if (IsBinary(path))
            {
                tb.Append("IS BINARY    : " + path);
            }
            else
            {
                tb.Append("IS NOT BINARY: " + path);
            }

            path = @"..\..\Form1.cs";

            if (IsBinary(path))
            {
                tb.Append("IS BINARY    : " + path);
            }
            else
            {
                tb.Append("IS NOT BINARY: " + path);
            }
        }
        public void EXAMPLE_RemoveWhiteSpace()
        {
            string dirExclude = "bin; obj; .vs";

            tb.Append("dirExclude=" + dirExclude);

            dirExclude = Regex.Replace(dirExclude, @"\s", "");

            string[] dirs = dirExclude.Split(';');

            foreach (string dir in dirs)
                tb.Append("dir=" + dir);

        }
    }
}