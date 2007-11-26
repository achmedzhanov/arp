using System.Threading;
using System.Windows.Forms;
using Arp.Assertions;
using Arp.UnusedReferences;
using Arp.UnusedReferences.UI;
using JetBrains.ActionManagement;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeView.Descriptors;
using JetBrains.ReSharper.Psi;
using JetBrains.Shell;
using JetBrains.UI;
using JetBrains.UI.PopupWindowManager;
using JetBrains.UI.Shell;
using JetBrains.WindowManagement;

namespace Arp
{
    [ActionHandler("Arp.DeleteUnusedReferencesAction")]
    public class DeleteUnusedReferencesAction : IActionHandler
    {
        ///<summary>
        ///
        ///            Updates action visual presentation. If presentation.Enabled is set to false, Execute
        ///            will not be called.
        ///            
        ///</summary>
        ///
        ///<param name="context">DataContext</param>
        ///<param name="presentation">presentation to update</param>
        ///<param name="nextUpdate">delegate to call</param>
        public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
        {
            ISolution solution = context.GetData<ISolution>(JetBrains.ReSharper.DataConstants.SOLUTION);
            if (solution != null)
                return true;
            else
                return false;
        }

        ///<summary>
        ///
        ///            Executes action. Called after Update, that set ActionPresentation.Enabled to true.
        ///            
        ///</summary>
        ///
        ///<param name="context">DataContext</param>
        ///<param name="nextExecute">delegate to call</param>
        public void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            ISolution solution = context.GetData<ISolution>(JetBrains.ReSharper.DataConstants.SOLUTION);
            Assert.CheckNotNull(solution);

            UnusedReferencesSearchResult results = null;
            bool emptyResult = false;

            UnusedReferencesSearchRequest request = new UnusedReferencesSearchRequest(solution);
            UnusedReferencesSearchDescriptor searchDescriptor = new UnusedReferencesSearchDescriptor(request);
            searchDescriptor.Search();
            emptyResult = request.EmptyResult;
            results = request.Results;

            if (searchDescriptor.Items == null)
                return;

            if (emptyResult)
                WindowUtil.ShowMessageBox("Unsued references not found",
                                          UIApplicationShell.Instance.Descriptor.ProductName, MessageBoxButtons.OK,
                                          MessageBoxIcon.Asterisk);
            else
            {
                ModulesChooserPanel panel = new ModulesChooserPanel(results);
                panel.AutoActivate = true;
                IPopupWindow window = PopupWindowManager.CreatePopupWindow(panel,
                                                                           DevenvLayouter.CreateCenteredCorner
                                                                               (), PopupWindowContext.Empty,
                                                                           ~HideFlags.None, true);
                window.ShowWindow();
            }
        }
    }
}