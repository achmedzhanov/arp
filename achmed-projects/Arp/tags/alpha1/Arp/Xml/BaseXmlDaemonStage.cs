using System;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xml.Tree;

namespace Arp.Xml
{
    public abstract class BaseXmlDaemonStage : IDaemonStage
    {
        ///<summary>
        ///
        ///            Creates a code analysis process corresponding to this stage for analysing a file.
        ///            
        ///</summary>
        ///
        ///<returns>
        ///Code analysis process to be executed or 
        ///<c>null</c> if this stage is not available for this file.
        ///</returns>
        ///
        public IDaemonStageProcess CreateProcess(IDaemonProcess process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            IXmlFile xmlFile = GetXmlFile(process.ProjectFile);

            if (xmlFile == null)
                return null;

            return CreateProcess(process, xmlFile);
        }

        protected abstract IDaemonStageProcess CreateProcess(IDaemonProcess process, IXmlFile file);


        protected static IXmlFile GetXmlFile(IProjectFile projectFile)
        {
            if (projectFile == null)
            {
                return null;
            }
            PsiManager manager = PsiManager.GetInstance(projectFile.GetSolution());
            if (!manager.IsCommitted(projectFile))
            {
                return null;
            }
            return (manager.GetPsiFile(projectFile) as IXmlFile);
        }

        ///<summary>
        ///
        ///            Check the error stripe indicator necessity for this stage after processing given <paramref name="projectFile" />
        ///</summary>
        ///
        public abstract ErrorStripeRequest NeedsErrorStripe(IProjectFile projectFile);

    }
}