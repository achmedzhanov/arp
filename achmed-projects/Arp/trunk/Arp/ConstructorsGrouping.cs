using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Shell;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using Arp.Assertions;

namespace Arp
{
    public class ConstructorsGrouping : IGroupingOption
    {
        private ITypeDeclaration declaration;
        private List<IConstructorDeclaration> constructorDeclarations = new List<IConstructorDeclaration>();

        public ConstructorsGrouping(ITypeDeclaration declaration)
        {
            if (declaration == null)
                throw new ArgumentNullException("declaration");

            this.declaration = declaration;
        }

        public bool IsAccept(ITypeMemberDeclaration memberDeclaration)
        {
            IConstructorDeclaration constructorDesclaration = memberDeclaration as IConstructorDeclaration;
            if (constructorDesclaration == null)
                return false;
            constructorDeclarations.Add(constructorDesclaration);
            return true;
        }

        public void Execute()
        {
            if (constructorDeclarations.Count == 0)
                return;


            IClassBodyNode bodyNode = ((IClassLikeDeclarationNode)declaration.ToTreeNode()).Body;
            //

            using (WriteLockCookie cookie = WriteLockCookie.Create())
            {
                ITreeNode after = null;
                
                using (WriteLockCookie cookie1 = WriteLockCookie.Create())
                {
                    IStartRegionNode start = CreateStartRegion("Constructors");
                    Assert.CheckNotNull(start);
                    after = ModificationUtil.AddChildAfter<ITreeNode>(bodyNode.FirstChild, start);
                }

                foreach (IConstructorDeclaration constructorDeclaration in constructorDeclarations)
                {
                    IConstructorDeclarationNode cdn = constructorDeclaration.ToTreeNode();
                    ModificationUtil.DeleteChild(cdn);
                    after = ModificationUtil.AddChildAfter<ITreeNode>(after, cdn);
                }

                using (WriteLockCookie cookie2 = WriteLockCookie.Create())
                {
                    IEndRegionNode end = CreateEndRegion();
                    Assert.CheckNotNull(end);
                    ModificationUtil.AddChildAfter<ITreeNode>(after, end);
                }

            }
        }

        protected IStartRegionNode CreateStartRegion(string regionType)
        {
            return CreateFromFile<IStartRegionNode>("#region " + regionType);
        }

        protected IEndRegionNode CreateEndRegion()
        {
            return CreateFromFile<IEndRegionNode>("#endregion ");
        }

        protected T CreateFromFile<T>(string text)
            where T : class
        {
            CSharpElementFactory factory = CSharpElementFactory.GetInstance(declaration.GetManager().Solution);
            T createdNode = (T)factory.CreateFile(text, new object[0]).ToTreeNode().FindNextNode(
                delegate(ITreeNode treeNode)
                {
                    return (treeNode as T) != null ?
                        TreeNodeActionType.ACCEPT : TreeNodeActionType.CONTINUE;
                });

            return createdNode;            
        }
    }
}