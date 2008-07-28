using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;

namespace Arp.Common.Psi.Daemon
{
    public interface IHighlightingProcessor : IRecursiveElementProcessor
    {
        HighlightingInfo[] Highlightings        
        { get;}
    }
}