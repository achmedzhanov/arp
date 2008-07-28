using System.Drawing;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;

namespace Arp.NH.Stages.DuplicateMapping
{
    public class DuplicateMappingHighlighting : IHighlighting
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

        public OverlapResolvePolicy OverlapResolvePolicy
        {
            get
            {
                return OverlapResolvePolicy.UNRESOLVED_ERROR;
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