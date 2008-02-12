using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Arp.Assertions;
using JetBrains.UI.Interop;

namespace Arp.Utils
{
    public static class Win32
    {
        [DllImport("user32.dll", SetLastError = false)]
        static extern UIntPtr GetMessageExtraInfo();

        public static void SendEscape(IntPtr target)
        {
            if(target != IntPtr.Zero)
            {
                int processId;
                int inputThread = Win32Declarations.GetWindowThreadProcessId(target, out processId);
                Assert.Check(inputThread == Win32Declarations.GetCurrentThreadId());                
            }

            INPUT[] input = new INPUT[2];
            input[0].type = input[1].type = Win32Declarations.INPUT_KEYBOARD;
            input[0].ki.wScan = input[1].ki.wScan = 0;
            input[0].ki.time = input[1].ki.time = 0;
            input[1].ki.dwFlags = Win32Declarations.KEYEVENTF_KEYUP;
            input[0].ki.dwExtraInfo = input[1].ki.dwExtraInfo = GetMessageExtraInfo();
            input[0].ki.wVk = input[1].ki.wVk = 0x1B; // escape

            uint ret = Win32Declarations.SendInput(input);

            if(ret != 2)
            {
                throw new Win32Exception("Unexpected error " + Win32Declarations.GetLastError());
            }

            // TODO use SetForegroundWindow ?
        }
    }
}