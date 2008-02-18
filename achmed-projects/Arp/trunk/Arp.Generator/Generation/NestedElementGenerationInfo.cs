using System;
using System.Collections.Generic;
using Arp.Common.Assertions;

namespace Arp.Generator.Generation
{
    public class NestedElementGenerationInfo
    {
        private readonly bool isCollection;
        private readonly string xmlName;
        private readonly IElementGenerationInfoRef type;
        private readonly ICollection<string> xmlNames;
        private readonly ICollection<IElementGenerationInfoRef> collectionTypes;
        


        public NestedElementGenerationInfo(IElementGenerationInfoRef type, string xmlName)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (xmlName == null) throw new ArgumentNullException("xmlName");
            isCollection = false;
            this.type = type;
            this.xmlName = xmlName;
        }


        public NestedElementGenerationInfo(ICollection<IElementGenerationInfoRef> collectionTypes, ICollection<string> xmlNames)
        {
            if (collectionTypes == null) throw new ArgumentNullException("collectionTypes");
            if (xmlNames == null) throw new ArgumentNullException("xmlNames");
            isCollection = true;
            this.collectionTypes = collectionTypes;
            this.xmlNames = xmlNames;
            Assert.Check(xmlNames.Count == collectionTypes.Count);
        }


        public bool IsCollection
        {
            get { return isCollection; }
        }

        public IElementGenerationInfoRef Type
        {
            get
            {
                if (IsCollection)
                    throw new InvalidOperationException();

                return type;
            }
        }

        public ICollection<IElementGenerationInfoRef> CollectionTypes
        {
            get
            {
                if (!IsCollection)
                    throw new InvalidOperationException();                
                
                return collectionTypes;
            }
        }


        public string XmlName
        {
            get
            {
                if (IsCollection)
                    throw new InvalidOperationException();                
                return xmlName;
            }
        }

        public ICollection<string> XmlNames
        {
            get
            {
                if (!IsCollection)
                    throw new InvalidOperationException();                
                return xmlNames;
            }
        }
    }
}