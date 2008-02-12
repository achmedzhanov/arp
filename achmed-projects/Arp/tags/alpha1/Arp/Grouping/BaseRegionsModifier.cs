using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Shell;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using Arp.Assertions;

namespace Arp.Grouping
{
    public class BaseRegionsModifier
    {
        protected readonly ITypeDeclaration declaration;

        public BaseRegionsModifier(ITypeDeclaration declaration)
        {
            if (declaration == null)
                throw new ArgumentNullException("declaration");

            this.declaration = declaration;
        }
        
        protected ITreeNode BodyFirstChild
        {
            get
            {
                IClassBodyNode body = ((IClassLikeDeclarationNode)declaration.ToTreeNode()).Body;
                return body.FirstChild;
            }
        }

        protected IStartRegionNode FindRegion(string regionType)
        {
            IClassBodyNode body = ((IClassLikeDeclarationNode)declaration.ToTreeNode()).Body;
            IStartRegionNode startRegionNode = (IStartRegionNode)body.FindNextNode(
                                                                     delegate(ITreeNode treeNode)
                                                                         {
                                                                             IStartRegionNode node = treeNode as IStartRegionNode;
                                                                             if (node != null && node.Name == regionType)
                                                                                 return TreeNodeActionType.ACCEPT;
                                                                             else
                                                                                 return TreeNodeActionType.CONTINUE;
                                                                         });
            return startRegionNode;
        }

        protected IStartRegionNode FindOrCreateRegion(string regionType)
        {
            IStartRegionNode startRegionNode = FindRegion(regionType);
            
            if(startRegionNode == null)
            {
                startRegionNode = CreateStartRegion(regionType);
                startRegionNode = (IStartRegionNode)ModificationUtil.AddChildAfter<ITreeNode>(BodyFirstChild, startRegionNode);
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