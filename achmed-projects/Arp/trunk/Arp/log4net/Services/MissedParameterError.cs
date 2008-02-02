using System;
using System.Drawing;
using Arp.log4net.Psi.Tree;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi.Tree;

namespace Arp.log4net.Services
{
    public class MissedParameterError : IHighlighting
    {
        private readonly IParameterDescriptor descriptor;
        private readonly IElement element;
        private readonly string toolTip;

        public MissedParameterError(IParameterDescriptor descriptor, IElement element)
        {
            if (descriptor == null) throw new ArgumentNullException("descriptor");
            if (element == null) throw new ArgumentNullException("element");
            this.descriptor = descriptor;
            this.element = element;
            this.toolTip = string.Format("Missed requred {0} {1}", (descriptor.IsAttribute ? "attribute" : "element" ), descriptor.Name);
        }

        #region IHighlighting Members

        ///<summary>
        ///
        ///            Attribute of this highlighting in the markup model
        ///            
        ///</summary>
        ///
        public string AttributeId
        {
            get { return HighlightingAttributeIds.ERROR_ATTRIBUTE; }
        }

        ///<summary>
        ///
        ///            Get the ovelap resolving policy for this highlighting
        ///            
        ///</summary>
        ///
        public OverlapResolvePolicy OverlapResolvePolicy
        {
            get { return OverlapResolvePolicy.NONE; }
        }

        ///<summary>
        ///
        ///            Get the severity of this highlighting
        ///            
        ///</summary>
        ///
        public Severity Severity
        {
            get { return Severity.ERROR; }
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
            get { return toolTip; }
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
            get { return ToolTip; }
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

        public IParameterDescriptor Descriptor
        {
            get { return descriptor; }
        }

        public IElement Element
        {
            get { return element; }
        }

        #endregion
    }
}