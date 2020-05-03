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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaylistEditor
{
    static class ClassHelp
    {

        /// <summary>
        /// detects if fietype is video or IPTV
        /// </summary>
        /// <param name="filename">filename</param>
        /// <returns>true if IPTV</returns>
        public static bool FileIsIPTV(string filename)
        {
            string line;
           
            using (StreamReader playlistFile = new StreamReader(filename))
            {
                while ((line = playlistFile.ReadLine()) != null)
                {
                    if (line.StartsWith("#EXTM3U"))
                    {
                        return true;  //is IPTV
                    }
                    else if (line.StartsWith("#EXTCPlayListM3U::M3U"))
                    {
                        return false;  //is Video
                    }

                }
                return false;
            }

        }

        /// <summary>
        /// converts AIMP lists to unix version
        /// </summary>
        /// <param name="a_filename">Link with path</param>
        /// <returns>path with nfs</returns>
        public static string ConvertAIPM(string a_filename, string nfs_server)
        {
            string UNCfileName = NativeMethods.UNCPath(a_filename);
            string entryName = "";

            if (UNCfileName.Contains("\\\\"))  // \\192.168.xxx.xxx
            {
                if (!string.IsNullOrEmpty(nfs_server))
                {
                    nfs_server = nfs_server.TrimEnd('/');  //replace last /

                    nfs_server = nfs_server.Replace("/", "\\");

                    if (UNCfileName.Contains(nfs_server))
                    {
                        string rest = UNCfileName.Replace(nfs_server, "");
                        rest = rest.Replace("\\\\\\", "\\");

                        entryName = "nfs://" + nfs_server + rest;
                        entryName = entryName.Replace("\\", "/");

                    }
                }

                return entryName;
            }
            return a_filename;
        }





        public static bool ActivateApp(string processName)
        {
            Process[] p = Process.GetProcessesByName(processName);

            // Activate the first application we find with this name
            if (p.Count() > 0)
            {
                NativeMethods.SetForegroundWindow(p[0].MainWindowHandle);
                return true;
            }
            return false;

        }

        /// <summary>
        /// get the title from youtube videos links
        /// </summary>
        /// <param name="url"></param>
        /// <returns>title of YT video</returns>
        //public static string GetTitle(string url)
        //{
        //    var api = $"http://youtube.com/get_video_info?video_id={GetArgs(url, "v", '?')}";
        //    var retvalue = "";

        //    try
        //    {
        //        retvalue = GetArgs(new WebClient().DownloadString(api), "title", '&');
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("Error to get YouTube title", "YouTube error", MessageBoxButtons.OK);
        //    }

        //    return retvalue;

        //}

        /// <summary>
        /// gets YouTube video title from html <title></title> tag
        /// </summary>
        /// <param name="url">youtube link url</param>
        /// <returns>title of youtube video</returns>
        public static string GetTitle_html(string url)
        {
            // https://stackoverflow.com/questions/329307/how-to-get-website-title-from-c-sharp

           
            string title = "";

            try
            {
                WebClient x = new WebClient();
                x.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) " +
                    "AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36");
                string source = x.DownloadString(url);
                
                title = Regex.Match(source, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                   RegexOptions.IgnoreCase).Groups["Title"].Value;

                byte[] bytes = System.Text.Encoding.Default.GetBytes(title);
                title = System.Text.Encoding.UTF8.GetString(bytes);
               
                title = WebUtility.HtmlDecode(title);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to get YouTube title " + ex.Message, "YouTube error", MessageBoxButtons.OK);
            }

            return title.Replace(" - YouTube", "");  //response "YouTube" if no video avaliable
        }

        


        //public static string GetTitle_html(string link)
        //{
        //    try
        //    {
        //        WebClient wc = new WebClient();
        //        string html = wc.DownloadString(link);

        //        Regex x = new Regex(@"<title>(.*)</title>");
        //        MatchCollection m = x.Matches(html);

        //        if (m.Count > 0)
        //        {
        //            return m[0].Value.Replace("<title>", "").Replace("</title>", "");
        //        }
        //        else
        //            return "";
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Could not connect. Error:" + ex.Message);
        //        return "";
        //    }
        //}


        /// <summary>
        /// gets the argumnts from a youtube video link
        /// </summary>
        /// <param name="args"></param>
        /// <param name="key"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        //private static string GetArgs(string args, string key, char query)
        //{
        //    var iqs = args.IndexOf(query);
        //    return iqs == -1
        //        ? string.Empty : HttpUtility.ParseQueryString(iqs < args.Length - 1
        //            ? args.Substring(iqs + 1) : string.Empty)[key];
        //}

        /// <summary>
        /// function to get the path of installed vlc
        /// </summary>
        /// <returns>path or empty</returns>
        public static string GetVlcPath()
        {
           
            object line;
          //  string softwareinstallpath = string.Empty;
            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (var baseKey = Microsoft.Win32.RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (var key = baseKey.OpenSubKey(registry_key))
                {
                    foreach (string subkey_name in key.GetSubKeyNames())
                    {
                        using (var subKey = key.OpenSubKey(subkey_name))
                        {
                            line = subKey.GetValue("DisplayName");
                            if (line != null && (line.ToString().ToUpper().Contains("VLC")))
                            {

                                string VlcPath = subKey.GetValue("InstallLocation").ToString();

                                Properties.Settings.Default.vlcpath = VlcPath;
                                Properties.Settings.Default.Save();
                                return VlcPath;

                            }
                        }
                    }
                }
            }
            return "";  //no vlc found

        }

        /// <summary>
        /// byte to string / string to byte
        /// </summary>
        /// <param name="arr"></param>
        /// <returns>string</returns>
        public static string ByteArrayToString(byte[] arr)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(arr);
        }

        public static byte[] StringToByteArray(string str)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetBytes(str);
        }

      
        /// <summary>
        /// gets command line arg with youtube-dl.exe for DASH video
        /// </summary>
        /// <param name="videolink"></param>
        /// <returns>command line arg or false if no DASH avaliable</returns>
        public static string GetVlcDashArg(string videolink)
        {
            string[] height = { "2160", "1440", "1080", "720", "480", "360" };
           
            int maxres = Properties.Settings.Default.maxres;
           
            string videoUrl = "";

            // vlc "https://video.stream" --input-slave="https://audio.stream"

            string filename = NativeMethods.GetFullPathFromWindows("youtube-dl.exe");

            if (string.IsNullOrEmpty(filename))
            {
                filename = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe";
            }



            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            //no cmd window
            ProcessStartInfo ps = new ProcessStartInfo();
            ps.FileName = filename;// Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe";
            ps.ErrorDialog = false;
            //ps.Arguments = "-f \"bestvideo[height<=" + height[maxres] + "]\" -g " + videolink;  //-f "bestvideo[height<=720]" -g 2FcRM-p4koo
            // ps.Arguments = "-f bestaudio -g " + videolink;
            // error keys with -  -> -- seperator
            ps.Arguments = "-f \"bestvideo[height<=" + height[maxres] + "]+bestaudio\" -g -- " + videolink;  //-f "bestvideo[height<=720]+bestaudio" -g 2FcRM-p4koo

            ps.CreateNoWindow = true; // comment this out
            ps.UseShellExecute = false; // true
            ps.RedirectStandardOutput = true; // false
            ps.EnvironmentVariables.Add("VARIABLE1", "1");
            ps.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; // comment this out

            using (Process proc = new Process())
            {
                proc.StartInfo = ps;
                //proc.Exited += new EventHandler(proc_Exited);
                //proc.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(proc_OutputDataReceived);
                proc.Start();
                StreamReader sr = proc.StandardOutput;
                videoUrl = sr.ReadToEnd();
                proc.WaitForExit();
                //proc.BeginOutputReadLine(); // Comment this out
            }

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;

            if (string.IsNullOrEmpty(videoUrl)) return "false";  //sumtingwong
            else if (videoUrl.Contains("manifest")) return "nodash";  //no dash

            //   string[] splitUrl = videoUrl.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            string[] splitUrl = videoUrl.Split(new[] { "\r\n", "\r", "\n" },StringSplitOptions.None);

            string result = splitUrl[0] + " --input-slave=" + splitUrl[1];

            return result;  //test


        }

        /// <summary>
        /// runs vlc with commandline args
        /// </summary>
        /// <param name="vlcArg"></param>
        public static void RunVlc (string vlcArg)
        {
            string vlcpath = Properties.Settings.Default.vlcpath;
            ProcessStartInfo ps = new ProcessStartInfo();
            ps.FileName = vlcpath + "\\" + "vlc.exe";
            ps.ErrorDialog = false;
            ps.Arguments = vlcArg + " --no-video-title-show "   ;  //test
            ps.Arguments += " --no-qt-error-dialogs";


            ps.CreateNoWindow = true;
            ps.UseShellExecute = false;

            ps.RedirectStandardOutput = true;
            ps.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            using (Process proc = new Process())
            {
                proc.StartInfo = ps;

                proc.Start();
                //  proc.WaitForExit();

            }
        }

        public static async void PopupForm(string label, string color, int delay)
        {
            await PopupDelay(label, color, delay);
        }

   


        /// <summary>
        /// async thread counter to show popup form
        /// </summary>
        /// <param name="item"></param>
        public static async  Task PopupDelay(string label, string color, int delay)
        {
            popup2 pop = new popup2();
            pop.Show();
            pop.label1.Text = label;
            pop.color(color);
            
            await Task.Delay(delay);
           
            pop.Close();            
        }

        /// <summary>
        /// Levenstein compare
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int LevenshteinDistance(string source, string target)
        {
            // degenerate cases
            if (source == target) return 0;
            if (source.Length == 0) return target.Length;
            if (target.Length == 0) return source.Length;

            // create two work vectors of integer distances
            int[] v0 = new int[target.Length + 1];
            int[] v1 = new int[target.Length + 1];

            // initialize v0 (the previous row of distances)
            // this row is A[0][i]: edit distance for an empty s
            // the distance is just the number of characters to delete from t
            for (int i = 0; i < v0.Length; i++)
                v0[i] = i;

            for (int i = 0; i < source.Length; i++)
            {
                // calculate v1 (current row distances) from the previous row v0

                // first element of v1 is A[i+1][0]
                //   edit distance is delete (i+1) chars from s to match empty t
                v1[0] = i + 1;

                // use formula to fill in the rest of the row
                for (int j = 0; j < target.Length; j++)
                {
                    var cost = (source[i] == target[j]) ? 0 : 1;
                    v1[j + 1] = Math.Min(v1[j] + 1, Math.Min(v0[j + 1] + 1, v0[j] + cost));
                }

                // copy v1 (current row) to v0 (previous row) for next iteration
                for (int j = 0; j < v0.Length; j++)
                    v0[j] = v1[j];
            }

            return v1[target.Length];
        }

        /// <summary>
        /// Calculate percentage similarity of two strings
        /// <param name="source">Source String to Compare with</param>
        /// <param name="target">Targeted String to Compare</param>
        /// <returns>Return Similarity between two strings from 0 to 1.0</returns>
        /// </summary>
        public static double CalculateSimilarity(string source, string target)
        {
            // soure:original name, target:filename
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;
            if (source == target.Substring(0, target.Length - 4)) return 1.0; //cut .ext
            if (source == target.Substring(0, target.Length - 5)) return 1.0; //cut .webm

            int stepsToSame = LevenshteinDistance(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }

        /// <summary>
        /// check if youtube-dl is availiable
        /// </summary>
        /// <returns>bool</returns>
        public static bool YT_dl()
        {
            if (!string.IsNullOrEmpty(NativeMethods.GetFullPathFromWindows("youtube-dl.exe")) ||
                File.Exists(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe"))
            {
                return true; // File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe");

            }
            return false;
        }
          


    /// <summary>
    /// Launch the folder view
    /// </summary>
    /// <param name="p_Filename">The fully qualified path and filename of the target to open as highlighted by explorer</param>
    public static bool LaunchFolderView(string p_Filename)
        {
            bool l_result = false;

            // Check the file exists
            if (File.Exists(p_Filename))
            {
                // Check the folder we get from the file exists
                // this function would just get "C:\Hello" from
                // an input of "C:\Hello\World.txt"
                string l_folder = Path.GetDirectoryName(p_Filename);//FileSystemHelpers.GetPathFromQualifiedPath(p_Filename);

                // Check the folder exists
                if (Directory.Exists(l_folder))
                {
                    try
                    {
                        // Start a new process for explorer
                        // in this location     
                        ProcessStartInfo l_psi = new ProcessStartInfo();
                        l_psi.FileName = "explorer.exe";
                         l_psi.Arguments = string.Format("/root,{0} /select,{1}", l_folder, p_Filename);
                        // l_psi.Arguments = string.Format("/root,{0}",l_folder);
                      //  l_psi.Arguments = string.Format(" {0} /select,{1}", l_folder, p_Filename);
                        l_psi.UseShellExecute = true;

                        Process l_newProcess = new Process();
                        l_newProcess.StartInfo = l_psi;
                        l_newProcess.Start();

                        // No error
                        l_result = true;
                    }
                    catch (Exception exception)
                    {
                        throw new Exception("Error in 'LaunchFolderView'.", exception);
                    }
                }
            }

            return l_result;
        }


        /// <summary>
        /// Move from temp path to target path
        /// </summary>
        /// <param name="sourcefilename">full path filename</param>
        /// <param name="targetPath">target path</param>
        public static string FileMove(string sourcefilename, string targetPath)
        {           
            string targetFilename = targetPath + "\\" + Path.GetFileName(sourcefilename);

            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (!File.Exists(sourcefilename))
                {
                    // This statement ensures that the file is created,
                    // but the handle is not kept.
                    using (FileStream fs = File.Create(sourcefilename)) { }
                }

                //ToDo ask to overwrite, show file compare. 
                // Ensure that the target does not exist.
                if (File.Exists(targetFilename))
                {
                    switch (MessageBox.Show("File exists, overwrite?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                    {
                        case DialogResult.Yes:
                             File.Delete(targetFilename);
                            break;

                        case DialogResult.No:
                            return "error";
                           // break;
                    }
                }
                   

                // Move the file.
                File.Move(sourcefilename, targetFilename);
                Console.WriteLine("{0} was moved to {1}.", sourcefilename, targetFilename);
                
                // See if the original exists now.
                if (File.Exists(sourcefilename))
                {
                    Console.WriteLine("The original file still exists, which is unexpected.");
                }
                else
                {
                    Console.WriteLine("The original file no longer exists, which is expected.");
                }

            }
           

            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            // Set cursor as hourglass
            Cursor.Current = Cursors.Default;

            return targetFilename;
        }


        public static bool MyFileExists(string uri, int timeout)
        {
            if (string.IsNullOrEmpty(uri)) return false;

            var task = new Task<bool>(() =>
            {
                var fi = new FileInfo(uri);
                return fi.Exists;
            });
            task.Start();
            //return task.Result;
            return task.Wait(timeout) && task.Result;
        }


        /// <summary>
        /// checks if Diectory exists with timeout
        /// </summary>
        /// <param name="openpath">path</param>
        /// <param name="timeout">timeout in ms</param>
        /// <returns>true/false</returns>
        public static bool MyDirectoryExists(string openpath, int timeout)
        {
            var task = new Task<bool>(() => { var info = new DirectoryInfo(openpath); return info.Exists; });
            task.Start();

            return task.Wait(timeout) && task.Result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public delegate bool DirectoryExistsDelegate(string folder);
        /// <summary>
        /// checks for network dir with timeout
        /// </summary>
        /// <param name="path"></param>
        /// <param name="millisecondsTimeout"></param>
        /// <returns></returns>
        public static bool DirectoryExistsTimeout(string path, int millisecondsTimeout)
        {
            try
            {
                DirectoryExistsDelegate callback = new DirectoryExistsDelegate(Directory.Exists);
                IAsyncResult result = callback.BeginInvoke(path, null, null);

                if (result.AsyncWaitHandle.WaitOne(millisecondsTimeout, false))
                {
                    return callback.EndInvoke(result);
                }
                else
                {
                    callback.EndInvoke(result);  // Needed to terminate thread?

                    return false;
                }
            }

            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// method to get first 1k of stream data to check if stream alive
        /// </summary>
        /// <param name="uri">URL to check</param>
        /// <returns>bool</returns>
        public static bool CheckStream(string uri)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
                req.Timeout = 5000; //set the timeout
                req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36";

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                StreamReader sr = new StreamReader(resp.GetResponseStream());
                // results = sr.ReadToEnd();
                char[] buffer = new char[1024];
                int results1 = sr.Read(buffer, 0, 1023);
                sr.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        public static bool IsDriveReady(string serverName)
        {
            //https://stackoverflow.com/questions/1232953/speed-up-file-exists-for-non-existing-network-shares
            // ***  SET YOUR TIMEOUT HERE  ***     
            int timeout = 5;    // 5 seconds 

            try
            {
                System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
                System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
                options.DontFragment = true;
                // Enter a valid ip address     
                string ipAddressOrHostName = serverName;
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = System.Text.Encoding.ASCII.GetBytes(data);
                System.Net.NetworkInformation.PingReply reply = pingSender.Send(ipAddressOrHostName, timeout, buffer, options);
                return (reply.Status == System.Net.NetworkInformation.IPStatus.Success);
            }
            catch (Exception)
            {

            }

            return false;

        }

        public static bool LoadItem(this Stack<object[][]> instance, DataGridView dgv)
        {
            if (instance.Count == 0)
            {
                return true;
            }
            object[][] rows = instance.Peek();
            return !ItemEquals(rows, dgv.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow).ToArray());
        }

        public static bool ItemEquals(this object[][] instance, DataGridViewRow[] dgvRows)
        {
            if (instance.Count() != dgvRows.Count())
            {
                return false;
            }
            return !Enumerable.Range(0, instance.GetLength(0)).Any(x => !instance[x].SequenceEqual(dgvRows[x].Cells.Cast<DataGridViewCell>().Select(c => c.Value).ToArray()));
        }

        public static bool CheckClipboard()
        {
            DataObject o = (DataObject)Clipboard.GetDataObject();

            if (Clipboard.ContainsText())
            {
                try
                {

                    string[] pastedRows = System.Text.RegularExpressions.Regex.Split(o.GetData(DataFormats.UnicodeText).ToString().TrimEnd("\r\n".ToCharArray()), "\r\n");
                    string[] pastedRowCells = pastedRows[0].Split(new char[] { '\t' });

                    if (pastedRowCells.Length == 3) return true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Paste operation failed. " + ex.Message, "Copy/Paste", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            return false;
        }

        /// <summary>
        /// send ping to kodi IP
        /// </summary>
        /// <param name="hostUri"></param>
        /// <param name="portNumber"></param>
        /// <returns>tur or false</returns>
        //public static bool PingHost(string hostUri, int portNumber)
        //{
        //    try
        //    {
        //        using (var client = new TcpClient(hostUri, portNumber))
        //            return true;
        //    }
        //    catch (SocketException ex)
        //    {
        //        MessageBox.Show("Error pinging host:'" + hostUri + ":" + portNumber.ToString() + "'" +ex.Message);
        //        return false;
        //    }
        //}
    }

 
}
