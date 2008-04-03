namespace Arp.Generator.Prototypes.NH.Psi
{
    public class RecursiveNHVisitor : INHVisitor
    {
        #region INHVisitor Members

        public virtual void  Visit(IClassMappingElement element)
        {
            Visit(element.Id);
            foreach (IPropertyMappingElement propty in element.Propties)
            {
                Visit(propty);
            } 
        }

        public virtual void  Visit(IIdMappingElement element)
        {
            // do nothing
        }

        public virtual void Visit(IPropertyMappingElement element)
        {
            // do nothing
        }

        #endregion
    }
}