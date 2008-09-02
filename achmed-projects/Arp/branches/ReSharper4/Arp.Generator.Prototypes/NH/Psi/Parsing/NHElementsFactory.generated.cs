using Arp.Generator.Prototypes.NH.Psi.Impl;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.Generator.Prototypes.NH.Psi.Parsing
{
    public partial class NHElementsFactory
    {
        protected IXmlTag CreateTagGenerated(IXmlTagHeaderNode header, IXmlTagContainer parentTag)
        {

            string name = header.Name.GetText();

            if (name == "class")
            {
                return new ClassMappingElementImpl();
            }
            else
                return null;
        }        
    }
}