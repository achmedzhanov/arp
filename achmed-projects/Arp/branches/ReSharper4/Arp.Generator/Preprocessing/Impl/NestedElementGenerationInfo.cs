using System;
using System.Collections.Generic;
using Arp.Common.Assertions;
using Arp.Common.Utils;
using Arp.Generator.Preprocessing.Impl;

namespace Arp.Generator.Preprocessing.Impl
{
    public class NestedElementGenerationInfo : INestedElementInfo
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

        #region INestedElementInfo Members

        public IElementInfo Element
        {
            get { return this.element; }
        }

        public ICollection<IElementInfo> Elements
        {
            get
            {
                return CollectionsUtils.Transform<ElementGenerationInfo, IElementInfo>(this.elements,
                                                                                    delegate(
                                                                                        ElementGenerationInfo source)
                                                                                    {
                                                                                        return
                                                                                            source;
                                                                                    });
            }
        }

        #endregion
    }
}