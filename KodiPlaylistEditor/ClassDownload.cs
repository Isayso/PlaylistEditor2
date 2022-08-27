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
using PlaylistEditor.Properties;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PlaylistEditor
{
    public static class ClassDownload
    {

        /// <summary>
        /// check if ffmpeg.exe is there
        /// </summary>
        /// <returns></returns>
        public static bool CheckForFfmpeg()
        {

            if (!string.IsNullOrEmpty(NativeMethods.GetFullPathFromWindows("ffmpeg.exe")) ||
              File.Exists(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\ffmpeg.exe"))
            {
                return true; // File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe");

            }

            MessageBox.Show("ffmpeg not found. Please install for download of higher resolutions",
              "Check for ffmpeg.exe", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return false;

        }



        /// <summary>
        /// download html link
        /// </summary>
        /// <param name="videolink"></param>
        /// <param name="NewPath"></param>
        /// <param name="fpsValue"></param>
        /// <param name="videofilename"></param>
        /// <returns></returns>
        public static string DownloadLink(string videolink, string NewPath, out string videofilename)
        {

            bool _verbose = Settings.Default.verbose;
            bool _formats = Settings.Default.showFormats;

            string output = Settings.Default.output;

            if (string.IsNullOrEmpty(output) && string.IsNullOrEmpty(NewPath))
            {
                output = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            }
            else if (!string.IsNullOrEmpty(NewPath))
            {
                output = NewPath;
            }

            string filename = NativeMethods.GetFullPathFromWindows("youtube-dl.exe");

            if (string.IsNullOrEmpty(filename)/* && string.IsNullOrEmpty(NewPath)*/)
            {
                filename = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe";
            }

            string location = output + "\\%(title)s.%(ext)s";

            ////no cmd window
            ProcessStartInfo ps = new ProcessStartInfo();

            ps.ErrorDialog = false;

            if (!_verbose && !_formats)  
            {
                ps.FileName = filename;

                ps.Arguments = " \"" + videolink + "\" -o \"" + location + "\"";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo

            }

            ps.CreateNoWindow = false; //false; // comment this out
            ps.UseShellExecute = false; // true
            ps.RedirectStandardOutput = false; // false
            ps.RedirectStandardError = false;

            using (Process proc = new Process())
            {
                proc.StartInfo = ps;
                proc.Start();
                proc.WaitForExit();
            }


            return videofilename = output;

        }



        /// <summary>
        /// get name of downloaded video file
        /// </summary>
        /// <param name="output"></param>
        /// <param name="namecell"></param>
        /// <returns>full name of videofile with ext and path</returns>
        static string GetDownloadFileName(string output, string namecell)
        {
            
            string fullName = "";
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(output);
            
            FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("*.*");
         
            foreach (FileInfo foundFile in filesInDir)
            {
                if (ClassHelp.CalculateSimilarity(namecell, foundFile.Name) > 0.8)  //compare only filename with youtube videoname
                {
                    fullName = foundFile.FullName;  //return filename with path
                    break;
                }                   
            }
          
            return fullName;
        }





    }
}
