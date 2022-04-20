//
//  Add methods to test with the prefix "TEST_", Example:
//  public void TEST_NewMethod(); //must be public void
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UTIL;

namespace UtilitiesTester_Tester
{
    public partial class Form1 : Form
    {

        private static string PREFIX = "TEST_";
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

            } else
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

            //tb.Append("Starting test: " + selection);
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
        public void TEST_AppConfigEx()
        {

            //config.Save() writes temp file so user must have R/W access
            //this will FAIL if app.exe.config installed in ProgramFiles\app.exe

            AppConfigEx config = new AppConfigEx();

            if (!config.Exists())
            {
                tb.Append("ERROR config file not found: " + config.GetPath());
                return;
            }

            string sAttr = config.Get("Tree");

            tb.Append("Tree = " + sAttr);

            if (sAttr == "Sycamore")
            {
                sAttr = "Plum";
            }
            else
            {
                sAttr = "Sycamore";
            }
            config.Set("Tree", sAttr);

            sAttr = config.Get("Tree");

            tb.Append("Tree = " + sAttr);

            //
            tb.Append("\r\nDictionary (sorted): ");

            SortedDictionary<string, string> dict = config.GetDictionary();

            foreach (KeyValuePair<string, string> kvp in dict)
            {
                tb.Append(String.Format("{0}={1}", kvp.Key, kvp.Value));
            }

            //
            tb.Append("\r\nHash Table (unsorted): ");

            Hashtable ht = new Hashtable();

            NameValueCollection settings = config.GetAppSettings();

            foreach (var key in settings.AllKeys)
            {
                ht.Add(key, settings[key]);
            }

            ICollection keys = ht.Keys;

            foreach (string key in keys)
            {
                tb.Append(key + ": " + ht[key]);
            }

            //
            tb.Append("\r\nCase Insensitive:");

            tb.Append(String.Format("Fruit={0}", config.Get("fRuIt")));

        }
        public void TEST_IniFile()
        {

            string iniFilePath = Assembly.GetExecutingAssembly().GetName().Name + ".ini";

            //IniReader ini = new IniReader("rfs.ini"); // (iniFilePath);
            IniReader ini = new IniReader(iniFilePath); //no errors, just empty strings

            if (!ini.Exists())
            {
                tb.Append("Ini file not found: " + iniFilePath);
                return;
            }

            tb.Append("Ini file dictionary:");

            System.Collections.Generic.Dictionary<string, string> dict = ini.GetDictionary();

            foreach (System.Collections.Generic.KeyValuePair<string, string> entry in dict)
            {
                tb.Append(entry.Key + ": " + entry.Value);
            }

        }
        public void TEST_LogFile()
        {
            LogFile log = new LogFile();
            log.Clear();
            log.Debug("test");
            log.Error("test");
            log.Fatal("test");
            log.Info("test");
            log.Trace("test");
            log.Warning("test");

            log.WriteLine("test");

            textBox1.Text += "Log file written: " + logFilePath + Environment.NewLine;

            buttonOpenLog_Click(null, null);

        }
        public void TEST_PathEx()
        {
            string path = Assembly.GetEntryAssembly().Location;
            
            tb.Append(String.Format("{0,-15}: {1}", "FullPath", path.FullPath()));
            tb.Append(String.Format("{0,-15}: {1}", "Dir", path.Dir()));
            tb.Append(String.Format("{0,-15}: {1}", "ParentDir", path.ParentDir()));
            tb.Append(String.Format("{0,-15}: {1}", "FileName", path.FileName()));
            tb.Append(String.Format("{0,-15}: {1}", "NameNoExt", path.NameNoExt()));
            tb.Append(String.Format("{0,-15}: {1}", "Drive", path.Drive()));
            tb.Append(String.Format("{0,-15}: {1}", "Ext", path.Ext()));
            tb.Append(String.Format("{0,-15}: {1}", "Root", path.Root()));
            tb.Append(String.Format("{0,-15}: {1}", "Exists", path.Exists()));
            tb.Append(String.Format("{0,-15}: {1}", "IsRooted", path.IsRooted()));
            tb.Append(String.Format("{0,-15}: {1}", "HasExt", path.HasExt()));
            tb.Append(String.Format("{0,-15}: {1}", "PathSeparator", path.PathSeparator()));
            tb.Append(String.Format("{0,-15}: {1}", "DirSeparator", path.DirSeparator()));
            tb.Append(String.Format("{0,-15}: {1}", "VolSeparator", path.VolSeparator()));
        }
        public void TEST_SplitPath()
        {
            string exePath = Assembly.GetEntryAssembly().Location;
            SplitPath path = new SplitPath(exePath);
            tb.Append(String.Format("{0,-15}: {1}", "FullPath", path.FullPath));
            tb.Append(String.Format("{0,-15}: {1}", "Dir", path.Dir));
            tb.Append(String.Format("{0,-15}: {1}", "ParentDir", path.ParentDir));
            tb.Append(String.Format("{0,-15}: {1}", "FileName", path.FileName));
            tb.Append(String.Format("{0,-15}: {1}", "NameNoExt", path.NameNoExt));
            tb.Append(String.Format("{0,-15}: {1}", "Drive", path.Drive));
            tb.Append(String.Format("{0,-15}: {1}", "Ext", path.Ext));
            tb.Append(String.Format("{0,-15}: {1}", "Root", path.Root));
            tb.Append(String.Format("{0,-15}: {1}", "Exists", path.Exists));
            tb.Append(String.Format("{0,-15}: {1}", "IsRooted", path.IsRooted));
            tb.Append(String.Format("{0,-15}: {1}", "HasExt", path.HasExt));
            tb.Append(String.Format("{0,-15}: {1}", "PathSeparator", path.PathSeparator));
            tb.Append(String.Format("{0,-15}: {1}", "DirSeparator", path.DirSeparator));
            tb.Append(String.Format("{0,-15}: {1}", "VolSeparator", path.VolSeparator));
            return;

            string randomFileNameWithDirSep = @"\" + exePath;
            string tempPath = Path.GetTempPath();
            //string tempPathNoDirSep = tempPath.Substring(0, tempPath.Length - 1);
            string tempPathNoDirSep = tempPath.TrimEnd(Path.DirectorySeparatorChar);
            string tempPathDoubleDirSep = Path.GetTempPath() + @"\";

            tb.Append(String.Format("{0,-15}: {1}", "Path", tempPathNoDirSep));
            tb.Append(String.Format("{0,-15}: {1}", "FileName", exePath));
            tb.Append(String.Format("{0,-15}: {1}", "Combine", Path.Combine(tempPathNoDirSep, exePath)));
            tb.Append("");
            tb.Append(String.Format("{0,-15}: {1}", "Path", tempPath));
            tb.Append(String.Format("{0,-15}: {1}", "FileName", exePath));
            tb.Append(String.Format("{0,-15}: {1}", "Combine", Path.Combine(tempPath, exePath)));
            tb.Append("");
            tb.Append(String.Format("{0,-15}: {1}", "Path", tempPathDoubleDirSep));
            tb.Append(String.Format("{0,-15}: {1}", "FileName", exePath));
            tb.Append(String.Format("{0,-15}: {1}", "Combine", Path.Combine(tempPathDoubleDirSep, exePath)));
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
        public void TEST_TextBoxEx()
        {
            tb.Append("Append test");
            tb.WriteLine("WriteLine test");

            tb.Append(String.Format("Line1: {0}, {1}", "this is a test", 1));
            tb.Append(String.Format("Line2: {0}, {1}, {2}", "this is a test", 1, 3.1415927));

            textBox1.AppendText("Append Text test" + Environment.NewLine);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}


