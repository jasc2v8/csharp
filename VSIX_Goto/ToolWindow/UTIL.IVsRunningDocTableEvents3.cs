//https://stackoverflow.com/questions/41620241/can-i-detect-document-saved-not-changed-with-visual-studio-workspace-in-a-vsix

using Community.VisualStudio.Toolkit;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

namespace UTIL
{
public class RunningDocTableEvents : IVsRunningDocTableEvents3
{
        #region Members

        // RDT
        uint rdtCookie;
        RunningDocumentTable rdt;

        private RunningDocumentTable mRunningDocumentTable;
        private DTE mDte;

        public delegate void OnBeforeSaveHandler(object sender, Document document);
        public event OnBeforeSaveHandler BeforeSave;

        #endregion

        #region Constructor

        public RunningDocTableEvents()
        {
            // Create a selection container for tracking selected RDT events.
            //selectionContainer = new MsVsShell.SelectionContainer();

            // Advise the RDT of this event sink.
            IOleServiceProvider sp =
                Package.GetGlobalService(typeof(IOleServiceProvider)) as IOleServiceProvider;
            if (sp == null) return;

            rdt = new RunningDocumentTable(new ServiceProvider(sp));
            if (rdt == null) return;
        }
        public RunningDocTableEvents(Package aPackage)
        {
            mDte = (DTE)Package.GetGlobalService(typeof(DTE));
            mRunningDocumentTable = new RunningDocumentTable(aPackage);
            mRunningDocumentTable.Advise(this);
        }

        #endregion

        #region IVsRunningDocTableEvents3 implementation

        public int OnAfterAttributeChange(uint docCookie, uint grfAttribs)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterAttributeChangeEx(uint docCookie, uint grfAttribs, IVsHierarchy pHierOld, uint itemidOld, string pszMkDocumentOld, IVsHierarchy pHierNew, uint itemidNew, string pszMkDocumentNew)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterDocumentWindowHide(uint docCookie, IVsWindowFrame pFrame)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterFirstDocumentLock(uint docCookie, uint dwRDTLockType, uint dwReadLocksRemaining, uint dwEditLocksRemaining)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterSave(uint docCookie)
        {
            RunningDocumentInfo rdi;
            rdi = rdt.GetDocumentInfo(docCookie);
            string moniker = rdi.Moniker;

            VS.MessageBox.Show("DocumentSaved inside CLASS: " + moniker);

            //UpdateListAsync().FireAndForget();
            return VSConstants.S_OK;
        }

        public int OnBeforeDocumentWindowShow(uint docCookie, int fFirstShow, IVsWindowFrame pFrame)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeLastDocumentUnlock(uint docCookie, uint dwRDTLockType, uint dwReadLocksRemaining, uint dwEditLocksRemaining)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeSave(uint docCookie)
        {
            if (null == BeforeSave)
                return VSConstants.S_OK;

            var document = FindDocumentByCookie(docCookie);
            if (null == document)
                return VSConstants.S_OK;

            BeforeSave(this, FindDocumentByCookie(docCookie));
            return VSConstants.S_OK;
        }

        #endregion

        #region Private Methods

        private Document FindDocumentByCookie(uint docCookie)
        {
            var documentInfo = mRunningDocumentTable.GetDocumentInfo(docCookie);
            return mDte.Documents.Cast<Document>().FirstOrDefault(doc => doc.FullName == documentInfo.Moniker);
        }
        #endregion
    }
}