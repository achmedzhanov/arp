using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace Arp.log4net.Psi.Tree
{
    public interface IParameterDescriptor
    {
        string Name { get;}
        IType Type { get;}
        IDeclaredElement DecraedElement { get;}
    }
}