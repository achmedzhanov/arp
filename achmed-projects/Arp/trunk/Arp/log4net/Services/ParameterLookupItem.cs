using System;
using System.Drawing;
using Arp.log4net.Psi.Tree;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.CodeInsight.Services.ParameterInfo;
using JetBrains.ReSharper.Psi;

namespace Arp.log4net.Services
{
    // TODO IParameterInfoProvidingLookupItem we need to implement .PreferredDeclaredElement to show Parameter XML Doc (!)
    // TODO implement GetReferences -> reference to property
    // TODO see TooltipManager 
    public class ParameterLookupItem : LookupItemBase 
    {
        private IParameterDescriptor parameterDescriptor;


        public ParameterLookupItem(IParameterDescriptor parameterDescriptor)
        {
            if (parameterDescriptor == null) throw new ArgumentNullException("parameterDescriptor");
            this.parameterDescriptor = parameterDescriptor;

            this.DisplayName = parameterDescriptor.Name;
            this.TypeInName = parameterDescriptor.Name;
        }

        ///<summary>
        ///
        ///            Gets item image
        ///            
        ///</summary>
        ///
        public override Image Image
        {
            get
            {
                return PsiIconManager.Instance.GetImage(CLRDeclaredElementType.PROPERTY);
            }
        }
    }
}