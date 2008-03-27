using System.Drawing;
using JetBrains.ReSharper.Psi;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class L4NDeclaredElementType : DeclaredElementType
    {
        private readonly string name;
        private readonly Image image;
        public static readonly L4NDeclaredElementType Appender = new L4NDeclaredElementType("appender");


        public L4NDeclaredElementType(string name) : base(name)
        {
            this.name = name;
        }


        public L4NDeclaredElementType(string baseName, string name, Image image) : base(baseName)
        {
            this.name = name;
            this.image = image;
        }

        ///<summary>
        ///
        ///            Presentable name of the declared element
        ///            
        ///</summary>
        ///
        public override string PresentableName
        {
            get { return name; }
        }

        ///<summary>
        ///
        ///            Image of the declared element
        ///            
        ///</summary>
        ///
        protected override Image Image
        {
            get
            {
                // TODO appender, logger, layout image
                return image;
            }
        }

        ///<summary>
        ///
        ///            Default declared element presenter
        ///            
        ///</summary>
        ///
        protected override IDeclaredElementPresenter DefaultPresenter
        {
            get { return L4NDeclaredElementPresenter.Instance; }
        }
    }
}