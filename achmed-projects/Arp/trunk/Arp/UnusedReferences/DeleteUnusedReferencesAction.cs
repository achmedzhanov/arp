using System.Threading;
using System.Windows.Forms;
using Arp.Assertions;
using Arp.UnusedReferences;
using Arp.UnusedReferences.UI;
using JetBrains.ActionManagement;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeView.Descriptors;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Refactorings.Workflow;
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
            return (new DeleteUnusedReferencesWorkflow()).IsAvailable(context);
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
            
            RefactoringActionUtil.ExecuteRefactoring(context, new DeleteUnusedReferencesWorkflow());
            
        }
    }
}