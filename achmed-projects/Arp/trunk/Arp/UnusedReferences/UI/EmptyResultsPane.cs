using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using JetBrains.ReSharper.Refactorings.Workflow;
using JetBrains.Shell.Progress;
using JetBrains.UI.Shell;

namespace Arp.UnusedReferences.UI
{
    public partial class EmptyResultsPane : UserControl, ICustomPage
    {
        public EmptyResultsPane()
        {
            InitializeComponent();
        }


        ///<summary>
        ///
        ///            Event which is raised when the <see cref="P:JetBrains.ReSharper.Refactorings.Workflow.ICustomPage.ContinueEnabled" /> flag changes value
        ///            
        ///</summary>
        ///
        public event EventHandler ContinueEnabledChanged;

        ///<summary>
        ///
        ///            Initializes page. Called before each show of the page which is caused by "Continue" button.
        ///            
        ///</summary>
        ///
        public bool Initialize(IProgressIndicator progressIndicator)
        {
            WindowUtil.ShowMessageBox("Unsued references not found",
                                          UIApplicationShell.Instance.Descriptor.ProductName, MessageBoxButtons.OK,
                                          MessageBoxIcon.Asterisk);            

            return false;
        }

        ///<summary>
        ///
        ///            Used for handling probable modal stage of confirming that the dialog really needs to be shown
        ///            
        ///</summary>
        ///
        public bool ConfirmBeforeShow()
        {
            return false;
        }

        ///<summary>
        ///
        ///            Refreshes internal page pointers. Returns 
        ///<c>false</c> if refresh is unsuccessfull and refactoring should be canceled.
        ///            
        ///</summary>
        ///
        public bool RefreshContents(IProgressIndicator progressIndicator)
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///
        ///            Commits page
        ///            
        ///</summary>
        ///
        ///<returns>
        ///Next page if any
        ///</returns>
        ///
        public ICustomPage Commit(IProgressIndicator progressIndicator, CustomPageCommitFlags flags)
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///
        ///            Gets page title
        ///            
        ///</summary>
        ///
        public string Title
        {
            get { return "#Title"; }
        }

        ///<summary>
        ///
        ///            Gets page description
        ///            
        ///</summary>
        ///
        public string Description
        {
            get { return "#Title"; }
        }

        ///<summary>
        ///
        ///            Gets UI control
        ///            
        ///</summary>
        ///
        public Control UI
        {
            get { return this; }
        }

        ///<summary>
        ///
        ///            Gets flag indicating whether the Continue button is enabled.
        ///            Note that this is ANDed with parent statuses, that include {not running workflow execution} and {validation passes}.
        ///            This means that you don't have to check the validator. If it's the only thing you can think of — just return 
        ///<c>True</c>.
        ///            
        ///</summary>
        ///
        public bool ContinueEnabled
        {
            get { return false; }
        }
    }
}
