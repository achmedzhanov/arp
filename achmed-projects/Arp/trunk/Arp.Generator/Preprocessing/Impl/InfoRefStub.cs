using System;

namespace Arp.Generator.Preprocessing.Impl
{
    public class InfoRefStub<T> : IInfoRef<T>
    {
        private readonly T info;

        public InfoRefStub(T typeGenerationInfo)
        {
            if (typeGenerationInfo == null) 
                throw new ArgumentNullException("typeGenerationInfo");
            this.info = typeGenerationInfo;
        }

        #region InfoRefStub<T> Members

        public T Get()
        {
            return info;
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
            return info.ToString();
        }
    }
}