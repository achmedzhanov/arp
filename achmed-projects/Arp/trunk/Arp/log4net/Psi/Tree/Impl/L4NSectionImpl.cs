using System.Collections.Generic;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class L4NSectionImpl : BaseL4NTag, IL4NSection
    {
        public L4NSectionImpl()
            : base(L4NElementType.L4NSECTION_ELEMENT)
        {
        }


        public ICollection<IAppender> GetAppenders()
        {
            return GetTagsByType<IAppender>();
        }

        public ICollection<ILogger> GetLoggers()
        {
            return GetTagsByType<ILogger>();
        }

    }
}