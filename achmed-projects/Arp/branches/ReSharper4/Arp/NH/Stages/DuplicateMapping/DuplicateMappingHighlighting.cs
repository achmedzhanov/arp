using System.Drawing;
using JetBrains.ReSharper.Daemon;
using JetBrains.DocumentModel;

namespace Arp.NH.Stages.DuplicateMapping
{
    [StaticSeverityHighlighting(Severity.ERROR, OverlapResolve = OverlapResolveKind.ERROR)]
    public class DuplicateMappingHighlighting : ICustomAttributeHighlighting
    {
        // Fields
        private readonly string message;


        public DuplicateMappingHighlighting(string message)
        {
            this.message = message;
        }

        // Properties
        public string AttributeId
        {
            get
            {
                return HighlightingAttributeIds.ERROR_ATTRIBUTE;
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


        public Severity Severity
        {
            get
            {
                return Severity.ERROR;
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