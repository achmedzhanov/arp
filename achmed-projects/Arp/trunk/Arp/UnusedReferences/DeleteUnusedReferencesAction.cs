using System.Windows.Forms;
using JetBrains.ActionManagement;
using JetBrains.ProjectModel;
using JetBrains.Shell;
using JetBrains.UI;
using JetBrains.UI.Shell;

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

//            
//            
//            IProjectModelElement modelElement = context.GetData<IProjectModelElement>(JetBrains.ReSharper.DataConstants.PROJECT_MODEL_ELEMENT);
//
//            IModuleReference reference = modelElement as IModuleReference;
//            if (reference != null)
//            {
//                return (reference.ResolveReferencedModule() != null);
//            }
//            return false;
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
            
 
            WindowUtil.ShowMessageBox("Unsued references not found", UIApplicationShell.Instance.Descriptor.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}