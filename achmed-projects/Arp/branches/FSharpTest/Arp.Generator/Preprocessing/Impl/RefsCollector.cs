using System.Collections.Generic;
using Arp.Common.Utils;

namespace Arp.Generator.Preprocessing.Impl
{
    public class InfoCollector<T, GH> : IInfoRef<ICollection<T>>
        where GH : IInfoRef<ICollection<T>>
    {
        private readonly ICollection<T> infos;
        private readonly ICollection<IInfoRef<GH>> collectionsRefs;

        private List<T> collected = null;


        public InfoCollector(ICollection<T> infos, ICollection<IInfoRef<GH>> collectionsRefs)
        {
            this.infos = infos;
            this.collectionsRefs = collectionsRefs;
        }

        #region IInfoRef<ICollection<T>> Members

        public ICollection<T> Get()
        {
            if(collected == null)
            {
                collected = new List<T>();
                collected.AddRange(infos);
                foreach (IInfoRef<GH> hRef in collectionsRefs)
                {
                    collected.AddRange(hRef.Get().Get());
                }
            }
            return collected;
        }

        #endregion
    }
}