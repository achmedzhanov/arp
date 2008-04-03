namespace Arp.Generator.Prototypes.NH.Psi
{
    public interface INHVisitor
    {
        void Visit(IClassMappingElement element);
        void Visit(IIdMappingElement element);
        void Visit(IPropertyMappingElement element);
    }
}