/*
 *      GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007
 *
 *         This program stores the YouTube Links on local device
 *         Copyright (C) <2019>  <Github: Isayso>
 *
 *         This program is free software: you can redistribute it and/or modify
 *         it under the terms of the GNU General Public License as published by
 *         the Free Software Foundation, either version 3 of the License, or
 *         (at your option) any later version.
 *
 *         This program is distributed in the hope that it will be useful,
 *         but WITHOUT ANY WARRANTY; without even the implied warranty of
 *         MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *         GNU General Public License for more details.
 *
 *         You should have received a copy of the GNU General Public License
 *         along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */
using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PlaylistEditor
{
    class NativeMethods
    {

        // DLL libraries used to manage hotkeys http://frasergreenroyd.com/c-global-keyboard-listeners-implementation-of-key-hooks/
        //https://ourcodeworld.com/articles/read/573/how-to-register-a-single-or-multiple-global-hotkeys-for-a-single-key-in-winforms
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr point);


        // https://docs.microsoft.com/en-us/windows/desktop/api/shlwapi/nf-shlwapi-pathfindonpathw
        // https://www.pinvoke.net/default.aspx/shlwapi.PathFindOnPath
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, SetLastError = false)]
        static extern bool PathFindOnPath([In, Out] StringBuilder pszFile, [In] string[] ppszOtherDirs);

        // from MAPIWIN.h :
        private const int MAX_PATH = 260;


        /// <summary>
        /// get the UNC path from Network drives
        /// </summary>
        /// <param name="path">path</param>
        /// <returns>UNC path filename</returns>
        public static string UNCPath(string path)
        {
            if (!path.StartsWith(@"\\"))
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Network\\" + path[0]))
                {
                    if (key != null)
                    {
                        return key.GetValue("RemotePath").ToString() + path.Remove(0, 2).ToString();
                    }
                }
            }
            return path;
        }

        //public static bool CheckOpenPath()
        //{
        //    // HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Comdlg32\OpenSaveMRU.
        //    using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Comdlg32\\OpenSaveMRU\\" + path[0]))
        //    {
        //        if (key != null)
        //        {
        //            return key.GetValue("RemotePath").ToString() + path.Remove(0, 2).ToString();
        //        }
        //    }
        //    return true;
        //}

        /// <summary>
        /// Gets the full path of the given executable filename as if the user had entered this
        /// executable in a shell. So, for example, the Windows PATH environment variable will
        /// be examined. If the filename can't be found by Windows, null is returned.</summary>
        /// <param name="exeName"></param>
        /// <returns>The full path if successful, or null otherwise.</returns>
        public static string GetFullPathFromWindows(string exeName)
        {
            if (exeName.Length >= MAX_PATH)
                throw new ArgumentException($"The executable name '{exeName}' must have less than {MAX_PATH} characters.",
                    nameof(exeName));

            StringBuilder sb = new StringBuilder(exeName, MAX_PATH);
            return PathFindOnPath(sb, null) ? sb.ToString() : null;
        }

    }
}
