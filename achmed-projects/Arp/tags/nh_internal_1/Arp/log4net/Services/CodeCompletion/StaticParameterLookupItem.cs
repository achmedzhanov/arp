using System.Drawing;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.CodeInsight.Services.Lookup;
using JetBrains.ReSharper.Psi;

namespace Arp.log4net.Services.CodeCompletion
{
    public class StaticParameterLookupItem : TextLookupItem
    {
        public StaticParameterLookupItem(string name) : base(name /*, name - second column text */)
        {
            
        }

        ///<summary>
        ///
        ///            Gets item image
        ///            
        ///</summary>
        ///
        public override Image Image
        {
            get { return PsiIconManager.Instance.GetImage(CLRDeclaredElementType.PARAMETER, PsiIconExtension.None); }
        }
    }
}