using System.Collections.Generic;

namespace Arp.log4net.Psi.Tree
{
    public interface IElementParametersOwner
    {
        ICollection<IParam> GetParams();
    }
}