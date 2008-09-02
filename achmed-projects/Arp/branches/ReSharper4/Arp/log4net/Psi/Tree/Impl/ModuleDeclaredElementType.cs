using System.Drawing;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi;

namespace Arp.log4net.Psi.Tree.Impl
{
    public class ModuleDeclaredElementType : DeclaredElementType
    {
        public static readonly ModuleDeclaredElementType INSTANCE = new ModuleDeclaredElementType();


        public ModuleDeclaredElementType() : base("")
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
            get { return ""; }
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
                return ProjectModelIconManager.Instance.GetAssemblyImage();
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