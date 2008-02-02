using System.Collections.Generic;
using Arp.log4net.Psi.Tree.Impl.StatisParameters;
using JetBrains.ReSharper.Psi.Impl;


namespace Arp.log4net.Psi.Tree.Impl
{
    public class LoggerImpl : BaseL4NTag, ILogger, IDeclaredParametersOwner, IParameterDescriptorProvider
    {
        static readonly StatisParameterDescriptorProvider parameters;
        static readonly StatisParameterDescriptorProvider rootParameters;
        private readonly SimpleParameterOwner owner;

        static LoggerImpl()
        {
            rootParameters = new StatisParameterDescriptorProvider();
            rootParameters.Add(new StatisParameterDescriptor("appender-ref", false));
            rootParameters.Add(new StatisParameterDescriptor("level", false));
            rootParameters.Add(new StatisParameterDescriptor("param", false));

            parameters = new StatisParameterDescriptorProvider();
            parameters.Add(rootParameters.GetParameterDescriptors());
            parameters.Add(new StatisParameterDescriptor("name", true, true));
            parameters.Add(new StatisParameterDescriptor("additivity", false, StatisParameterDescriptor.BOOLEAN_VALUES));
        }

        public LoggerImpl()
            : base(L4NElementType.LOGGER_ELEMENT)
        {
            owner = new SimpleParameterOwner(this, this);
        }

        #region ILogger Members
        public string Name
        {
            get
            {
                return GetAttributeValue(L4NConstants.NAME, string.Empty);
            }
        }

        public new IL4NSection Parent
        {
            get { return (IL4NSection)base.Parent; }
        }

        

        public bool IsRoot
        {
            get { return TagName == L4NConstants.ROOT_LOGGER; }
        }

        #endregion

        #region IParameterDescriptorProvider Members

        /// <summary>
        /// return list of all available parameters
        /// </summary>
        /// <returns></returns>
        public ICollection<IParameterDescriptor> GetParameterDescriptors()
        {
            return GetProvider().GetParameterDescriptors();
        }

        public IParameterDescriptor GetParameterDescriptor(string name)
        {
            return GetProvider().GetParameterDescriptor(name);
        }

        private StatisParameterDescriptorProvider GetProvider()
        {
            return IsRoot ? rootParameters : parameters;
        }

        public bool IsAvailable
        {
            get
            {
                return true;
            }
        }

        #endregion

        ICollection<IDeclaredParameter> IDeclaredParametersOwner.GetParams()
        {
            return this.owner.GetParams();
        }
    }
}