using System;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;
using NUnit.Framework;

namespace Arp.Tests.Parsing
{
    public class BaseParsingTest
    {
        protected void AssertChildsList(CompositeElement compositeElement, params Action<IElement> [] actions)
        {
            int index = 0;
            for (TreeElement child = compositeElement.firstChild; 
                 child != compositeElement.lastChild; child = child.nextSibling, index++)
            {
                if(index >= actions.Length)
                {
                    Assert.Fail("There is child unnecessary with index " + index + " " + child);
                }

                actions[index](child);
            }

            if(index < actions.Length - 1)
                Assert.Fail("There is no element with index  " + index);

        }
    }
}