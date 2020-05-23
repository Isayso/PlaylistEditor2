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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaylistEditor
{
    public static class ClassDownload
    {
        //private static string videoUrlnew;
        //private static string videoTitle;
        //private static string audioUrl;
        //private static Video VideoInfo;

        //private static YoutubeClient _youtube;



        /// <summary>
        /// opens cmd window and downloads video 
        /// </summary>
        /// <param name="videolink">YT video link</param>
        /// <param name="NewPath">folder whee to write</param>
        /// <param name="videofilename">filename of downloaded video</param>
        /// <returns>filename of video in folder</returns>
        //public static string DownloadYTLink(string videolink, string NewPath, string fpsValue, out string videofilename)
        //{

        //    string[] height = { "2160", "1440", "1080", "720", "480", "360" };
        //    string[] combovideo = { "", ",ext!=webm", ",ext=mp4", ",ext=mkv", "novideo" };
        //    string[] comboaudio = { "", "[ext=m4a]", "[ext=acc]", "[ext=ogg]" };

        //    int maxres = Settings.Default.maxres;
        //    int cvideo = Settings.Default.combovideo;
        //    int caudio = Settings.Default.comboaudio;
        //    bool _highfps = Settings.Default.fps;

        //    bool _verbose = Settings.Default.verbose;
        //    bool _formats = Settings.Default.showFormats;
        //    bool _allsubs = Settings.Default.allsubs;

        //    string output = Settings.Default.output;

        //    string highfps;
        //    if (_highfps) highfps = "[fps"+fpsValue.Trim()+"]";
        //    else highfps = "";
            

        //    if (string.IsNullOrEmpty(output) && string.IsNullOrEmpty(NewPath))  
        //    {
        //        output = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        //    }
        //    else if (!string.IsNullOrEmpty(NewPath))
        //    {
        //        output = NewPath;
        //    }

        //    string filename = NativeMethods.GetFullPathFromWindows("youtube-dl.exe");

        //    if (string.IsNullOrEmpty(filename)/* && string.IsNullOrEmpty(NewPath)*/)
        //    {
        //        filename = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe";
        //    }


        //  //  File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe"))
        //                // _youtube_dl = true; // File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe");

         

        //    string location = output + "\\%(title)s.%(ext)s";
          
        //    ////no cmd window
        //    ProcessStartInfo ps = new ProcessStartInfo();

        //    ps.ErrorDialog = false;

        //    if (!_verbose && !_formats)
        //    {
        //        ps.FileName = filename;
        //        if (cvideo == 4)  //novideo
        //        {
        //            ps.Arguments = " -f \"bestaudio" + comboaudio[caudio] + "\" \"" + videolink + "\" -o \"" + location + "\"";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
        //        }
        //        else
        //        {
        //            //  --restrict-filenames  ??
        //            ps.Arguments = " -f \"bestvideo[height<=" + height[maxres] + combovideo[cvideo] + "]" + highfps + "+bestaudio" + comboaudio[caudio] + "/best" + comboaudio[caudio] + "/best\" \"" + videolink + "\" -o \"" + location + "\"";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
        //            if (_allsubs) ps.Arguments += " --all-subs --write-auto-sub --sub-lang en";
        //        }

        //    }
        //    else if (_verbose)
        //    {
        //        ps.FileName = "CMD";
        //        // filename = filename.Replace(@"\\", @"\"); 
        //        // ps.Arguments = "/K youtube-dl.exe -f \"bestvideo[height<=" + height[maxres] + "]+bestaudio[ext=m4a]/best[ext=mp4]/best\" " + videolink + " -o \"" + output + "\"";
        //        // ps.Arguments = "/K \""+filename.Replace(@"\\", @"\") + "\" -f \"bestvideo[height<=" + height[maxres] + combovideo[cvideo] + "]+bestaudio" + comboaudio[caudio] + "/best" + comboaudio[caudio] + "/best\" \"" + videolink + "\" -o \"" + output + "\" --verbose";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
        //        if (cvideo == 4)  //novideo
        //        {
        //            ps.Arguments = "/K youtube-dl.exe -f \"bestaudio" + comboaudio[caudio] + "\" \"" + videolink + "\" -o \"" + location + "\"";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo

        //        }
        //        else
        //        {   
        //        ps.Arguments = "/K youtube-dl.exe -f \"bestvideo[height<=" + height[maxres] + combovideo[cvideo] + "]" + highfps + "+bestaudio" + comboaudio[caudio] + "/best" + comboaudio[caudio] + "/best\" \"" + videolink + "\" -o \"" + location + "\" --verbose";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
        //            if (_allsubs) ps.Arguments += " --write-subs --write-auto-sub --sub-lang en";
        //        }

        //    }
        //    else if (_formats)
        //    {
        //        ps.FileName = "CMD";              
        //        ps.Arguments = "/K youtube-dl.exe --list-formats " + videolink;  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
        //       // if (_allsubs) ps.Arguments += " --list-subs";
        //        ps.CreateNoWindow = false; //false; // comment this out
        //        ps.UseShellExecute = false; // true
        //        ps.RedirectStandardOutput = false; // false
        //        ps.RedirectStandardError = false;

        //        using (Process proc = new Process())
        //        {
        //            proc.StartInfo = ps;
        //            proc.Start();                   
        //            proc.WaitForExit();                   
        //        }

        //        return videofilename = "false";
        //    }

        //    ps.CreateNoWindow = false; //false; // comment this out
        //    ps.UseShellExecute = false; // true
        //    ps.RedirectStandardOutput = false; // false
        //    ps.RedirectStandardError = false;
      
        //    using (Process proc = new Process())
        //    {
        //        proc.StartInfo = ps;
        //    proc.Start();
        //      //  proc.BeginOutputReadLine();
        //        //    if (proc != null && !proc.HasExited)
        //    proc.WaitForExit();
        //        //    //proc.BeginOutputReadLine(); // Comment this out
        //    }
    
           
        // //   return videofilename = GetDownloadFileName(output, ClassHelp.GetTitle_client(videolink));
        //    return videofilename = GetDownloadFileName(output, ClassHelp.GetTitle_html(videolink));

        //}


        public static string DownloadYTLinkEx(string videolink, string NewPath, out string videofilename)
        {
            Cursor.Current = Cursors.WaitCursor;


            int maxres = Settings.Default.maxres;  //-> SetVideoQuality
            int cvideo = Settings.Default.combovideo; //-> SetFileContainer .mp4 | .webm

            if (!CheckForFfmpeg() && maxres >= 720) return videofilename = "error";


            Task.Run(async () => { await ClassYTExplode.DownloadStream(videolink, NewPath, maxres, cvideo); }).Wait();  //-> videoUrlnew   audioUrl 

            Cursor.Current = Cursors.Default;


            return videofilename = ClassYTExplode.videoTitle;

        }

        /// <summary>
        /// check if ffmpeg.exe is there
        /// </summary>
        /// <returns></returns>
        private static bool CheckForFfmpeg()
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

            //string[] height = { "2160", "1440", "1080", "720", "480", "360" };
            //string[] combovideo = { "", ",ext!=webm", ",ext=mp4", ",ext=mkv", "novideo" };
            //string[] comboaudio = { "", "[ext=m4a]", "[ext=acc]", "[ext=ogg]" };

            //int maxres = Settings.Default.maxres;
            //int cvideo = Settings.Default.combovideo;
            //int caudio = Settings.Default.comboaudio;
      //      bool _highfps = Settings.Default.fps;

            bool _verbose = Settings.Default.verbose;
            bool _formats = Settings.Default.showFormats;
            //bool _allsubs = Settings.Default.allsubs;

            string output = Settings.Default.output;

          //  string highfps;
            //if (_highfps) highfps = "[fps" + fpsValue.Trim() + "]";
            //else highfps = "";


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


            //  File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe"))
            // _youtube_dl = true; // File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe");



            string location = output + "\\%(title)s.%(ext)s";

            ////no cmd window
            ProcessStartInfo ps = new ProcessStartInfo();

            ps.ErrorDialog = false;

            if (!_verbose && !_formats)  
            {
                ps.FileName = filename;

                ps.Arguments = " \"" + videolink + "\" -o \"" + location + "\"";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo


                //if (cvideo == 4)  //novideo
                //{
                //    ps.Arguments = " -f \"bestaudio" + comboaudio[caudio] + "\" \"" + videolink + "\" -o \"" + location + "\"";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
                //}
                //else
                //{
                //    //  --restrict-filenames  ??
                //    ps.Arguments = " -f \"bestvideo[height<=" + height[maxres] + combovideo[cvideo] + "]" + highfps + "+bestaudio" + comboaudio[caudio] + "/best" + comboaudio[caudio] + "/best\" \"" + videolink + "\" -o \"" + location + "\"";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
                //    if (_allsubs) ps.Arguments += " --all-subs --write-auto-sub --sub-lang en";
                //}

            }
            //else if (_verbose)
            //{
            //    ps.FileName = "CMD";
            //    // filename = filename.Replace(@"\\", @"\"); 
            //    // ps.Arguments = "/K youtube-dl.exe -f \"bestvideo[height<=" + height[maxres] + "]+bestaudio[ext=m4a]/best[ext=mp4]/best\" " + videolink + " -o \"" + output + "\"";
            //    // ps.Arguments = "/K \""+filename.Replace(@"\\", @"\") + "\" -f \"bestvideo[height<=" + height[maxres] + combovideo[cvideo] + "]+bestaudio" + comboaudio[caudio] + "/best" + comboaudio[caudio] + "/best\" \"" + videolink + "\" -o \"" + output + "\" --verbose";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
            //    if (cvideo == 4)  //novideo
            //    {
            //        ps.Arguments = "/K youtube-dl.exe -f \"bestaudio" + comboaudio[caudio] + "\" \"" + videolink + "\" -o \"" + location + "\"";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo

            //    }
            //    else
            //    {
            //        ps.Arguments = "/K youtube-dl.exe -f \"bestvideo[height<=" + height[maxres] + combovideo[cvideo] + "]" + highfps + "+bestaudio" + comboaudio[caudio] + "/best" + comboaudio[caudio] + "/best\" \"" + videolink + "\" -o \"" + location + "\" --verbose";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
            //        if (_allsubs) ps.Arguments += " --write-subs --write-auto-sub --sub-lang en";
            //    }

            //}
            //else if (_formats)
            //{
            //    ps.FileName = "CMD";
            //    ps.Arguments = "/K youtube-dl.exe --list-formats " + videolink;  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
            //                                                                     // if (_allsubs) ps.Arguments += " --list-subs";
            //    ps.CreateNoWindow = false; //false; // comment this out
            //    ps.UseShellExecute = false; // true
            //    ps.RedirectStandardOutput = false; // false
            //    ps.RedirectStandardError = false;

            //    using (Process proc = new Process())
            //    {
            //        proc.StartInfo = ps;
            //        proc.Start();
            //        proc.WaitForExit();
            //    }

            //    return videofilename = "false";
            //}

            ps.CreateNoWindow = false; //false; // comment this out
            ps.UseShellExecute = false; // true
            ps.RedirectStandardOutput = false; // false
            ps.RedirectStandardError = false;

            using (Process proc = new Process())
            {
                proc.StartInfo = ps;
                proc.Start();
                //  proc.BeginOutputReadLine();
                //    if (proc != null && !proc.HasExited)
                proc.WaitForExit();
                //    //proc.BeginOutputReadLine(); // Comment this out
            }


            return videofilename = output;
          //  return videofilename = GetDownloadFileName(output, ClassHelp.GetTitle_html(videolink));

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
