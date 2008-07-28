using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace Arp.log4net.Psi.Tree
{
    public interface IParameterDescriptor
    {
        string Name { get;}
        IType Type { get;}
        IDeclaredElement DecraedElement { get;}
        bool IsRequired { get;}
        string RequredBefore { get;}
        string RequredAfter { get;}
        bool IsAttribute { get;}
        // TODO top, bottom bounds
        string[] Conflicts { get;}

        bool IsEnumerable { get;}
        string [] PossibleValues { get;}

        bool RequredType { get; }
    }
}