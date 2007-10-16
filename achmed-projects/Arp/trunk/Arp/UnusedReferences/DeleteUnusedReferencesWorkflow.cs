using JetBrains.ActionManagement;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Refactorings.Conflicts;
using JetBrains.ReSharper.Refactorings.Workflow;
using JetBrains.Shell.Progress;

namespace Arp
{
    public class DeleteUnusedReferencesWorkflow : IRefactoringWorkflow
    {
        ///<summary>
        ///
        ///            Checks if the refactoring is available in given data context
        ///            
        ///</summary>
        ///
        public bool IsAvailable(IDataContext context)
        {
            throw new System.NotImplementedException();
        }

        ///<summary>
        ///
        ///            Initializes workflow with specified data context
        ///            
        ///</summary>
        ///
        public bool Initialize(IDataContext context, IInitializationContext initializationContext)
        {
            throw new System.NotImplementedException();
        }

        ///<summary>
        ///
        ///            Performs pre-execute steps
        ///            
        ///</summary>
        ///
        public bool PreExecute(TaskExecutorCreator taskExecutorCreator)
        {
            throw new System.NotImplementedException();
        }

        ///<summary>
        ///
        ///            Rolls back pre-execute steps
        ///            
        ///</summary>
        ///
        public void RollbackPreExecute(TaskExecutorCreator taskExecutorCreator)
        {
            throw new System.NotImplementedException();
        }

        ///<summary>
        ///
        ///            Executes refactoring
        ///            
        ///</summary>
        ///
        public void Execute(IProgressIndicator progressIndicator)
        {
            throw new System.NotImplementedException();
        }

        ///<summary>
        ///
        ///            Performs post-execute steps
        ///            
        ///</summary>
        ///
        public void PostExecute(TaskExecutorCreator taskExecutorCreator)
        {
            throw new System.NotImplementedException();
        }

        ///<summary>
        ///
        ///            Updates all neccessary data. This might change list of pending UI pages.
        ///            
        ///</summary>
        ///
        public bool RecoverAfterExternalChanges(IProgressIndicator progressIndicator)
        {
            throw new System.NotImplementedException();
        }

        ///<summary>
        ///
        ///            Request to check all internal structures to make sure everything is fine after user edited text
        ///            
        ///</summary>
        ///
        public bool CheckInternalState(IProgressIndicator progressIndicator)
        {
            throw new System.NotImplementedException();
        }

        ///<summary>
        ///
        ///            Gets refactoring title
        ///            
        ///</summary>
        ///
        public string Title
        {
            get { throw new System.NotImplementedException(); }
        }

        ///<summary>
        ///
        ///            Gets conflict searcher
        ///            
        ///</summary>
        ///
        public IConflictSearcher ConflictSearcher
        {
            get { throw new System.NotImplementedException(); }
        }

        ///<summary>
        ///
        ///            Gets first pending custom page
        ///            
        ///</summary>
        ///
        public ICustomPage FirstPendingCustomPage
        {
            get { throw new System.NotImplementedException(); }
        }

        ///<summary>
        ///
        ///            Gets flag indicating that this refactoring might modify many documents
        ///            
        ///</summary>
        ///
        public bool MightModifyManyDocuments
        {
            get { throw new System.NotImplementedException(); }
        }

        ///<summary>
        ///
        ///            The langugage that the refactoring have been invoked in.
        ///            
        ///</summary>
        ///
        public PsiLanguageType InvocationLanguage
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}