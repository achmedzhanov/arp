
using Arp.log4net.Services.QuickFixes;
using JetBrains.ComponentModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.Shell;

namespace Arp.log4net.Services
{
    [ShellComponentImplementation(ProgramConfigurations.ALL)]
    public class QuickFixRegistrarComponent : IShellComponent
    {
        #region IComponent Members

        public void Init()
        {
            QuickFixTable table = QuickFixTable.Instance;
            table.RegisterQuickFix(typeof(MissedParameterError), typeof(CreateMissedParameterFix), 1);            
        }

        #endregion

        #region IDisposable Members

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose()
        {
            
        }

        #endregion

        
        
    }
}