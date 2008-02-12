using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Caches;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class TypedParametersOwner : BaseL4NTag
    {
        public TypedParametersOwner(CompositeNodeType _type) : base(_type)
        {
        }

        private bool calculatedCLRType = false;
        protected ITypeElement appenderCLRType = null;

        public ITypeElement TypeFromAttribute
        {
            get
            {
                if(!calculatedCLRType)
                {
                    string typeValue = GetAttributeStringValue(L4NConstants.TYPE, null);
                    
                    if(!string.IsNullOrEmpty(typeValue))
                    {
                        IDeclarationsCache cache = GetManager().GetDeclarationsCache(DeclarationsCacheScope.SolutionScope(this.GetProjectFile().GetSolution(), true), true);
                        appenderCLRType = cache.GetTypeElementByCLRName( /* TODO trim typeValue */ typeValue);
                        
                        // TODO what will wa do when cache return more then one element ?
                        //appenderCLRType = cache.GetTypeElementsByCLRName()
                    }

                    calculatedCLRType = true;

                }

                return appenderCLRType;
            }
        }
    }
}