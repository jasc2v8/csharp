using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Shell;

namespace ToolWindow
{
    public class MyToolWindow : BaseToolWindow<MyToolWindow>
    {
        public override string GetTitle(int toolWindowId) => "GOTO";
        public override Type PaneType => typeof(Pane);

        public override async Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {
            Version vsVersion = await VS.Shell.GetVsVersionAsync();
            return new MyToolWindowControl(vsVersion);
        }

        [Guid("7D39B69B-1E83-4FAD-8A2E-6A0F78E4B2CC")]
        public class Pane : ToolWindowPane
        {
            public Pane()
            {
                //BitmapImageMoniker = KnownMonikers.ToolWindow;
                BitmapImageMoniker = KnownMonikers.GoToSourceCode;
            }
        }
    }
}