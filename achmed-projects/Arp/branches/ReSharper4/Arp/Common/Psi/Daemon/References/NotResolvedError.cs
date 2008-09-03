using System.Drawing;
using JetBrains.ReSharper.Daemon;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Psi.Resolve;

namespace Arp.Common.Psi.Daemon.References
{
    [StaticSeverityHighlighting(Severity.ERROR, OverlapResolve = OverlapResolveKind.ERROR)]
    public class NotResolvedError : ICustomAttributeHighlighting
    {
        // Fields
        private readonly string message;
        private readonly IReference reference;

        // Methods
        public NotResolvedError(IReference reference)
        {
            this.reference = reference;
            this.message = string.Format("Cannot resolve symbol '{0}'", this.Reference.GetName());
        }

        // Properties
        public string AttributeId
        {
            get
            {
                return HighlightingAttributeIds.UNRESOLVED_ERROR_ATTRIBUTE;
            }
        }

        public Color ColorOnStripe
        {
            get
            {
                return Color.Empty;
            }
        }

        public string ErrorStripeToolTip
        {
            get
            {
                return this.ToolTip;
            }
        }

        public int NavigationOffsetPatch
        {
            get
            {
                return 0;
            }
        }

        public DocumentRange Range
        {
            get
            {
                return this.Reference.GetDocumentRange();
            }
        }

        public IReference Reference
        {
            get
            {
                return this.reference;
            }
        }

        public Severity Severity
        {
            get
            {
                return Severity. ERROR;
            }
        }

        public bool ShowToolTipInStatusBar
        {
            get
            {
                return true;
            }
        }

        public string ToolTip
        {
            get
            {
                return this.message;
            }
        }
        
    }
}