using Arp.Assertions;
using JetBrains.ReSharper.Psi;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class ElementParametrInfoImpl : IElementParametrInfo
    {
        private readonly IProperty property;


        public ElementParametrInfoImpl(IProperty property)
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
    }
}