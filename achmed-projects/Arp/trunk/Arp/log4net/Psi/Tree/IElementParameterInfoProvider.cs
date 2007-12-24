using System.Collections.Generic;

namespace Arp.log4net.Psi.Tree
{
    public interface IElementParameterInfoProvider
    {
        ICollection<IElementParametrInfo> GetParameterInfos();

        bool IsValid { get;}

    }
}