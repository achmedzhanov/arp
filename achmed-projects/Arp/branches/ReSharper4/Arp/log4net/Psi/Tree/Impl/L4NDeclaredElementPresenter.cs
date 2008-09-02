using System;
using JetBrains.ComponentModel;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.Shell;
using JetBrains.Util;

namespace Arp.log4net.Psi.Tree.Impl
{
    [ShellComponentInterface(ProgramConfigurations.ALL), ShellComponentImplementation]
    public class L4NDeclaredElementPresenter : IDeclaredElementPresenter, IShellComponent
    {
        ///<param name="element">Contains <see cref="T:JetBrains.ReSharper.Psi.IDeclaredElement" /> to provide string presentation of.</param>
        ///<param name="marking">Returns the markup of the string with a <see cref="T:JetBrains.ReSharper.Psi.IDeclaredElement" /> presentation.</param>
        ///<summary>
        ///
        ///             Returns a string containing declared element text presentation made according to this presenter settings.
        ///             This method is usefull when additional processing is required for the returned string,
        ///             e.g. as is done in the following method:
        ///             
        ///<code>
        ///
        ///             RichText Foo(IMethod method)
        ///             {
        ///               DeclaredElementPresenterMarking marking;
        ///               RichTextParameters rtp = new RichTextParameters(ourFont);
        ///               // make rich text with declared element presentation
        ///               RichText result = new RichText(ourInvocableFormatter.Format(method, out marking),rtp);
        ///               // highlight name of declared element in rich text
        ///               result.SetColors(SystemColors.HighlightText,SystemColors.Info,marking.NameRange.StartOffset,marking.NameRange.EndOffset);
        ///               return result;
        ///             }
        ///             
        ///</code>
        ///
        ///</summary>
        ///
        public string Format(DeclaredElementPresenterStyle style, IDeclaredElement element, ISubstitution substitution,
                             out DeclaredElementPresenterMarking marking)
        {
            DeclaredElementPresenterMarking m = new DeclaredElementPresenterMarking();

            if (style.ShowName != NameStyle.NONE)
            {
                m.NameRange = new TextRange(0, element.ShortName.Length); 
            }

            marking = m;
            return element.ShortName;
        }

        ///<summary>
        ///
        ///            Returns language specific presentation for a given parameter kind 
        ///            
        ///</summary>
        ///
        public string Format(ParameterKind parameterKind)
        {
            throw new NotSupportedException();
        }

        ///<summary>
        ///
        ///            Returns language specific presentation for a given access rights value
        ///            
        ///</summary>
        ///
        public string Format(AccessRights accessRights)
        {
            throw new NotSupportedException();
        }

        public void Init()
        {
            // do nothing
        }

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        public void Dispose()
        {
            // do nothing
        }

        public static L4NDeclaredElementPresenter Instance
        {
            get
            {
                return (L4NDeclaredElementPresenter)JetBrains.Shell.Shell.Instance.GetComponent(typeof(L4NDeclaredElementPresenter));
            }
        }

    }
}