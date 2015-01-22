using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Findall2.Scanners
{
    /// <summary>
    /// Used to address a flaw in <see cref="FileSystemInfo.Refresh"/> where it may not refresh the properties of the file.
    /// </summary>
    /// <remarks>This has been observed when Log4Net is holding the file open.</remarks>
    public static class FileRefresher
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
        private static extern SafeFileHandle CreateFile(
            [MarshalAs(UnmanagedType.LPWStr)]
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        /// <summary>
        /// Indicates file was in use at the exact moment it was checked.
        /// </summary>
        /// <param name="path">The full path to the file on disk.</param>
        /// <returns>true if an exception occurred opening the file.  False if no exception occurred.</returns>
        /// <remarks>
        /// Will not throw under any circumstances. It is possible that this method will refresh the file attributes.
        /// NOTE: this function essentially performs the same task as <see cref="RefreshFile"/> but carries additional
        /// overhead so it is of little value to perform this function first followed by RefreshFile.
        /// </remarks>
        public static bool FileIsInUse(string path)
        {
            try
            {
                using (new FileStream(path, FileMode.Open))
                {
                    return false;
                }
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// Forces a file system refresh by obtaining a file handle via the <see cref="CreateFile"/> API
        /// without actually requesting any permissions or making any changes to the file itself.
        /// </summary>
        /// <param name="path">The full path of the file on disk.</param>
        /// <remarks>
        /// Call <see cref="FileSystemInfo.Refresh"/> to get new file attributes such as
        /// <see cref="FileSystemInfo.LastWriteTime"/>.
        /// </remarks>
        /// <exception cref="Exception">
        /// Thrown by <see cref="Marshal.ThrowExceptionForHR(int)"/> if a file handle could not be obtained.
        /// </exception>
        public static void RefreshFile(string path)
        {
            const uint fileReadAccess = 0x00000001;
            const uint fileWriteAccess = 0x00000002;
            const uint fileDeleteAccess = 0x00000004;
            const uint anyAccess = 0;
            const uint shareAccess = fileReadAccess | fileWriteAccess | fileDeleteAccess;
            const uint openExisting = 0x00000003;
            const uint noFlagsAndAttributes = 0;

            using (SafeFileHandle handle = CreateFile(path, anyAccess, shareAccess, IntPtr.Zero, openExisting, noFlagsAndAttributes, IntPtr.Zero))
            {
                if (handle == null || handle.IsInvalid)
                {
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                }
            }
        }
    }
}
