using System;
using System.Collections.Generic;
using Arp.Grouping;
using JetBrains.ReSharper.Psi.CSharp.Impl;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Shell;

namespace Arp.Grouping
{
    public class PredicateGrouping : BaseRegionsModifier, IGroupingOption
    {
        private List<ITypeMemberDeclaration> filedDeclarations = new List<ITypeMemberDeclaration>();
        private string regionType;
        private Predicate<ITypeMemberDeclaration> predicate;

        public PredicateGrouping(ITypeDeclaration declaration, string regionType, Predicate<ITypeMemberDeclaration> predicate)
            : base(declaration)
        {
            if (regionType == null)
                throw new ArgumentNullException("regionType");

            if(predicate == null)
                throw new ArgumentNullException("predicate");

            this.regionType = regionType;
            this.predicate = predicate;

        }

        public virtual bool IsAccept(ITypeMemberDeclaration memberDeclaration)
        {
            
            if(!predicate(memberDeclaration))
                return false;

            filedDeclarations.Add(memberDeclaration);
            return true;
        }

        public virtual void Execute()
        {
            if (filedDeclarations.Count == 0)
                return;

            using (WriteLockCookie cookie = WriteLockCookie.Create())
            {
                ITreeNode after = FindOrCreateRegion(regionType);

                foreach (ITypeMemberDeclaration typeMemberDeclaration in filedDeclarations)
                {
                    ITreeNode cdn = typeMemberDeclaration.ToTreeNode();

                    if (cdn is IMultipleDeclarationMemberNode)
                    {

                        MultipleDeclarationUtil.SplitDeclarationBefore((IMultipleDeclarationMemberNode)cdn);
                        cdn = 
                            ((IMultipleDeclarationMemberNode)cdn).MultipleDeclaration;
                    }

                    after = ModificationUtil.AddChildAfter<ITreeNode>(after, cdn);
                    ModificationUtil.DeleteChild(cdn);
                }
            }
        }
    }
}