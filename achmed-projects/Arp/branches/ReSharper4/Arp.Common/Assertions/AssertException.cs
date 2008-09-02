using System;

namespace Arp.Common.Assertions
{
    public class AssertException : Exception
    {
        public AssertException()
        {
        }

        public AssertException(string message)
            : base(message)
        {
        }
    }
}