using System.Collections.Generic;
using Arp.Assertions;
using JetBrains.ActionManagement;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeView.Occurences;
using JetBrains.ReSharper.CodeView.Search;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Shell;
using JetBrains.Shell.Progress;
using JetBrains.UI.Shell.Progress;
using JetBrains.Util;

namespace Arp.Serialization
{
    [ActionHandler("Arp.Serialization.MarkSerializableDerivedTypes")]
    public class MarkSerializableDerivedTypes : IActionHandler
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
            IClass @class =
                context.GetData<IDeclaredElement>(JetBrains.ReSharper.DataConstants.DECLARED_ELEMENT) as IClass;
            return @class != null && ExistsSerializableAttribute(@class);
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
            IClass @class =
                context.GetData<IDeclaredElement>(JetBrains.ReSharper.DataConstants.DECLARED_ELEMENT) as IClass;
            ISolution solution = @class.GetManager().Solution;
            @class.GetManager().CommitAllDocuments();
            Assert.CheckNotNull(@class);
            using (WriteLockCookie cookie = WriteLockCookie.Create())
            {
                SearchInheritorsRequest request =
                    new SearchInheritorsRequest(@class, SearchScope.SOLUTION, @class.GetManager().Solution);
                bool canceled = false;
                ICollection<IOccurence> occurences = null;
                using (ProgressWindow taskExecutor = new ProgressWindow(true))
                {
                    taskExecutor.ExecuteTask(delegate(IProgressIndicator progress)
                                                 {
                                                     occurences = request.Search(progress);
                                                     return null;
                                                 }, "", out canceled);
                }

                if (canceled)
                    return;

                Assert.CheckNotNull(occurences);

                using (CommandCookie.Create("MarkSerializableDerivedTypes"))
                {
                    PsiManager.GetInstance(solution).DoTransaction(
                        delegate(object[] args)
                        {
                            foreach (IOccurence occurence in occurences)
                            {
                                DeclaredElementOccurence declaredElementOccurence = (DeclaredElementOccurence)occurence;
                                IClass inheritClass =
                                    declaredElementOccurence.OccurenceElement.GetValidDeclaredElement() as IClass;
                                if (inheritClass != null)
                                    AddAtribute(inheritClass);
                            }
                        }, new object[] { });                    
                }
           }
        }

        protected void AddAtribute(IClass @class)
        {
            if (ExistsSerializableAttribute(@class))
                return;

            IDeclaration declaration = @class.GetDeclarations()[0];
            IMetaInfoTargetDeclarationNode metaInfoTargetDeclarationNode =
                declaration.ToTreeNode() as IMetaInfoTargetDeclarationNode;
            Assert.CheckNotNull(metaInfoTargetDeclarationNode);
            CSharpElementFactory elementFactory = CSharpElementFactory.GetInstance(@class.GetManager().Solution);
            IAttribute serializableAttribute =
                elementFactory.CreateTypeMemberDeclaration("[Serializable]void Foo(){}").Attributes[0];
            metaInfoTargetDeclarationNode.AddAttributeAfter(serializableAttribute, null);
        }

        protected bool ExistsSerializableAttribute(IClass @class)
        {
            IList<IAttributeInstance> instances = @class.GetAttributeInstances(false);
            if (instances == null)
                return false;
            IList<IAttributeInstance> found = CollectionUtil.FindAll(instances, delegate(IAttributeInstance obj)
                                                                                    {
                                                                                        return
                                                                                            obj.AttributeType.GetCLRName
                                                                                                () ==
                                                                                            "System.SerializableAttribute";
                                                                                    });
            return found.Count > 0;
        }
    }
}