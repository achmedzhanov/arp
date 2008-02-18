using System.Collections.Generic;

namespace Arp.Generator.Generation
{
    public class ElementGenerationInfo
    {
        private string xmlName;
        private TypeName typeName;
        private ICollection<AttributeGenerationInfo> attributes;
        private ICollection<NestedElementGenerationInfo> flatNestedElements;


        public ElementGenerationInfo(string xmlName, TypeName typeName, ICollection<AttributeGenerationInfo> attributes, ICollection<NestedElementGenerationInfo> flatNestedElements)
        {
            this.xmlName = xmlName;
            this.typeName = typeName;
            this.attributes = attributes;
            this.flatNestedElements = flatNestedElements;
        }

        public ICollection<AttributeGenerationInfo> Attributes
        {
            get { return attributes; }
        }
                

        public ICollection<NestedElementGenerationInfo> FlatNestedElements
        {
            get { return flatNestedElements; }
        }


        public string XmlName
        {
            get { return xmlName; }
        }

        public TypeName TypeName
        {
            get { return typeName; }
        }
    }
}