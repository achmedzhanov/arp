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
    public abstract class BaseGrouping : IGroupingOption
    {
        protected readonly ITypeDeclaration declaration;

        public BaseGrouping(ITypeDeclaration declaration)
        {
            if (declaration == null)
                throw new ArgumentNullException("declaration");

            this.declaration = declaration;
        }


        public abstract bool IsAccept(ITypeMemberDeclaration memberDeclaration);


        public abstract void Execute();


        protected IStartRegionNode FindOrCreateRegion(string regionType)
        {
            IClassBodyNode body = ((IClassLikeDeclarationNode)declaration.ToTreeNode()).Body;
            IStartRegionNode startRegionNode = (IStartRegionNode)body.FindNextNode(                
                delegate(ITreeNode treeNode)
                    {
                        IStartRegionNode node = treeNode as IStartRegionNode;
                        if(node != null && node.Name == regionType)
                            return TreeNodeActionType.ACCEPT;
                        else
                            return TreeNodeActionType.CONTINUE;
                    });

            
            if(startRegionNode == null)
            {
                startRegionNode = CreateStartRegion(regionType);
                startRegionNode = (IStartRegionNode)ModificationUtil.AddChildAfter<ITreeNode>(body.FirstChild, startRegionNode);
            }

            if(startRegionNode.EndRegion == null)
            {
                using (WriteLockCookie cookie = WriteLockCookie.Create())
                {
                    IEndRegionNode end = CreateEndRegion();
                    Assert.CheckNotNull(end);
                    ModificationUtil.AddChildAfter<ITreeNode>(startRegionNode, end);
                }                    
            }

            Assert.CheckNotNull(startRegionNode.EndRegion);

            return startRegionNode;
           
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