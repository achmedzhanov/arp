using System.Collections.Generic;
using System.Text;
using JetBrains.ReSharper.Psi;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class EncodingParameterDescriptorDecorator : IParameterDescriptor
    {
        private IParameterDescriptor original;

        public EncodingParameterDescriptorDecorator(IParameterDescriptor original)
        {
            this.original = original;
        }


        public string Name
        {
            get { return original.Name; }
        }

        public IType Type
        {
            get { return original.Type; }
        }

        public IDeclaredElement DecraedElement
        {
            get { return original.DecraedElement; }
        }

        public bool IsRequired
        {
            get { return original.IsRequired; }
        }

        public string RequredBefore
        {
            get { return original.RequredBefore; }
        }

        public string RequredAfter
        {
            get { return original.RequredAfter; }
        }

        public bool IsAttribute
        {
            get { return original.IsAttribute; }
        }

        public string[] Conflicts
        {
            get { return original.Conflicts; }
        }

        #region IParameterDescriptor Members

        public bool IsEnumerable
        {
            get { return true; }
        }

        public string[] PossibleValues
        {
            get
            {
                return GetEncodingNames();
            }
        }

        #endregion

        public bool RequredType
        {
            get { return false; }
        }

        private string[] GetEncodingNames()
        {
            List <string> names = new List<string>();
            EncodingInfo[] encodings = Encoding.GetEncodings();
            foreach (EncodingInfo info in encodings)
            {
                names.Add(info.Name);
            }
            return names.ToArray();
        }
    }
}