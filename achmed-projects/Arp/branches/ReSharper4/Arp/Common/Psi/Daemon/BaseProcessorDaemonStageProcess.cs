using System;
using Arp.Common.Psi.Daemon;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.Common.Psi.Daemon
{
    public abstract class BaseProcessorDaemonStageProcess : IDaemonStageProcess
    {
        private readonly IXmlFile file;
        private readonly IDaemonProcess process;

        public BaseProcessorDaemonStageProcess(IXmlFile file, IDaemonProcess process)
        {
            if (file == null) 
                throw new ArgumentNullException("file");
            if (process == null) 
                throw new ArgumentNullException("process");

            this.file = file;
            this.process = process;
        }

        ///<summary>
        ///
        ///            Executes the process and returns resulting highlightings and embedded objects to be inserted into the editor.
        ///            The process should check for <see cref="P:JetBrains.ReSharper.Daemon.IDaemonProcess.InterruptFlag" /> periodically (with intervals less than 100 ms)
        ///            and throw <see cref="T:JetBrains.Shell.Progress.ProcessCancelledException" /> if it is true. 
        ///            Failing to do so may cause the program to prevent user from typing while analysing the code.
        ///            
        ///</summary>
        ///
        ///<returns>
        ///New highlightings and embedded objects. Return 
        ///<c>null</c> if this stage doesn't produce
        ///            any of them.
        ///</returns>
        ///
        public DaemonStageProcessResult Execute()
        {
            IHighlightingProcessor processor = CreateProcessor();
            file.ProcessDescendants(processor);
            DaemonStageProcessResult result = new DaemonStageProcessResult();
            result.Highlightings = processor.Highlightings;
            result.RehighlightedRanges = new TextRange[] { new TextRange(0, file.GetTextLength()) };
            return result;
            
            return result;
        }

        protected abstract IHighlightingProcessor CreateProcessor();



    }
}