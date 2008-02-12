using System;
using System.Collections.Generic;
using Arp.Assertions;
using Arp.Grouping;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Shell;

namespace Arp.Grouping
{
    public class ReorderRegions : BaseRegionsModifier
    {
        private readonly ICollection<string> regionTypesOrder;
        
        public ReorderRegions(ITypeDeclaration declaration, ICollection<string> regionTypesOrder) : base(declaration)
        {
            if (regionTypesOrder == null)
                throw new ArgumentNullException("regionTypesOrder");

            this.regionTypesOrder = regionTypesOrder;
        }

        public void Reorder()
        {
            Assert.CheckNotNull(regionTypesOrder);

            if (regionTypesOrder.Count == 0)
                return;

            ITreeNode previous = BodyFirstChild;

            foreach (string regionType in regionTypesOrder)
            {
                
                IStartRegionNode regionNode = FindRegion(regionType);
                if(regionNode == null)
                    continue;

                previous = MoveRegionAfter(previous, regionNode).EndRegion;
            }
        }

        protected IStartRegionNode MoveRegionAfter(ITreeNode after, IStartRegionNode startRegion)
        {
            Assert.CheckNotNull(startRegion.EndRegion);
            
            using (WriteLockCookie cookie = WriteLockCookie.Create())
            {
                IEndRegionNode endRegion = startRegion.EndRegion;

                List<ITreeNode> regionNodes = new List<ITreeNode>();
                ITreeNode current = startRegion;
                while (current.NextSibling != endRegion)
                {
                    regionNodes.Add(current.NextSibling);
                    current = current.NextSibling;
                }


                List<ITreeNode> nodesToMove = new List<ITreeNode>(regionNodes);
//                nodesToMove.Insert(0, startRegion);
//                nodesToMove.Add(endRegion);


                
                startRegion = MoveNodeAfter(after, startRegion);
                endRegion = MoveNodeAfter(startRegion, endRegion);

                ITreeNode previous = startRegion;

                foreach (ITreeNode nodetoMove in nodesToMove)
                {
                    // fix for nullref exception, crush on IWhitespaceNode
                    // check for ITypeMemberDeclaration ?
                    if (nodetoMove is IWhitespaceNode || nodetoMove is IErrorElementNode)
                    {
                        //ModificationUtil.DeleteChild(nodetoMove);
                        continue;
                    }

                    previous = MoveNodeAfter(previous, nodetoMove);
                }

                return startRegion;
            }
        }

        protected T MoveNodeAfter<T>(ITreeNode after, T node)
            where T : ITreeNode
        {
            using (WriteLockCookie cookie = WriteLockCookie.Create())
            {
                T added = ModificationUtil.AddChildAfter(after, node);
                ModificationUtil.DeleteChild(node);
                return added;
            }
        }

    }
}