namespace Arp.Generator.Preprocessing.Impl
{
    public class AttributeGenerationInfo : IAttributeInfo
    {
        private string xmlName;
        private string name;
        
        // TODO
        //private string type;


        public AttributeGenerationInfo(string xmlName, string name)
        {
            this.xmlName = xmlName;
            this.name = name;
        }


        public string XmlName
        {
            get { return xmlName; }
        }

        public string Name
        {
            get { return name; }
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
            return "[XmlName:" + XmlName + ", Name:" + Name + "]";
        }

        #region IAttributeInfo Members

        string IAttributeInfo.Name
        {
            get { return xmlName; }
        }

        #endregion
    }
}