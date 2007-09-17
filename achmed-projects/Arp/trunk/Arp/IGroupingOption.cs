using JetBrains.ReSharper.Psi.Tree;

namespace Arp
{
    public interface IGroupingOption
    {
        bool IsAccept(ITypeMemberDeclaration memberDeclaration);
        void Execute();
    }
}