/*
 * VS Extension Name
 *      VSIX.Name "GotoToolWindow"
 *      Version: source.extension.vsixmanifest
 * 
 * Tool Window and Tab Title
 *      GetTitle() "GOTO"
 *      MyToolWindow.cs
 * 
 * Icons
 *      Menu, View, Goto Tool Window
 *          VSCommandTable.vsct
 *          <Icon guid="ImageCatalogGuid" id="GoToSourceCode"/>
 * 
 *      Extensions, Manage, Installed
 *          source.extension.vsixmanifest, icon = 32x32
 *          not sure where this is shown? preview=200x200
 *      
 * To Install: Double-click on \bin\x64\Release\GotoToolWindow.vsix
 *             Increment version number to update in source.extension.vsixmanifest
 * 
 */

using Community.VisualStudio.Toolkit;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Threading;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Task = System.Threading.Tasks.Task;
using UTIL;
using System.Windows.Data;
using System.ComponentModel;
using Microsoft.VisualStudio.CommandBars;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace ToolWindow
{
    public partial class MyToolWindowControl : UserControl
    {
        private string CurrentActiveDocument { get; set; }

        DTE dte { get; set; }= null;
        EnvDTE.WindowEvents winEvents { get; set; } = null;
        EnvDTE.DocumentEvents docEvents { get; set; } = null;

        private LogFile Log { get; set; }

        private List<ListItem> GotoList { get; set; }

        private readonly string[] AccessibilityWords = { "public", "private", "protected", "static", "internal" };

        private readonly string[] TypeWords = { "class ", "enum ", "struct ", "record " };

        public MyToolWindowControl(Version vsVersion)
        {
            InitializeComponent();

            Log = new LogFile(@"D:\goto.log");
            Log.Clear();
            Log.Info("Log Cleared");

            ThreadHelper.ThrowIfNotOnUIThread();

            dte = Package.GetGlobalService(typeof(DTE)) as DTE;

            winEvents = dte.Events.WindowEvents;
            docEvents = dte.Events.DocumentEvents;

            winEvents.WindowActivated += WindowEvents_WindowActivated;
            docEvents.DocumentSaved += DocumentEvents_DocumentSaved;

        }

        private void MyToolWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void DocumentEvents_DocumentSaved(Document Document)
            => UpdateListAsync().FireAndForget();

        private void OnOpened(object sender, RoutedEventArgs e)
        {
            //Log.Warning("OnOpened");
        }
        private void OnClosed(object sender, RoutedEventArgs e)
        {
            //Log.Warning("OnClosed");

        }

        private void WindowEvents_WindowActivated(EnvDTE.Window GotFocus, EnvDTE.Window LostFocus)
        {
            _ = Task.Run(async () =>
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                if (GotFocus.Document.FullName != CurrentActiveDocument)
                {
                    CurrentActiveDocument = GotFocus.Document.FullName;
                    UpdateListAsync().FireAndForget();
                }
            });

        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listView.ItemsSource).Refresh();
        }

        private void listBoxGoto_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                ListItem item = new ListItem();
                item = (ListItem)listView.Items[listView.SelectedIndex];
                _ = GotoLineAsync(item.Number);
            }
        }
        private void listBoxGoto_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListItem item = new ListItem();
            item = (ListItem)listView.Items[listView.SelectedIndex];
            _ = GotoLineAsync(item.Number);
        }

        private async Task UpdateListAsync()
        {
            textBoxFilter.Clear();

            string codeBuffer = await GetCodeAsync();

            GotoList = new List<ListItem>();

            GotoList = ParseCode(codeBuffer);

            LoadList(GotoList);

            listView.ScrollIntoView(listView.Items[0]);

        }

        private async Task<string> GetCodeAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            try
            {
                DTE dte = Package.GetGlobalService(typeof(DTE)) as DTE;

                if (dte.ActiveDocument == null)
                    return String.Empty;

                TextDocument textDocument = (TextDocument)dte.ActiveDocument.Object("TextDocument");

                EditPoint editPoint = textDocument.StartPoint.CreateEditPoint();

                return editPoint.GetText(textDocument.EndPoint) ?? string.Empty;
            }
            catch (Exception)
            {
                // Ignore
            }

            return string.Empty;
        }
        private List<ListItem> ParseCode(string buffer)
        {
            List<ListItem> codeList = new List<ListItem>();

            CodeParser parser = new CodeParser();

            codeList = parser.ParseAllText(buffer);
 
            return codeList;
        }

        private void LoadList(List<ListItem> list)
        {
            listView.BeginInit();
            listView.ItemsSource = list;
            listView.EndInit();

            CollectionViewSource.GetDefaultView(listView.ItemsSource).Filter = ListViewFilter;

        }

        private bool ListViewFilter(object item)
        {
            if (String.IsNullOrEmpty(textBoxFilter.Text))
                return true;
            else
                return ((item as ListItem).Text.IndexOf(textBoxFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        private async Task GotoLineAsync(int LineNumber)
        {
            await DoGotoAsync(LineNumber);
        }
        private async Task<string> DoGotoAsync(int LineNumber)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            try
            {
                //activate current document

                if (dte.ActiveDocument == null)
                    return String.Empty;
                else
                    dte.ActiveDocument.Activate();

                //goto line number
                DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();

                IWpfTextView textView = docView?.TextView;
                if (textView == null) return String.Empty;

                textView.Selection.Clear();

                ITextSnapshotLine line = textView.TextSnapshot.GetLineFromLineNumber(LineNumber);

                textView.Caret.MoveTo(line.Start);

                textView.ViewScroller.EnsureSpanVisible(
                    new SnapshotSpan(textView.Caret.Position.BufferPosition, 0));

            }
            catch (Exception)
            {
                // Ignore
            }

            return string.Empty;
        }

        private void ButtonRefresh_OnClick(object sender, RoutedEventArgs e)
             => UpdateListAsync().FireAndForget();
    }
}