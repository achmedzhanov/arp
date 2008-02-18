using System.Collections.Generic;
using JetBrains.ReSharper.Psi.Tree;

namespace Arp.Generator.Prototypes.NH.Psi
{
    public interface IClassMappingElement : IElement
    {
        IIdMappingElement Id { get;}
        IList<IPropertyMappingElement> Propties { get;}
        IBooleanAttribute Lazy { get;}
        IRefenceName Name { get;}
    }
}