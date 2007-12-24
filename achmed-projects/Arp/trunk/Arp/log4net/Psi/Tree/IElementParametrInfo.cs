using JetBrains.ReSharper.Psi;

namespace Arp.log4net.Psi.Tree
{
    public interface IElementParametrInfo
    {
        string Name { get;}
        IType Type { get;}
    }
}