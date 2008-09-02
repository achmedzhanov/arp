using System.Collections.Generic;
using JetBrains.ReSharper.Daemon;

namespace Arp.Common.Psi.Daemon
{
    public class BaseProcessor
    {
        protected readonly List<HighlightingInfo> highlightings = new List<HighlightingInfo>();

        public HighlightingInfo[] Highlightings
        {
            get { return highlightings.ToArray(); }
        }
    }
}