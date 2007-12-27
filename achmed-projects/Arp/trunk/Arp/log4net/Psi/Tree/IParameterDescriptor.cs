using JetBrains.ReSharper.Psi;

namespace Arp.log4net.Psi.Tree
{
    public interface IParameterDescriptor
    {
        string Name { get;}
        IType Type { get;}
    }
}