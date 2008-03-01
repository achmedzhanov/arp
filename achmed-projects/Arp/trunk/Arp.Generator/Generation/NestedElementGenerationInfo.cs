using System;
using System.Collections.Generic;
using Arp.Common.Assertions;

namespace Arp.Generator.Generation
{
    public class NestedElementGenerationInfo
    {
        private readonly bool isCollection;
        private readonly ElementGenerationInfo element;
        private readonly ICollection<ElementGenerationInfo> elements;



        public NestedElementGenerationInfo(ElementGenerationInfo element)
        {
            if (element == null) throw new ArgumentNullException("element");
            isCollection = false;
            this.element = element;
        }


        public NestedElementGenerationInfo(ICollection<ElementGenerationInfo> elements)
        {
            if (elements == null) throw new ArgumentNullException("elements");
            this.elements = elements;
            isCollection = true;
        }

        public bool IsCollection
        {
            get { return isCollection; }
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
            if(isCollection)
            {
                return "[collection]";
            }
            else
            {
                return "[element" + element + "]";
            }
        }
    }
}