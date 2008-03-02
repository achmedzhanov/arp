using System;
using System.Collections.Generic;

namespace Arp.Generator.Preprocessing.Impl
{
    public class GroupGenerationInfo
    {
        private string name;
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



    }
}