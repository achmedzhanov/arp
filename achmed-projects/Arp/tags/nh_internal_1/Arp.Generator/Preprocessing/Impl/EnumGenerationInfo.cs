using System;
using System.Collections.Generic;
using Arp.Common.Assertions;

namespace Arp.Generator.Preprocessing.Impl
{
    public class EnumGenerationInfo
    {
        private readonly TypeName typeName;
        private readonly IDictionary<string, string> namesValues;
        private readonly IDictionary<string, string> namesXmlValues;

        public EnumGenerationInfo(TypeName typeName, IDictionary<string, string> namesValues, IDictionary<string, string> namesXmlValues)
        {
            if (typeName == null) throw new ArgumentNullException("typeName");
            if (namesValues == null) throw new ArgumentNullException("namesValues");
            if (namesXmlValues == null) throw new ArgumentNullException("namesXmlValues");
            
            this.typeName = typeName;
            this.namesValues = namesValues;
            this.namesXmlValues = namesXmlValues;

            Assert.Check(namesValues.Count == namesXmlValues.Count);

        }

        public TypeName TypeName
        {
            get { return typeName; }
        }

        public IDictionary<string, string> NamesValues
        {
            get { return namesValues; }
        }


        public IDictionary<string, string> NamesXmlValues
        {
            get { return namesXmlValues; }
        }
    }
}