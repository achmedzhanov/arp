using Arp.Common.Psi.Daemon;
using Arp.NH.Services.References;
using Arp.Xml;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.NH.Stages.DuplicateMapping
{
    [DaemonStage()]
    public class DiplaceMappingsStage : BaseXmlDaemonStage
    {
        protected override IDaemonStageProcess CreateProcess(IDaemonProcess process, IXmlFile file)
        {
            return new ProcessorDaemonStageProcess<DiplaceMappingsProcessor>(file, process);
        }

        ///<summary>
        ///
        ///            Check the error stripe indicator necessity for this stage after processing given <paramref name="projectFile" />
        ///</summary>
        ///
        public override ErrorStripeRequest NeedsErrorStripe(IProjectFile projectFile)
        {
            return ErrorStripeRequest.STRIPE_AND_ERRORS;
        }        
    }
}