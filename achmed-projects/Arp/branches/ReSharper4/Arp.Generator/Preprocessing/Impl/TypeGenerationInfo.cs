using System.Collections.Generic;
using Arp.Common.Utils;

namespace Arp.Generator.Preprocessing.Impl
{
    public class TypeGenerationInfo : ITypeInfo
    {
        private readonly TypeName typeName;
        private readonly List<AttributeGenerationInfo> attributes = new List<AttributeGenerationInfo>();
        private readonly List<IInfoRef<AttributesGroupGenerationInfo>> attributeGroups = new List<IInfoRef<AttributesGroupGenerationInfo>>();
        
        private readonly List<NestedElementGenerationInfo> flatNestedElements = new List<NestedElementGenerationInfo>();
        private readonly List<IInfoRef<GroupGenerationInfo>> groups = new List<IInfoRef<GroupGenerationInfo>>();
        private string baseName;

        new InfoCollector<NestedElementGenerationInfo, GroupGenerationInfo> nestedElementsCollector = null;
        new InfoCollector<AttributeGenerationInfo, AttributesGroupGenerationInfo> attributesCollector = null;

        public TypeGenerationInfo(TypeName typeName, string baseName)
        {
            this.typeName = typeName;
            this.baseName = baseName;

            nestedElementsCollector = new InfoCollector<NestedElementGenerationInfo, GroupGenerationInfo>(flatNestedElements, groups);

            attributesCollector = new InfoCollector<AttributeGenerationInfo, AttributesGroupGenerationInfo>(attributes, attributeGroups);

        }


        public TypeName TypeName
        {
            get { return typeName; }
        }

        public List<AttributeGenerationInfo> Attributes
        {
            get { return attributes; }
        }

        public List<NestedElementGenerationInfo> FlatNestedElements
        {
            get { return flatNestedElements; }
        }

        public List<IInfoRef<AttributesGroupGenerationInfo>> AttributeGroups
        {
            get { return attributeGroups; }
        }

        public List<IInfoRef<GroupGenerationInfo>> Groups
        {
            get { return groups; }
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
            return string.Format(GetType() + " [" + TypeName + "]");
        }

        #region ITypeInfo Members

        public string BaseName
        {
            get
            {
                return baseName;
            }
        }

        public ICollection<IAttributeInfo> AttributesInfo
        {
            get
            {
                return CollectionsUtils.ImplicitCast<AttributeGenerationInfo, IAttributeInfo>(attributesCollector.Get());
            }
        }

        public ICollection<INestedElementInfo> NestedElementsInfo
        {
            get
            {
                return CollectionsUtils.ImplicitCast<NestedElementGenerationInfo, INestedElementInfo>(nestedElementsCollector.Get());
            }
        }

        #endregion
    }
}