namespace Arp.Generator.Preprocessing.Impl
{
    public class TypeName
    {
        private string shortName;
        private string @namespace;


        public TypeName(string shortName, string @namespace)
        {
            this.shortName = shortName;
            this.@namespace = @namespace;
        }

        public string ShortName
        {
            get { return shortName; }
        }

        public string Namespace
        {
            get { return @namespace; }
        }

        public string FullName
        {
            get
            {
                return string.Format("{0}.{1}", @namespace, ShortName);
            }
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
            return FullName;
        }
    }
}