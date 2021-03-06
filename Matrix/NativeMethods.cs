﻿using System;

namespace Matrix
{
    public partial class NativeMethods
    {
        public static Int32 STD_OUTPUT_HANDLE = -11;

        /// Return Type: HANDLE->void*
        ///nStdHandle: DWORD->unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "GetStdHandle")]
        internal static extern IntPtr GetStdHandle(Int32 nStdHandle);

        /// Return Type: BOOL->int
        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "AllocConsole")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        internal static extern bool AllocConsole();
    }
}
