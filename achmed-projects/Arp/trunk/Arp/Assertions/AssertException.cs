using System;

namespace Arp.Assertions
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