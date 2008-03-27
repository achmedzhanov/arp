using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Util;
using JetBrains.Util.DataStructures;

namespace Arp.log4net.Psi.Tree.Impl.StatisParameters
{
    public class StatisParameterDescriptorProvider : IParameterDescriptorProvider
    {
        readonly List<IParameterDescriptor> descriptors = new List<IParameterDescriptor>();


        #region IParameterDescriptorProvider Members

        /// <summary>
        /// return list of all available parameters
        /// </summary>
        /// <returns></returns>
        public ICollection<IParameterDescriptor> GetParameterDescriptors()
        {
            return new ReadOnlyCollection<IParameterDescriptor>(descriptors);
        }

        public IParameterDescriptor GetParameterDescriptor(string name)
        {
            return descriptors.Find(delegate(IParameterDescriptor obj)
                                 {
                                     return obj.Name == name;
                                 });
        }

        public bool IsAvailable
        {
            get { throw new System.NotImplementedException(); }
        }

        #endregion

        public void Add(IParameterDescriptor descriptor)
        {
            if (descriptor == null) throw new ArgumentNullException("descriptor");

            List<IParameterDescriptor> list = new List<IParameterDescriptor>();
            list.Add(descriptor);
            Add(list);
        }

        public void Add(IEnumerable<IParameterDescriptor> descriptors)
        {
            if (descriptors == null) throw new ArgumentNullException("descriptors");

            foreach (IParameterDescriptor descriptor in descriptors)
            {
                if (GetParameterDescriptor(descriptor.Name) != null)
                    throw new ArgumentException("Descriptor with name " + descriptor.Name + " already exists");
            }

            foreach (IParameterDescriptor descriptor in descriptors)
            {
                this.descriptors.Add(descriptor);
            }

        }

    }
}