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
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlaylistEditor.Properties;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace PlaylistEditor
{
    public static class ClassDownload
    {
        private static string videoUrlnew;
        private static string videoTitle;
        private static string audioUrl;
        private static Video VideoInfo;

        private static YoutubeClient _youtube;



        /// <summary>
        /// opens cmd window and downloads video 
        /// </summary>
        /// <param name="videolink">YT video link</param>
        /// <param name="NewPath">folder whee to write</param>
        /// <param name="videofilename">filename of downloaded video</param>
        /// <returns>filename of video in folder</returns>
        public static string DownloadYTLink(string videolink, string NewPath, string fpsValue, out string videofilename)
        {

            string[] height = { "2160", "1440", "1080", "720", "480", "360" };
            string[] combovideo = { "", ",ext!=webm", ",ext=mp4", ",ext=mkv", "novideo" };
            string[] comboaudio = { "", "[ext=m4a]", "[ext=acc]", "[ext=ogg]" };

            int maxres = Settings.Default.maxres;
            int cvideo = Settings.Default.combovideo;
            int caudio = Settings.Default.comboaudio;
            bool _highfps = Settings.Default.fps;

            bool _verbose = Settings.Default.verbose;
            bool _formats = Settings.Default.showFormats;
            bool _allsubs = Settings.Default.allsubs;

            string output = Settings.Default.output;

            string highfps;
            if (_highfps) highfps = "[fps"+fpsValue.Trim()+"]";
            else highfps = "";
            

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
                if (cvideo == 4)  //novideo
                {
                    ps.Arguments = " -f \"bestaudio" + comboaudio[caudio] + "\" \"" + videolink + "\" -o \"" + location + "\"";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
                }
                else
                {
                    //  --restrict-filenames  ??
                    ps.Arguments = " -f \"bestvideo[height<=" + height[maxres] + combovideo[cvideo] + "]" + highfps + "+bestaudio" + comboaudio[caudio] + "/best" + comboaudio[caudio] + "/best\" \"" + videolink + "\" -o \"" + location + "\"";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
                    if (_allsubs) ps.Arguments += " --all-subs --write-auto-sub --sub-lang en";
                }

            }
            else if (_verbose)
            {
                ps.FileName = "CMD";
                // filename = filename.Replace(@"\\", @"\"); 
                // ps.Arguments = "/K youtube-dl.exe -f \"bestvideo[height<=" + height[maxres] + "]+bestaudio[ext=m4a]/best[ext=mp4]/best\" " + videolink + " -o \"" + output + "\"";
                // ps.Arguments = "/K \""+filename.Replace(@"\\", @"\") + "\" -f \"bestvideo[height<=" + height[maxres] + combovideo[cvideo] + "]+bestaudio" + comboaudio[caudio] + "/best" + comboaudio[caudio] + "/best\" \"" + videolink + "\" -o \"" + output + "\" --verbose";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
                if (cvideo == 4)  //novideo
                {
                    ps.Arguments = "/K youtube-dl.exe -f \"bestaudio" + comboaudio[caudio] + "\" \"" + videolink + "\" -o \"" + location + "\"";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo

                }
                else
                {   
                ps.Arguments = "/K youtube-dl.exe -f \"bestvideo[height<=" + height[maxres] + combovideo[cvideo] + "]" + highfps + "+bestaudio" + comboaudio[caudio] + "/best" + comboaudio[caudio] + "/best\" \"" + videolink + "\" -o \"" + location + "\" --verbose";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
                    if (_allsubs) ps.Arguments += " --write-subs --write-auto-sub --sub-lang en";
                }

            }
            else if (_formats)
            {
                ps.FileName = "CMD";              
                ps.Arguments = "/K youtube-dl.exe --list-formats " + videolink;  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
               // if (_allsubs) ps.Arguments += " --list-subs";
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

                return videofilename = "false";
            }

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
    
           
         //   return videofilename = GetDownloadFileName(output, ClassHelp.GetTitle_client(videolink));
            return videofilename = GetDownloadFileName(output, ClassHelp.GetTitle_html(videolink));

        }


        public static string DownloadYTLinkEx(string videolink, string NewPath, string fpsValue, out string videofilename)
        {
            Cursor.Current = Cursors.WaitCursor;

            int maxres = Settings.Default.maxres;  //-> SetVideoQuality
            int cvideo = Settings.Default.combovideo; //-> SetFileContainer .mp4 | .webm

            Task.Run(async () => { await DownloadStream(videolink, NewPath, maxres); }).Wait();  //-> videoUrlnew   audioUrl 

            Cursor.Current = Cursors.Default;


            return videofilename = videoTitle;

        }

        private static async Task DownloadStream(string videoId, string NewPath, int height = 2)
        {

            var youtube = new YoutubeClient();
            var converter = new YoutubeConverter(youtube); // re-using the same client instance for efficiency, not required


            try
            {

                // Get stream manifest
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoId);

                // Select audio stream
                var audioStreamInfo = streamManifest.GetAudio().WithHighestBitrate();

                // Select video stream
                //  var videoStreamInfo = streamManifest.GetVideo().FirstOrDefault(s => s.VideoQualityLabel == "1080p60");
                  var videoStreamInfo = streamManifest.GetVideoOnly()
                    .FirstOrDefault(s => s.VideoQuality <= ClassHelp.SetVideoQuality(height));

                //var videoStreamInfo = streamManifest.GetVideoOnly()
                //                      .Where(s => s.VideoQuality <= ClassHelp.SetVideoQuality(height))
                //                      .Where(t => t.Container == Container.Mp4)
                //                      .Select(h => h.Url).ToList();
                //  var test = streamManifest.get


                // Combine them into a collection
                var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };

                VideoInfo = await youtube.Videos.GetAsync(videoId);  //video info
                videoTitle = (VideoInfo.Title +".mp4").Replace("/", "");
                

                // Download and process them into one file
                await converter.DownloadAndProcessMediaStreamsAsync(streamInfos, videoTitle, "mp4");

            }
            catch (Exception ex)
            {
                MessageBox.Show("error " + ex.Message, "Stream Download", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

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



//        public static string DownloadYTLinkExt(string videolink, string NewPath, out string videofilename)
//        {

//            CmdOutput cmdoutput;

//            cmdoutput = new CmdOutput();





//            string[] height = { "2160", "1440", "1080", "720", "480" };

//            int maxres = Properties.Settings.Default.maxres;
//            string output = Properties.Settings.Default.output;   //ToDo possible bug remove last \
//            if (string.IsNullOrEmpty(output))
//            {
//                output = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

//            }
//            else if (!string.IsNullOrEmpty(NewPath))
//            {
//                output = NewPath;
//            }

//            string filename = NativeMethods.GetFullPathFromWindows("youtube-dl.exe");

//            if (string.IsNullOrEmpty(filename) && string.IsNullOrEmpty(NewPath))
//            {
//                filename = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe";
//            }


//            //  File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe"))
//            // _youtube_dl = true; // File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe");



//            output = output + "\\%(title)s.%(ext)s";
//            //string argument = "-f \"bestvideo[height<=" + height[maxres] + "]+bestaudio\" " + videolink + " -o \"" + output + "\"";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
//            //string command = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe";

//            //   ProcessResult result = await ExecuteShellCommand(command, argument, 10);

//            ////no cmd window
//            ProcessStartInfo ps = new ProcessStartInfo();

//            ps.FileName = filename; // Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe";
//            ps.ErrorDialog = false;
//            // ps.Arguments = "-f \"bestvideo[height<=" + height[maxres] + "]\" -g " + videolink;  //-f "bestvideo[height<=720]" -g 2FcRM-p4koo
//            // string arg = "/K youtube-dl -f "+ '\u0022'+ "bestvideo[height<=" + height[maxres] + "]+bestaudio " + '\u0022' + videolink + " -o "+ '\u0022' + output + '\u0022';
//            //  ps.Arguments = "/K youtube-dl.exe -f \"bestvideo[height<=" + height[maxres] + "]+bestaudio\" " + videolink + " -o \"" + output + "\"";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
//            ps.Arguments = " -f \"bestvideo[height<=" + height[maxres] + "]+bestaudio[ext=m4a]/best[ext=mp4]/best\" \"" + videolink + "\" -o \"" + output + "\"";  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo
//#if DEBUG
//            //ps.FileName = "CMD";
//            //ps.Arguments = "/K youtube-dl.exe -f \"bestvideo[height<=" + height[maxres] + "]+bestaudio\" " + videolink + " -o \"" + output + "\"";
//#endif


//            //  ps.Arguments = arg;
//            ps.CreateNoWindow = true; //false; // comment this out
//            ps.UseShellExecute = false; // true
//            ps.RedirectStandardOutput = true; // false
//            ps.RedirectStandardError = true;
//            //                                   // ps.EnvironmentVariables.Add("VARIABLE1", "1");
//            //                                   // ps.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; // comment this out

//            using (Process proc = new Process())
//            {
//                proc.StartInfo = ps;

//                cmdoutput.Show();




//                //  proc.OutputDataReceived += (sender, a) => Console.WriteLine(a.Data);



//                //    //proc.Exited += new EventHandler(proc_Exited);
//                //    //proc.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_OutputDataReceived);
//                //proc.Start();
//                //proc.BeginOutputReadLine();
//                ////    if (proc != null && !proc.HasExited)
//                //proc.WaitForExit();
//                ////    //proc.BeginOutputReadLine(); // Comment this out

//                /////////////////////////////////////////////////
//                proc.ErrorDataReceived += proc_DataReceived;
//                proc.OutputDataReceived += proc_DataReceived;

//                proc.Start();

//                proc.BeginErrorReadLine();
//                proc.BeginOutputReadLine();

//                proc.WaitForExit();





//            }
//            videofilename = GetDownloadFileName(output, ClassHelp.GetTitle(videolink));
//            return videofilename;


//            //    process.OutputDataReceived += (sender, a) => Console.WriteLine(a.Data);
//            //process.Start();
//            //  process.BeginOutputReadLine();
//            //process.StartInfo.RedirectStandardOutput = true;
//            //process.
//            void proc_DataReceived(object sender, DataReceivedEventArgs e)
//            {
//                string value = "";
//                // output will be in string e.Data
//                // ToDo how to send to form2
//                if (e.Data != null)
//                {
//                    value = e.Data.ToString();
//                    if (value.Contains("ETA"))
//                    {
//                        cmdoutput.textbox_cmdout.Text = cmdoutput.textbox_cmdout.Text.Remove(cmdoutput.textbox_cmdout.Text.LastIndexOf(Environment.NewLine));
//                        //myOutput.textbox_cmdout.Text = value;
//                        cmdoutput.textbox_cmdout.Text += value + Environment.NewLine;
//                    }
//                    else
//                    {
//                        cmdoutput.textbox_cmdout.Text += value + Environment.NewLine;
//                    }


//                }


//            }



//        }


        //public static async Task<ProcessResult> ExecuteShellCommand(string command, string arguments, int timeout)
        //{
        //    var result = new ProcessResult();

        //    using (var process = new Process())
        //    {
                

        //        process.StartInfo.FileName = command;
        //        process.StartInfo.Arguments = arguments;
        //        process.StartInfo.UseShellExecute = false;
        //        process.StartInfo.RedirectStandardInput = true;
        //        process.StartInfo.RedirectStandardOutput = true;  //true
        //        process.StartInfo.RedirectStandardError = true;  //true
        //        process.StartInfo.CreateNoWindow = true;   //true

                

        //        var outputBuilder = new StringBuilder();
        //        var outputCloseEvent = new TaskCompletionSource<bool>();

        //        process.OutputDataReceived += (s, e) =>
        //        {
                    
        //            if (string.IsNullOrEmpty(e.Data))
        //            {
        //                outputCloseEvent.SetResult(true);
        //            }
        //            else
        //            {
        //                outputBuilder.AppendLine(e.Data);
        //            }
        //        };

        //        var errorBuilder = new StringBuilder();
        //        var errorCloseEvent = new TaskCompletionSource<bool>();

        //        process.ErrorDataReceived += (s, e) =>
        //        {
                    
        //            if (string.IsNullOrEmpty(e.Data))
        //            {
        //                errorCloseEvent.SetResult(true);
        //            }
        //            else
        //            {
        //                errorBuilder.AppendLine(e.Data);
        //            }
        //        };

        //        bool isStarted;

        //        try
        //        {
        //            isStarted = process.Start();
        //        }
        //        catch (Exception error)
        //        {
        //            // Usually it occurs when an executable file is not found or is not executable

        //            result.Completed = true;
        //            result.ExitCode = -1;
        //            result.Output = error.Message;

        //            isStarted = false;
        //        }

        //        if (isStarted)
        //        {
        //            // Reads the output stream first and then waits because deadlocks are possible
        //            process.BeginOutputReadLine();
        //            process.BeginErrorReadLine();

        //            // Creates task to wait for process exit using timeout
        //            var waitForExit = WaitForExitAsync(process, timeout);

        //            // Create task to wait for process exit and closing all output streams
        //            var processTask = Task.WhenAll(waitForExit, outputCloseEvent.Task, errorCloseEvent.Task);

        //            // Waits process completion and then checks it was not completed by timeout
        //            if (await Task.WhenAny(Task.Delay(timeout), processTask) == processTask && waitForExit.Result)
        //            {
        //                result.Completed = true;
        //                result.ExitCode = process.ExitCode;

        //                // Adds process output if it was completed with error
        //                if (process.ExitCode != 0)
        //                {
        //                    result.Output = $"{outputBuilder}{errorBuilder}";
        //                }
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    // Kill hung process
        //                    process.Kill();
        //                }
        //                catch
        //                {
        //                }
        //            }
        //        }
        //    }

        //    return result;
        //}


        //private static Task<bool> WaitForExitAsync(Process process, int timeout)
        //{
        //    return Task.Run(() => process.WaitForExit(timeout));
        //}


        //public struct ProcessResult
        //{
        //    public bool Completed;
        //    public int? ExitCode;
        //    public string Output;
        //}



    }
}
