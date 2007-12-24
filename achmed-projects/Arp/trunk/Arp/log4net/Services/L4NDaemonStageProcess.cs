using System;
using System.Collections.Generic;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.log4net.Services
{
    public class L4NDaemonStageProcess : IDaemonStageProcess
    {
        private readonly IXmlFile file;
        private readonly IDaemonProcess process;


        public L4NDaemonStageProcess(IXmlFile file, IDaemonProcess process)
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
            L4NProcessor processor = new L4NProcessor();
            file.ProcessDescendants(processor);
            DaemonStageProcessResult result = new DaemonStageProcessResult();
            result.Highlightings = processor.Highlightings;
            result.RehighlightedRanges = new TextRange[] { new TextRange(0, file.GetTextLength()) };
            return result;
            
            return result;
        }

    }
}