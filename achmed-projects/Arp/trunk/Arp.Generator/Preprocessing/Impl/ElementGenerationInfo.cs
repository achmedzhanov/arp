using System;
using System.Collections.Generic;

namespace Arp.Generator.Preprocessing.Impl
{
    public class ElementGenerationInfo
    {
        private readonly string xmlName;
        private readonly ITypeGenerationInfoRef typeGenerationInfoRef;

        public ElementGenerationInfo(string xmlName, TypeGenerationInfo typeGenerationInfo)
            : this(xmlName, new TypeGenerationInfoRefStub(typeGenerationInfo))
        {
        }


        public ElementGenerationInfo(string xmlName, ITypeGenerationInfoRef typeGenerationInfoRef)
        {
            if (xmlName == null) throw new ArgumentNullException("xmlName");
            if (typeGenerationInfoRef == null) throw new ArgumentNullException("typeGenerationInfoRef");
            this.xmlName = xmlName;
            this.typeGenerationInfoRef = typeGenerationInfoRef;
        }

        public string XmlName
        {
            get { return xmlName; }
        }

        public TypeGenerationInfo TypeGenerationInfo
        {
            get { return typeGenerationInfoRef.Get(); }
        }

        ///<summary>
        ///Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format(GetType() + " [" + typeGenerationInfoRef + "]");
        }
    }
}