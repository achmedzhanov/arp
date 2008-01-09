using Arp.Assertions;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class ParameterDescriptorImpl : IParameterDescriptor
    {
        private readonly IProperty property;


        public ParameterDescriptorImpl(IProperty property)
        {
            this.property = property;
        }

        public string Name
        {
            get
            {
                return Normalize(property.ShortName);
            }
        }

        private string Normalize(string name)
        {
            Assert.Check(name.Length > 0);
            if(char.IsUpper(name[0]))
            {
                return char.ToLower(name[0]) + name.Substring(1);
            }
            
            return name;
        }

        public IType Type
        {
            get
            {
                return property.Type;
            }
        }


        public IDeclaredElement DecraedElement
        {
            get { return property; }
        }

        ///<summary>
        ///Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("[Name:{0},Type:{1}]", Name, Type);
        }
    }
}