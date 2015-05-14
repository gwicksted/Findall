using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Findall2.Utilities
{
    /// <summary>
    /// Class that encapsulates native P/Invoke method calls
    /// </summary>
    public static class SafeNativeMethods
    {
        /// <summary>
        /// Call to CreateFileW (<see cref="CharSet.Unicode"/> causes this) used to open a file handle.
        /// </summary>
        /// <param name="lpFileName">The full path to the file on disk.</param>
        /// <param name="dwDesiredAccess">The desired access required (such as read/write or no specific access).</param>
        /// <param name="dwShareMode">The amount of sharing permitted (if other applications attempt to access).</param>
        /// <param name="lpSecurityAttributes">Security parameters.</param>
        /// <param name="dwCreationDisposition">Create new, create or re-create, create or append, open existing.</param>
        /// <param name="dwFlagsAndAttributes"></param>
        /// <param name="hTemplateFile"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern SafeFileHandle CreateFile(
            [MarshalAs(UnmanagedType.LPWStr)]
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);
    }
}
