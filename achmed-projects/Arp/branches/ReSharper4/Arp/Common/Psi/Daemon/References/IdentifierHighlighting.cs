using System;
using System.Drawing;
using Arp.log4net.Services;
using JetBrains.ReSharper.Daemon;

namespace Arp.Common.Psi.Daemon.References
{
    [DaemonTooltipProvider(typeof(L4NIdentifierTooltipProvider))]
    [StaticSeverityHighlighting(Severity.INFO, OverlapResolve = OverlapResolveKind.NONE)]
    public class IdentifierHighlighting : ICustomAttributeHighlighting
    {
        private readonly string attributeId;

        public IdentifierHighlighting(string attributeId)
        {
            if (attributeId == null) throw new ArgumentNullException("attributeId");
            this.attributeId = attributeId;
        }

        ///<summary>
        ///
        ///            Attribute of this highlighting in the markup model
        ///            
        ///</summary>
        ///
        public string AttributeId
        {
            get { return attributeId; }
        }

        ///<summary>
        ///
        ///            Get the severity of this highlighting
        ///            
        ///</summary>
        ///
        public Severity Severity
        {
            get { return Severity.INFO; }
        }

        ///<summary>
        ///
        ///            Message for this highlighting to show in tooltip and in status bar (if <see cref="P:JetBrains.ReSharper.Daemon.IHighlighting.ShowToolTipInStatusBar" /> is 
        ///<c>true</c>)
        ///            To override the default mechanism of tooltip, mark the implementation class with 
        ///            <see cref="T:JetBrains.ReSharper.Daemon.DaemonTooltipProviderAttribute" /> attribute, and then this property will not be called
        ///            
        ///</summary>
        ///
        public string ToolTip
        {
            get { throw new System.NotImplementedException(); }
        }

        ///<summary>
        ///
        ///            Message for this highlighting to show in tooltip and in status bar (if <see cref="P:JetBrains.ReSharper.Daemon.IHighlighting.ShowToolTipInStatusBar" /> is 
        ///<c>true</c>)
        ///            
        ///</summary>
        ///
        public string ErrorStripeToolTip
        {
            get { throw new System.NotImplementedException(); }
        }

        ///<summary>
        ///
        ///            Color on gutter for this highlighting
        ///            NOTE: Will be called only if Severity == INFO
        ///            
        ///</summary>
        ///
        public Color ColorOnStripe
        {
            get { return Color.Empty; }
        }

        ///<summary>
        ///
        ///            Identifies if the tooltip message should be shown in the status bar when the cursor is over the highlighting
        ///            
        ///</summary>
        ///
        public bool ShowToolTipInStatusBar
        {
            get { return false; }
        }

        ///<summary>
        ///
        ///            Specifies the offset from the Range.StartOffset to set the cursor to when navigating 
        ///            to this highlighting. Usually returns 
        ///<c>0</c>
        ///</summary>
        ///
        public int NavigationOffsetPatch
        {
            get { return 0; }
        }
    }
}