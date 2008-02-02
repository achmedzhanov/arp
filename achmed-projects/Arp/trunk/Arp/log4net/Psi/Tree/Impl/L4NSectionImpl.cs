using System.Collections.Generic;
using Arp.log4net.Psi.Tree.Impl.StatisParameters;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class L4NSectionImpl : BaseL4NTag, IL4NSection, IParameterDescriptorProvider
    {
        private static StatisParameterDescriptorProvider parameters;

        static L4NSectionImpl()
        {
            parameters = new StatisParameterDescriptorProvider();
            // elements
            parameters.Add(new StatisParameterDescriptor("appender", false));
            parameters.Add(new StatisParameterDescriptor("root", false));
            parameters.Add(new StatisParameterDescriptor("logger", false));
//            parameters.Add(new StatisParameterDescriptor("renderer", false));
//            parameters.Add(new StatisParameterDescriptor("param", false));

            parameters.Add(new StatisParameterDescriptor("debug", false, StatisParameterDescriptor.BOOLEAN_VALUES));
            parameters.Add(new StatisParameterDescriptor("update", false, new string[] {"Merge", "Override"}));
            parameters.Add(new StatisParameterDescriptor("threshold", false, StatisParameterDescriptor.THRESHOLD));            
        }

        public L4NSectionImpl()
            : base(L4NElementType.L4NSECTION_ELEMENT)
        {
        }


        public ICollection<IAppender> GetAppenders()
        {
            return GetTagsByType<IAppender>();
        }

        public ICollection<ILogger> GetLoggers()
        {
            return GetTagsByType<ILogger>();
        }

        #region IParameterDescriptorProvider Members

        /// <summary>
        /// return list of all available parameters
        /// </summary>
        /// <returns></returns>
        public ICollection<IParameterDescriptor> GetParameterDescriptors()
        {
            return parameters.GetParameterDescriptors();
        }

        public IParameterDescriptor GetParameterDescriptor(string name)
        {
            return parameters.GetParameterDescriptor(name);
        }

        /// <summary>
        /// return list of all available parameters
        /// </summary>
        /// <returns></returns>


        public bool IsAvailable
        {
            get { return true ; }
        }

        #endregion
    }
}