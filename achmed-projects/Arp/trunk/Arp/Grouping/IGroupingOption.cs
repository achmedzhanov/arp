using System.Collections.Generic;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace Arp.Grouping
{
    public interface IGroupingOption
    {
        bool IsAccept(ITypeMemberDeclaration memberDeclaration);
        void Execute();
    }
}