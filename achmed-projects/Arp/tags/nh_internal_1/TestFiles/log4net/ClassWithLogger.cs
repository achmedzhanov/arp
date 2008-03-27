using System.Reflection;
using log4net;

namespace TestFiles.log4net
{
    public class ClassWithLogger
    {
        private static readonly ILog log = LogManager.GetLogger(MethodInfo.GetCurrentMethod().DeclaringType);

        public void TestMethod()
        {
            log.DebugFormat("MARK");
        }

        void AnoterMethod()
        {
            
        }
    }
}