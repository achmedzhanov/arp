using System;
using System.Windows.Forms;
using Arp.Assertions;
using Arp.UnusedReferences;
using Arp.UnusedReferences.UI;
using JetBrains.ActionManagement;
using JetBrains.ProjectModel;
using JetBrains.ReSharper;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Refactorings.Conflicts;
using JetBrains.ReSharper.Refactorings.Workflow;
using JetBrains.Shell;
using JetBrains.Shell.Progress;
using JetBrains.UI.PopupWindowManager;
using JetBrains.UI.Shell;
using JetBrains.WindowManagement;

namespace Arp
{
    public class DeleteUnusedReferencesWorkflow : IRefactoringWorkflow
    {

        private PsiLanguageType psiLanguageType =  PsiLanguageType.UNKNOWN;
        private ISolution solution;
        private ICustomPage page = null;
        private IModuleReference[] referencesToDelete = null;
        private UnusedReferencesSearchResult searchResults = null;

        ///<summary>
        ///
        ///            Checks if the refactoring is available in given data context
        ///            
        ///</summary>
        ///
        public bool IsAvailable(IDataContext context)
        {
            ISolution solution = context.GetData<ISolution>(JetBrains.ReSharper.DataConstants.SOLUTION);
            if (solution != null)
                return true;
            else
                return false;
        }

        public IModuleReference[] RefrencesToDelete
        {
            set
            {
                if (value == null)
                    throw new ArgumentNullException();

                referencesToDelete = value;

            }
        }


        public UnusedReferencesSearchResult SearchResults
        {
            get { return searchResults; }
        }

        ///<summary>
        ///
        ///            Initializes workflow with specified data context
        ///            
        ///</summary>
        ///
        public bool Initialize(IDataContext context, IInitializationContext initializationContext)
        {
            solution = context.GetData<ISolution>(JetBrains.ReSharper.DataConstants.SOLUTION);

            psiLanguageType = context.GetData<PsiLanguageType>(DataConstants.PSI_LANGUAGE_TYPE);

            Assert.CheckNotNull(solution);

            bool emptyResult = false;

            UnusedReferencesSearchRequest request = new UnusedReferencesSearchRequest(solution);
            UnusedReferencesSearchDescriptor searchDescriptor = new UnusedReferencesSearchDescriptor(request);
            using(ReadLockCookie cookie = ReadLockCookie.Create())
            {
                searchDescriptor.Search();
            }
            
            emptyResult = request.EmptyResult;
            searchResults = request.Results;
            if (searchDescriptor.Items == null)
                return false;

            if (emptyResult)
            {
                EmptyResultsPane panel = new EmptyResultsPane();
                page = panel;
            }
            else
            {
                ModulesChooserPane pane = new ModulesChooserPane(this);
                pane.AutoActivate = true;
                page = pane;
            }

            return true;
        }

        ///<summary>
        ///
        ///            Performs pre-execute steps
        ///            
        ///</summary>
        ///
        public bool PreExecute(TaskExecutorCreator taskExecutorCreator)
        {
            
            return true;
        }

        ///<summary>
        ///
        ///            Rolls back pre-execute steps
        ///            
        ///</summary>
        ///
        public void RollbackPreExecute(TaskExecutorCreator taskExecutorCreator)
        {
            // do nothing
        }

        ///<summary>
        ///
        ///            Executes refactoring
        ///            
        ///</summary>
        ///
        public void Execute(IProgressIndicator progressIndicator)
        {
            // do nothing
            Assert.CheckNotNull(referencesToDelete);

            using(WriteLockCookie cookie = WriteLockCookie.Create(true))
            {
                foreach (IModuleReference moduleReference in referencesToDelete)
                {
                    IProject project = moduleReference.GetProject();
                    // TODO dispatch RemoveModuleReference|RemoveProjectReference ?
                    project.RemoveModuleReference(moduleReference);
                }            
            }
        }

        ///<summary>
        ///
        ///            Performs post-execute steps
        ///            
        ///</summary>
        ///
        public void PostExecute(TaskExecutorCreator taskExecutorCreator)
        {
            // do nothing
        }

        ///<summary>
        ///
        ///            Updates all neccessary data. This might change list of pending UI pages.
        ///            
        ///</summary>
        ///
        public bool RecoverAfterExternalChanges(IProgressIndicator progressIndicator)
        {
            // do nothing
            return true;
        }

        ///<summary>
        ///
        ///            Request to check all internal structures to make sure everything is fine after user edited text
        ///            
        ///</summary>
        ///
        public bool CheckInternalState(IProgressIndicator progressIndicator)
        {
            progressIndicator.Start(1);
            progressIndicator.Stop();
            return true;
        }

        ///<summary>
        ///
        ///            Gets refactoring title
        ///            
        ///</summary>
        ///
        public string Title
        {
            get
            {
                // todo user resources!
                return "Delete Unused References";
            }
        }

        ///<summary>
        ///
        ///            Gets conflict searcher
        ///            
        ///</summary>
        ///
        public IConflictSearcher ConflictSearcher
        {
            get
            {
                return null;
            }
        }

        ///<summary>
        ///
        ///            Gets first pending custom page
        ///            
        ///</summary>
        ///
        public ICustomPage FirstPendingCustomPage
        {
            get
            {
                return page;
            }
        }

        ///<summary>
        ///
        ///            Gets flag indicating that this refactoring might modify many documents
        ///            
        ///</summary>
        ///
        public bool MightModifyManyDocuments
        {
            get
            {
                return true;
            }
        }

        ///<summary>
        ///
        ///            The langugage that the refactoring have been invoked in.
        ///            
        ///</summary>
        ///
        public PsiLanguageType InvocationLanguage
        {
            get
            {
                return psiLanguageType;
            }
        }
    }
}