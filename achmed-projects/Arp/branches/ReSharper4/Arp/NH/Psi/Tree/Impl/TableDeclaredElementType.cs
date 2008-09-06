using System.Drawing;
using System.Reflection;
using JetBrains.ReSharper.Psi;
using JetBrains.UI;

namespace Arp.NH.Psi.Tree
{
    public class TableDeclaredElementType : DeclaredElementType
    {
        public static readonly TableDeclaredElementType INSTANCE = new TableDeclaredElementType();


        public TableDeclaredElementType()
            : base("")
        {
        }

        ///<summary>
        ///
        ///            Presentable name of the declared element
        ///            
        ///</summary>
        ///
        public override string PresentableName
        {
            get { return "Table"; }
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
                Bitmap image = ImageLoader.GetImage("table.ico", new Assembly[0]);
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
            get
            {
                return null;
            }
        }
    }
}