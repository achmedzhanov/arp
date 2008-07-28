using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Arp.Utils;

namespace EscapeInputSender
{
    class Program
    {
     
        static void Main(string[] args)
        {
            while(true)
            {
                Thread.Sleep(5 * 1000);
                Console.WriteLine(DateTime.Now + " Send Escape");
                Win32.SendEscape((IntPtr)0);
            }
        }
    }
}
