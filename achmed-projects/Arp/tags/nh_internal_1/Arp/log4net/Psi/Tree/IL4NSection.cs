using System.Collections.Generic;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.log4net.Psi.Tree
{
    public interface IL4NSection : IXmlTag, IL4NElement
    {
        /// <summary>
        /// return all appendres from secion
        /// </summary>
        /// <returns></returns>
        ICollection<IAppender> GetAppenders();
        
        /// <summary>
        /// return all loggers from section 
        /// </summary>
        /// <returns></returns>
        ICollection<ILogger> GetLoggers();
    }
}