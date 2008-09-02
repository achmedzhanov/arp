using System;
using System.Collections.Generic;
using Arp.Common.Psi.Daemon;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Editor;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Xml.Tree;
using JetBrains.Util;

namespace Arp.Common.Psi.Daemon
{
    public class ProcessorDaemonStageProcess<T> : BaseProcessorDaemonStageProcess
        where T : IHighlightingProcessor, new()
    {
        public ProcessorDaemonStageProcess(IXmlFile file, IDaemonProcess process) : base(file, process)
        {
        }

        protected override IHighlightingProcessor CreateProcessor()
        {
            return new T();
        }
    }
}