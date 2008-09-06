namespace Arp.Databases.Meta.Impl
{
    public class BaseColumn : IColumn
    {
        private readonly string name;

        public BaseColumn(string name)
        {
            this.name = name;
        }



        #region IColumn Members

        public string Name
        {
            get { return name; }
        }

        #endregion
    }
}