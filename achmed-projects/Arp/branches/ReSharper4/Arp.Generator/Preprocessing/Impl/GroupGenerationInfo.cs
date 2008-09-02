using System;
using System.Collections.Generic;
using Arp.Common.Utils;

namespace Arp.Generator.Preprocessing.Impl
{
    public class GroupGenerationInfo : IInfoRef<ICollection<NestedElementGenerationInfo>>
    {
        private readonly string name;
        private readonly List<NestedElementGenerationInfo> flatNestedElements = new List<NestedElementGenerationInfo>();


        public GroupGenerationInfo(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            this.name = name;
        }


        public string Name
        {
            get { return name; }
        }

        public List<NestedElementGenerationInfo> FlatNestedElements
        {
            get { return flatNestedElements; }
        }

        #region IInfoRef<ICollection<INestedElementInfo>> Members

        public ICollection<NestedElementGenerationInfo> Get()
        {
            return FlatNestedElements;
        }

        #endregion
    }
}