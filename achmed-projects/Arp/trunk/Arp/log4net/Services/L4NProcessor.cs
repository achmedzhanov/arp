using System;
using System.Collections.Generic;
using Arp.log4net.Psi.Tree;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Services
{
    public class L4NProcessor : IRecursiveElementProcessor
    {
        
        List<HighlightingInfo> highlightings = new List<HighlightingInfo>();

        public bool InteriorShouldBeProcessed(IElement element)
        {
//            IL4NElement l4nElement = element as IL4NElement;
//            if (l4nElement == null)
//                return false;
//
//            return true;

            return true;
        }

        public void ProcessBeforeInterior(IElement element)
        {
            // do nothing
        }


        public HighlightingInfo[] Highlightings
        {
            get { return highlightings.ToArray(); }
        }

        public void ProcessAfterInterior(IElement element)
        {
            IElementParametersOwner elementParametersOwner = element as IElementParametersOwner;

            if(elementParametersOwner != null)
            {
                IElementParameterInfoProvider elementParameterInfoProvider = elementParametersOwner as IElementParameterInfoProvider;

                if (elementParameterInfoProvider == null)
                    return;

                if(!elementParameterInfoProvider.IsValid)
                    return;

                ICollection<IElementParametrInfo> infos = elementParameterInfoProvider.GetParameterInfos();

                foreach (IParam param in elementParametersOwner.GetParams())
                {
                    IList<IElementParametrInfo> filteredInfos = CollectionUtil.FindAll(infos, delegate(IElementParametrInfo obj)
                                                                            {
                                                                                return param.Name == obj.Name;
                                                                            });

                    if(filteredInfos.Count == 0)
                    {
                        highlightings.Add(new HighlightingInfo(param.NameDocumentRange, new InvalidPropertyHighlighting()));
                    }
                }
            }
        }

        public bool ProcessingIsFinished
        {
            get { return false; }
        }
    }
}