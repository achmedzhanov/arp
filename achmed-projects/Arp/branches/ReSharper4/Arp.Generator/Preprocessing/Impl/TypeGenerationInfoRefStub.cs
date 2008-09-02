using System;
using Arp.Generator.Preprocessing.Impl;

namespace Arp.Generator.Preprocessing.Impl
{
    public class TypeGenerationInfoRefStub : IInfoRef<TypeGenerationInfo>
    {
        private readonly TypeGenerationInfo typeGenerationInfo;

        public TypeGenerationInfoRefStub(TypeGenerationInfo typeGenerationInfo)
        {
            if (typeGenerationInfo == null) throw new ArgumentNullException("typeGenerationInfo");
            this.typeGenerationInfo = typeGenerationInfo;
        }

        #region ITypeGenerationInfoRef Members

        public TypeGenerationInfo Get()
        {
            return typeGenerationInfo;
        }

        #endregion

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
            return typeGenerationInfo.ToString();
        }
    }
}