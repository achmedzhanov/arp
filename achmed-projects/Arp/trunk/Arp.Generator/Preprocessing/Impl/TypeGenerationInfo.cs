using System.Collections.Generic;

namespace Arp.Generator.Preprocessing.Impl
{
    public class TypeGenerationInfo
    {
        private readonly TypeName typeName;
        private readonly List<AttributeGenerationInfo> attributes = new List<AttributeGenerationInfo>();
        private readonly List<IInfoRef<AttributesGroupGenerationInfo>> attributeGroups = new List<IInfoRef<AttributesGroupGenerationInfo>>();
        
        private readonly List<NestedElementGenerationInfo> flatNestedElements = new List<NestedElementGenerationInfo>();
        private readonly List<IInfoRef<GroupGenerationInfo>> groups = new List<IInfoRef<GroupGenerationInfo>>();


        public TypeGenerationInfo(TypeName typeName)
        {
            this.typeName = typeName;
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
    }
}