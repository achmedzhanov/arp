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


        public TypeGenerationInfo(TypeName typeName, string baseName)
        {
            this.typeName = typeName;
            this.baseName = baseName;
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
                // TODO collect attributes from groups
                return CollectionsUtils.Transform<AttributeGenerationInfo, IAttributeInfo>(this.attributes,
                                                                                    delegate(
                                                                                        AttributeGenerationInfo source)
                                                                                        {
                                                                                            return
                                                                                                source;
                                                                                        });
            }
        }

        public ICollection<INestedElementInfo> NestedElementsInfo
        {
            get
            {
                // TODO collect elements from groups

                return CollectionsUtils.Transform<NestedElementGenerationInfo, INestedElementInfo>(this.flatNestedElements,
                                                                                                    delegate(
                                                                                                        NestedElementGenerationInfo source)
                                                                                                    {
                                                                                                        return
                                                                                                            source;
                                                                                                    });

            }
        }

        #endregion
    }
}