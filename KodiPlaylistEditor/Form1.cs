﻿/*
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
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
using static PlaylistEditor.ClassHelp;

//ToDo drag&drop
//ToDo move block of rows

//move to top with ctrl up/dowm

//option search in all fields 

//drag drop rows?
//license change

// ToDo list play with vlc?  https://forum.videolan.org/viewtopic.php?t=65006 OK
//if saved undo must make green






namespace PlaylistEditor
{

    public partial class Form1 : Form
    {
        //for undo/redo
        Stack<object[][]> undoStack = new Stack<object[][]>();
        Stack<object[][]> redoStack = new Stack<object[][]>();

        Boolean ignore = false;

        CancellationTokenSource tokenSource;


        bool isModified = false;

        //zoom of fonts
        public float zoomf = 1;
        //  private static readonly int ROWHEIGHT = 25;
        private const float FONTSIZE = 9.163636F;

        public List<string> data = new List<string>(); //Datalist file
        public string fileName = "";
        public string line, ytPluginLink = "";
        public SortableBindingList<PlayEntry> entries = new SortableBindingList<PlayEntry>();
        const int mActionHotKeyID = 1;  //var for key hook listener
        const int mActionHotKeyID2 = 2;

        public List<String> mruItems = new List<String>();  //string to display in UI
        public List<Label> labels = new List<Label>();  //labels for mruItems

        static Configuration appdata;
        public string mruFile = "";
        public static readonly string MRULISTFILE = "MRUList.txt";


        private string path;

        public string nfs_server = Settings.Default.server;  //IPs from settings
        public bool rDrive = Settings.Default.replaceDrive;  //todo not us
        public string rpi_ip = Settings.Default.rpi;
        public string downloadlink = "";
        public string dialogPath = "";
        public static string output = Settings.Default.output;

        public string keyValue = "";  //highlight string

        private const string YTPLUGIN = "plugin://plugin.video.youtube/play/?video_id=";
        private const string YTURL = "https://www.youtube.com/watch?v=";
        private const int COLWIDTH = 500;

        public bool _isIt = true;
        public bool _foundtext = false;
        public bool _taglocal = false;
        public bool _taglink = false;
        public bool _vlcfound = false;
        public bool _savenow = false;
        public bool _mark = false;

        public static string videoTitle;

        public static Video VideoInfo;

        private static YoutubeClient _youtube;
        public double _progress;


        public string[] videotypes = new[] {  ".m4v", ".3g2", ".3gp",".nsv",".tp",".ts",".ty",".strm",".pls",".rm",
                    ".rmvb",".mpd",".m3u",".m3u8",".ifo",".mov",".qt",".divx",".xvid",".bivx",".vob",".nrg",".pva",
                    ".wmv",".asf",".asx",".ogm",".m2v",".avi",".dat",".mpg",".mpeg",".mp4",".mkv",".mk3d",".avc",
                    ".vp3",".svq3",".nuv",".viv",".dv",".fli",".flv",".001",".wpl",".vdr",".dvr-ms",".xsp",".mts",
                    ".m2t",".m2ts",".evo",".ogv",".sdp",".avs",".rec",".url",".pxml",".vc1",".h264",".rcv",".rss",
                    ".mpls",".webm",".bdmv",".wtv",".trp",".f4v" };


        string vlcpath = Settings.Default.vlcpath;
        public bool useDash = Settings.Default.useDash;

        //  https://www.codeproject.com/articles/811035/drag-and-move-rows-in-datagridview-control

        public SortableBindingList<PlayEntry> GetList()
        {
            return entries;
        }


        public Form1()
        {
            InitializeComponent();

            //appdata path to write mru file
            appdata = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            mruFile = appdata.FilePath.Replace("user.config", "") + MRULISTFILE;

            if (Settings.Default.cleanexit == false)
            {
                Settings.Default.Upgrade();
                // Settings.Default.Reset();  //if an unusual shutdown occured, reset settings
                PopupForm("Last Settings loaded! Please control settings!", "red", 3000);

            }


            this.Text = String.Format("Playlist Editor" + " v{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0, 5));

#if DEBUG
            //  Clipboard.Clear();
            this.Text = String.Format("PlaylistEditor DEBUG" + " v{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0, 5));

#endif

            //   _youtube_dl = YT_dl();


            //if (Debugger.IsAttached)
            //   Settings.Default.Reset();

            //if (Debugger.IsAttached)
            //{
            //  //  Settings.Default.combodown = 0;
            //    Settings.Default.Save();
            //}
            comboBox1.SelectedIndex = Settings.Default.maxres;
            comboBox_audio.SelectedIndex = Settings.Default.comboaudio;
            comboBox_video.SelectedIndex = Settings.Default.combovideo;
            checkBox_rlink.Checked = Settings.Default.replaceDrive;

            // dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ShowCellToolTips = false;
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            dataGridView1.MultiSelect = true;
            //  dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;//   .EditOnF2;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;//   .EditOnF2;

            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.RowHeadersVisible = false;


            comboBox_download.Items.Clear();

            foreach (object item in Settings.Default.combopathlist)
            {
                comboBox_download.Items.Add(item);
            }
            comboBox_download.SelectedIndex = 0;

            RWSettings("read1st");  //read values from settings

            //dataGridView1.Font = new Font("Microsoft Sans Serif", 9.163636F, dataGridView1.Font.Style);
            //read mru
            mruItems.Clear();

            if (!File.Exists(mruFile))
            {
                for (int i = 1; i < 6; i++)   //file 1-5
                {
                    mruItems.Add("file" + i.ToString());
                }
                File.WriteAllLines(mruFile, mruItems);  //overwrite
            }

            mruItems = File.ReadAllLines(mruFile).ToList();

            var spec_key = Settings.Default.specKey;
            var spec_key2 = Settings.Default.specKey2;
            var hotlabel = Settings.Default.hotkey;
            var hotlabel2 = Settings.Default.hotkey2;

            //Modifier keys codes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8  must be added
            //   RegisterHotKey(this.Handle, mActionHotKeyID, 1, (int)Keys.Y);  //ALT-Y
            NativeMethods.RegisterHotKey(this.Handle, mActionHotKeyID, spec_key, hotlabel);  //ALT-Y

            if (Settings.Default.kodi_hotkey)
                NativeMethods.RegisterHotKey(this.Handle, mActionHotKeyID2, spec_key2, hotlabel2);  //WIN-Y


            plabel_Filename.Text = "";
            button_revert.Visible = false;
            button_cancel.Visible = false;
            downloadYTFileToolStripMenuItem.Visible = true;
            button_download.Visible = true;


            _vlcfound = !string.IsNullOrEmpty(vlcpath) ? true : false;

            if (_vlcfound)
            {
                button_vlc.Visible = true;
            }
            else if (!_vlcfound)  //first run
            {
                vlcpath = GetVlcPath();
                if (string.IsNullOrEmpty(vlcpath))
                {
                    // button_vlc.Visible = false; //no vlc installed
                    _vlcfound = false;
                }
                else { _vlcfound = true; }
            }

            dataGridView1.DoubleBuffered(true);



            //cmdline in index 0 path of app
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1) //drag drop path oder file on Icon
            {
                plabel_Filename.Text = args[1];
                if (importDataset(args[1], false)) { };
                button_revert.Visible = true;
                // dataGridView1.Columns[0].Width = COLWIDTH;  // Name column 
            }
        }


        /// <summary>
        /// listener to CTRL-Y hotkey for import of youtube link from clipboard
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x0312 && m.WParam.ToInt32() == mActionHotKeyID)
            {
                // key pressed matches our listener
                // copy to clipboard -> crtl-y -> youtube url -> parse titel from url -> cut strings -> add line -> add entries
                //get from clipboard
                //detect wich app has focus, send ctrl-c

                //Process[] local = Process.GetProcessesByName("vivaldi");
                //if (local.Length > 0)
                //{
                //    Process p = local[0];
                //    IntPtr h = p.MainWindowHandle;
                //    NativeMethods.SetForegroundWindow(h);
                //    p.WaitForInputIdle();
                //    SendKeys.SendWait("^(c)");
                //    p.WaitForInputIdle();
                //    if (Clipboard.ContainsText())
                //    {
                //        MessageBox.Show(Clipboard.GetText());
                //    }
                //}

                //if (ActivateApp("Vivaldi"))
                //{
                //    //SendKeys.SendWait("^(a)");
                //    SendKeys.SendWait("^(c)");
                //    //if (Clipboard.ContainsText())
                //    //{
                //    //    MessageBox.Show(Clipboard.GetText());
                //    //}
                //}




                IDataObject yLink = Clipboard.GetDataObject();

                string yt_Link = (String)yLink.GetData(DataFormats.Text);

                // if (string.IsNullOrEmpty(yt_Link)) return; //clipboard empty Goodbye
                if (string.IsNullOrEmpty(yt_Link) || yt_Link.Contains("search_query=")) return; //clipboard empty Goodbye


                // possible YT links:
                //https://www.youtube.com/watch?v=KZ2aFOTf_4Y&list=PLZ1f3amS4y1e4UsI2PgslUM3xssFUHQuG&index=2
                //https://www.youtube.com/watch?v=KZ2aFOTf_4Y
                // KZ2aFOTf_4Y
                //
                //embedded: 
                //https://youtu.be/1zrejG-WI3U
                //https://www.youtube.com/embed/xE146-LsbyQ?wmode=opaque
                //https://www.youtube.com/embed/gIOvCiy2fKs
                //https://www.youtube-nocookie.com/embed/AmXgH_zdv6k?feature=oembed
                //https://www.youtube.com/embed/1zrejG-WI3U?version=3&rel=1&fs=1&autohide=2&showsearch=0&showinfo=1&iv_load_policy=1&wmode=transparent
                //
                // channel https://www.youtube.com/channel/UCpSV2QTmd34FfoFmbZ0O7Sw
                //https://www.youtube.com/watch?v=0yAiPIOugv4
                // 
                // search query
                //https://www.youtube.com/results?search_query=ariana+honda+stage


                if (yt_Link.Contains(".youtube.com") || yt_Link.Contains("www.youtube-nocookie.com") || yt_Link.Contains("youtu.be"))
                {
                    if ((yt_Link.Contains("embed") || yt_Link.Contains("youtu.be/")) && !yt_Link.Contains("=youtu.be/"))  //variant embed link
                    {
                        string[] key_em = yt_Link.Split('?');
                        key_em[0] = key_em[0].Split('/').Last();
                        ytPluginLink = YTPLUGIN + key_em[0];
                        // yt_Link = "https://www.youtube.com/watch?v=" + key_em[0];

                    }
                    //https://www.youtube.com/watch?time_continue=16&v=UaTYYk3HxOc&feature=emb_logo
                    else if (yt_Link.Contains("time_continue"))
                    {
                        string[] key = yt_Link.Split('=');  //variant normal or YT playlist link
                        if (key.Length > 1)     //if channel has no '='
                        {
                            if (key[2].Contains('&'))
                                key[2] = key[2].Split('&').First();

                            //  ytPluginLink = YTPLUGIN + key[1];
                            ytPluginLink = YTPLUGIN + key[2];

                        }
                    }



                    else
                    {
                        string[] key = yt_Link.Split('=');  //variant normal or YT playlist link
                        if (key.Length > 1)     //if channel has no '='
                        {
                            if (key[1].Contains('&'))
                                key[1] = key[1].Split('&').First();

                            ytPluginLink = YTPLUGIN + key[1];

                        }
                    }


                    if (string.IsNullOrEmpty(ytPluginLink))
                    {
                        ytPluginLink = "Link N/A";
                    }


                    // Is Data Text?

                    if (yLink.GetDataPresent(DataFormats.Text) && ytPluginLink != "Link N/A")
                    {
                        var url = (String)yLink.GetData(DataFormats.Text);  //yLink Clipboarddata
                        if (url.Contains("music.youtube"))
                        {
                            string[] key = yt_Link.Split('=');  //variant normal or YT playlist link
                            if (key.Length > 1)     //if channel has no '='
                            {
                                if (key[1].Contains('&'))
                                    key[1] = key[1].Split('&').First();

                                url = YTURL + key[1];

                            }
                        }
                        // var url = (String)yt_Link.GetData(DataFormats.Text);
                        // string name = GetTitle(url);
                        //  string name = GetTitle_html(url);
                        string name = GetTitle_client(url);  //new client
                        //  if (string.IsNullOrEmpty(name)) name = GetTitle_new(yUrl_search);
                        //string name = GetTitle(yUrl);
#if DEBUG
                        Console.WriteLine(name);
#endif
                        if (!string.IsNullOrEmpty(name))
                        {
                            name = name.Replace("#", " ");  //kodi doesn't like #
                            name = name.Replace(":", " -");  //youtube-dl doesn't like :

                            if (dataGridView1.RowCount > 0)
                            {
                                entries.Add(new PlayEntry(Name: name, Link: ytPluginLink));
                                dataGridView1.Rows[entries.Count - 1].Selected = true;
                                dataGridView1.FirstDisplayedScrollingRowIndex = entries.Count - 1;

                            }
                            else
                            {
                                dataGridView1.DataSource = entries; //writes grid
                                data.Add("Name");
                                data.Add("Link");
                                entries.Add(new PlayEntry(Name: name, Link: ytPluginLink));

                                label_central.SendToBack();
                            }

                            PopupForm("YouTube Link added", "blue", 2000);


                            if (_taglink) button_check.PerformClick(); //grid gets pushed up and changing color

                            // DataGridView1_CellValidated(null, null); if(undoStack.Count>1) ShowReUnDo(0);

                            toSave(true);
                        }
                    }

                    else
                    {
                        PopupForm("Wrong input. Use full YouTube link", "red", 2000);
                        //  MessageBox.Show("Wrong input. Use full YouTube link.");
                    }
                }
                else if ((yt_Link.StartsWith("http") || yt_Link.StartsWith("\\\\") || yt_Link.Contains(@":\"))
                    && videotypes.Any(yt_Link.EndsWith))  //option http
                {
                    var url = yt_Link;//  (String)yLink.GetData(DataFormats.Text);  //yLink Clipboarddata
                    string name = ""; // url.Split('/').Last();

                    //string name = GetTitle_html(url);
                    if (url.StartsWith("\\\\"))
                    {
                        name = url.Split('\\').Last();
                        ytPluginLink = "nfs:" + url.Replace("\\", "/").Trim();
                    }
                    else if (url.Contains(@":\"))
                    {
                        name = url.Split('\\').Last();
                        ytPluginLink = url;
                    }
                    else
                    {
                        name = url.Split('/').Last();
                        ytPluginLink = url;
                    }

#if DEBUG
                    Console.WriteLine(name);
#endif
                    if (!string.IsNullOrEmpty(name))
                    {
                        name = name.Replace("#", " ");  //kodi doesn't like #
                        name = name.Replace(":", " -");  //youtube-dl doesn't like :

                        if (dataGridView1.RowCount > 0)
                        {
                            entries.Add(new PlayEntry(Name: name, Link: ytPluginLink));
                            dataGridView1.Rows[entries.Count - 1].Selected = true;
                            dataGridView1.FirstDisplayedScrollingRowIndex = entries.Count - 1;
                            //  DataGridView1_CellValidated(null, null);

                        }
                        else
                        {
                            dataGridView1.DataSource = entries; //writes grid
                            data.Add("Name");
                            data.Add("Link");
                            entries.Add(new PlayEntry(Name: name, Link: ytPluginLink));
                            // DataGridView1_CellValidated(null, null);

                        }

                        PopupForm("html Link added", "blue", 2000);

                        if (_taglink) button_check.PerformClick(); //grid gets pushed up and changing color

                        //DataGridView1_CellValidated(null, null); if (undoStack.Count > 1) ShowReUnDo(0);

                        toSave(true);
                    }
                }


            }

            // kodi hotkey
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == mActionHotKeyID2)
            {
                IDataObject kLink = Clipboard.GetDataObject();

                string kodi_Link = (String)kLink.GetData(DataFormats.Text);

                if (kodi_Link.Contains(".youtube.com") || kodi_Link.Contains("www.youtube-nocookie.com") || kodi_Link.Contains("youtu.be"))
                {
                    if (kodi_Link.Contains("embed") || kodi_Link.Contains("youtu.be"))  //variant embed link
                    {
                        string[] key_em = kodi_Link.Split('?');
                        key_em[0] = key_em[0].Split('/').Last();
                        ytPluginLink = YTPLUGIN + key_em[0];
                        // yt_Link = "https://www.youtube.com/watch?v=" + key_em[0];

                    }
                    else
                    {
                        string[] key = kodi_Link.Split('=');  //variant normal or YT playlist link
                        if (key.Length > 1)     //if channel has no '='
                        {
                            if (key[1].Contains('&'))
                                key[1] = key[1].Split('&').First();

                            ytPluginLink = YTPLUGIN + key[1];

                        }
                    }

                }
                else if (kodi_Link.StartsWith("http") && videotypes.Any(kodi_Link.EndsWith))
                {
                    ytPluginLink = kodi_Link;
                }


                string jLink = "{ \"jsonrpc\":\"2.0\",\"method\":\"Player.Open\",\"params\":{ \"item\":{ \"file\":\"" + ytPluginLink + "\"} },\"id\":0}";

                ClassKodi.Run(jLink);  //don't know exactly what I wan't to do with the bool


            }

            base.WndProc(ref m);
        }



        private void labelMRU_Click(object sender, EventArgs e)
        {
            //check if saved
            if (isModified == true && dataGridView1.RowCount > 0)
            {
                DialogResult dialogSave = MessageBox.Show("Do you want to save your current playlist?",
                "Save Playlist", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogSave == DialogResult.Yes)
                {
                    button_save.PerformClick();
                    isModified = false;
                }
                if (dialogSave == DialogResult.Cancel) { panelMRU.Visible = false; return; }
            }

            Label obj = sender as Label;

            if (obj.Text.StartsWith("file") || string.IsNullOrEmpty(obj.Text)) return;  //default list on startup

            if (obj.Name == "label1") SortMruItems(1);
            if (obj.Name == "label2") SortMruItems(2);
            if (obj.Name == "label3") SortMruItems(3);
            if (obj.Name == "label4") SortMruItems(4);
            if (obj.Name == "label5") SortMruItems(5);

            //  undoStack.Clear(); redoStack.Clear(); ShowReUnDo(0); toSave(false);//reset stacks
            toSave(false, true);

            if (importDataset(mruItems[0], false))
            {
                File.WriteAllLines(mruFile, mruItems);  //overwrite
                button_revert.Visible = true;
                panelMRU.Visible = false;
            }

        }

        /// <summary>
        /// Sort MRU List
        /// </summary>
        /// <param name="place">position of label</param>
        private void SortMruItems(int place)
        {
            string tmp = mruItems[place - 1];
            for (int i = place - 1; i > 0; i--)
            {
                mruItems[i] = mruItems[i - 1];
            }
            mruItems[0] = tmp;
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Save combobox Items

            Settings.Default.combopathlist.Clear();

            foreach (object item in comboBox_download.Items)
            {
                Settings.Default.combopathlist.Add(item.ToString());
            }


            Settings.Default.combodown = 0;  //to avoid false start
            Settings.Default.cleanexit = true; //clean exit
            Settings.Default.F2Location = this.Location;
            Settings.Default.F2Size = this.Size;

            Settings.Default.Save();

            NativeMethods.UnregisterHotKey(this.Handle, mActionHotKeyID);


            if (isModified == true && dataGridView1.RowCount > 0)
            {
                DialogResult dialogSave = MessageBox.Show("Do you want to save your current playlist?",
                "Save Playlist", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogSave == DialogResult.Yes)
                {
                    button_save.PerformClick();
                    isModified = false;
                }
                if (dialogSave == DialogResult.Cancel) e.Cancel = true;

            }

            File.WriteAllLines(mruFile, mruItems);  //overwrite


            //  Application.Exit();
        }


        #region Button

        private void button_open_Click(object sender, EventArgs e)
        {
            if (panelMRU.Visible)
            {
                panelMRU.Visible = false;
            }
            else
            {   //read mru
                mruItems.Clear();

                mruItems = File.ReadAllLines(mruFile).ToList();

                panelMRU.Visible = true;
            }
        }

        private void label_open_Click(object sender, EventArgs e)  //open from panel2
        {
            if (isModified == true && dataGridView1.RowCount > 0)
            {
                DialogResult dialogSave = MessageBox.Show("Do you want to save your current playlist?",
                "Save Playlist", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogSave == DialogResult.Yes)
                {
                    button_save.PerformClick();
                    isModified = false;
                }
                if (dialogSave == DialogResult.Cancel) { panelMRU.Visible = false; return; }

            }

            // Set cursor as hourglass
            Cursor.Current = Cursors.WaitCursor;

            var openpath = Settings.Default.openpath;
            if (!string.IsNullOrEmpty(openpath) && !MyDirectoryExists(openpath, 4000))
                openpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";


            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = openpath;
                openFileDialog.Filter = "m3u files|*.m3u|All files|*.*";
                //openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = false;
                //openFileDialog1.CheckFileExists = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // undoStack.Clear(); redoStack.Clear(); toSave(false); ShowReUnDo(0);//reset stacks
                    toSave(false, true);

                    if (importDataset(openFileDialog.FileName, false))
                    {
                        dataGridView1.Columns[0].Width = COLWIDTH;  // Name column  

                        if (!TestDup(openFileDialog.FileName))
                        {
                            for (int i = mruItems.Count - 1; i > 0; i--)
                            {
                                mruItems[i] = mruItems[i - 1];
                            }
                            //string tmp = openFileDialog.FileName;
                            mruItems[0] = openFileDialog.FileName;
                        }

                        File.WriteAllLines(mruFile, mruItems);  //overwrite
                    }


                    button_revert.Visible = true;

                }
                else  //cancel
                {
                    panelMRU.Visible = false;
                    return;
                }

                Settings.Default.openpath = Path.GetDirectoryName(openFileDialog.FileName);
                Settings.Default.Save();
            }


            if (_taglocal) button_tag.PerformClick();   //toDo dataGridView1.ClearSelection(); better???
                                                        //  if (_taglink) button_check.PerformClick();

            button_check.BackColor = Color.MidnightBlue;
            _taglink = false;

            Cursor.Current = Cursors.Default;
            panelMRU.Visible = false;

        }

        /// <summary>
        /// test if new filename is already in list
        /// </summary>
        /// <param name="newfile">new filename</param>
        /// <returns>true/false</returns>
        private bool TestDup(string newfile)
        {
            int x = 1;
            foreach (string _str in mruItems)
            {
                if (newfile.Contains(_str) && !string.IsNullOrEmpty(_str))
                {
                    SortMruItems(x);
                    return true;  //want to know x
                }
                x++;
            }
            return false;
        }


        private void button_Info_Click(object sender, EventArgs e)
        {
            if (_taglocal) button_tag.PerformClick();

            using (AboutBox1 a = new AboutBox1())
            {
                a.ShowDialog();  //centre position on Infoform
            }

        }

        private void button_settings_Click(object sender, EventArgs e)
        {
            if (_taglocal) button_tag.PerformClick();

            //Save combobox Items
            Settings.Default.combopathlist.Clear();

            foreach (object item in comboBox_download.Items)
            {
                Settings.Default.combopathlist.Add(item.ToString());
            }
            Settings.Default.Save();

            using (settings s = new settings())
            {
                s.ShowDialog();
            }

            comboBox1.SelectedIndex = Settings.Default.maxres;

            comboBox_download.Items.Clear();

            foreach (object item in Settings.Default.combopathlist)
            {
                comboBox_download.Items.Add(item);
            }
            comboBox_download.SelectedIndex = 0;

        }


        private void button_vlc_Click(object sender, EventArgs e)
        {
            string param = "";
            useDash = Settings.Default.useDash;

            if (!_vlcfound)
            {
                vlcpath = GetVlcPath();
                if (string.IsNullOrEmpty(vlcpath))
                {
                    PopupForm("VLC player not found", "red", 3000);
                    // _vlcfound = true;
                }
                return;
            }

            // dataGridView1.ClearSelection();

            if (dataGridView1.RowCount > 0 && _vlcfound)
            {

                //   string dashfile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe";//  Directory.GetCurrentDirectory() + "\\youtube-dl.exe";

                // vlc "file:///\\192.168.178.100\\Multimedia\Konzerte\Ariana Grande\Ariana Grande_ No Tears Left to Cry (TV Debut).mp4"
                // nfs://192.168.178.100/Multimedia/Konzerte/Ariana Grande/Ariana Grande - Breathin (Live on Ellen _ 2018) (1080p_30fps_H264-128kbit_AAC).mp4
                // vlc https://www.youtube.com/watch?v=PjdzVSfxa1o

                //  MessageBox.Show(dashfile +",");

                string playcell = dataGridView1.CurrentRow.Cells[1].Value.ToString();

                if (playcell.Contains("plugin") && playcell.Contains("youtube"))
                {
                    string[] key = playcell.Split('=');  //variant normal or YT playlist link
                    if (key.Length > 1)     //if link has no '='
                    {
                        if (!useDash /*|| !_youtube_dl*/)  // normal res or no youtube_dl
                        {
                            param = YTURL + key[1];
                        }
                        else
                        {
                            //  param = GetVlcDashArg(key[1]);
                            param = GetVlcDashArg2(key[1]);  //youtube-dl delete

                            if (param == "false")
                            {
                                 return;  //no fallback to no dash
                                //param = YTURL + key[1];
                            }
                            if (param == "nodash")
                            {
                                param = YTURL + key[1];
                            }
                        }
                    }
                }
                else if (playcell.Contains("nfs"))
                {
                    //nfs: replace with file:///   
                    //replace / with \
                    playcell = playcell.Replace("/", "\\").Replace("nfs:", "file:///");

                    param = "\"" + playcell + "\"";
                }
                else if (playcell.Contains(":\\") || playcell.Contains("\\\\"))
                {
                    // $ youtube-dl - j iVAgTiBrrDA | jq '.formats[0].manifest_url' "https://manifest.googlevideo.com/..."
                    // playcell = playcell.Replace("/", "\\").Replace("nfs:", "file:///");
                    // playcell = playcell.Replace("nfs:", "file:///");

                    param = "\"" + playcell + "\"";
                }
                else if (playcell.Contains("\\\\"))     //  \\nas
                {
                    param = "\"" + playcell + "\"";
                }
                else if (playcell.StartsWith("http"))     //  html option
                {
                    param = " " + playcell;
                }

                RunVlc(param);

            }
        }

        /// <summary>
        /// import of playlist entries
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="append">false/true for append</param>
        public bool importDataset(string filename, bool append)
        {

            Cursor.Current = Cursors.WaitCursor;
            bool _aimp = false;

            if (!MyFileExists(filename, 5000))
            {
                PopupForm("File not found", "red", 1500);
                return false;
            }

            if (FileIsIPTV(filename))
            {
                DialogResult dialogSave = MessageBox.Show("Playlist in AIMP Format?",
                "Import Playlist", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogSave == DialogResult.No)
                {
                    // MessageBox.Show("File has IPTV format! Use PlaylistEditorTV. ");
                    PopupForm(" If File has IPTV format, use PlaylistEditorTV", "red", 3500);
                    return false;
                }

                _aimp = true;
            }

            StreamReader playlistFile = new StreamReader(filename);
            if (!append)
            {
                entries.Clear();
                plabel_Filename.Text = filename;
            }

            dataGridView1.DataSource = entries;

            //string endchar;  //true for Unix files
            //TryDetectNewline(filename, out endchar);

            //if (endchar.Equals("\n"))
            //{
            //    isUnixFile = true;
            //}

            while ((line = playlistFile.ReadLine()) != null)
            {
                if (line.StartsWith("#EXTINF"))
                {
                    data.Add("Name");
                    data.Add("Link");

                    string lastPart = line.Split(',').Last().Replace("#", " ");  //ToDo bug if text contains ","
                    data[0] = lastPart;

                    if (data[0] == "")
                    {
                        data[0] = "Name N/A";
                    }
                    continue;
                }

                else if (line.Contains("//") || line.Contains("/storage") || line.Contains(":\\") || line.StartsWith("\\\\"))  //2. row after plugin
                {
                    if (_aimp && checkBox_unix.Checked && rDrive)
                        data[1] = ConvertAIPM(line, nfs_server);
                    else
                        data[1] = line;
                }
                else
                {
                    continue;  //if file has irregular linefeed.
                }

                if (data.Count > 0)
                {
                    try
                    {
                        entries.Add(new PlayEntry(Name: data[0].Trim(), Link: data[1].Trim()));

                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        //  MessageBox.Show("An entry has been omitted due to its incorrect format");
                        PopupForm("An entry has been omitted due to its incorrect format", "red", 2000);
                        continue;
                    }
                }
                data.Clear();  //dataset delete

            }
            playlistFile.Close();  //bug  file write denied on H:  

            // dataGridView1.BringToFront();
            label_central.SendToBack();

            Cursor.Current = Cursors.Default;

            if (entries.Count == 0)
            {
                MessageBox.Show("Wrong file structure! ", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_aimp) toSave(true);
            else toSave(false);


            dataGridView1.Rows[0].Selected = true;

            return true;
        }



        private void button_delLine_Click(object sender, EventArgs e)
        {
            // dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = true;
            //
            //   dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = true;


            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    entries.RemoveAt(row.Index);
                }

                toSave(true);
            }

            if (_taglocal) button_tag.PerformClick();
            //  dataGridView1.Refresh();  //to save on undo stack
            // DataGridView1_CellValidated(null, null);
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            // if (_taglocal) button_tag.PerformClick();

            Cursor.Current = Cursors.WaitCursor;

            saveFileDialog1.FileName = plabel_Filename.Text;

            if ((ModifierKeys == Keys.Shift || _savenow) && !string.IsNullOrEmpty(plabel_Filename.Text)
                && MyDirectoryExists(Path.GetDirectoryName(plabel_Filename.Text), 4000))
            {
                saveFileDialog1.FileName = plabel_Filename.Text;
                // ((Control)sender).Hide();

                //check if path is avaliable to avoid network timeout
                ////var savepath = Path.GetDirectoryName(plabel_Filename.Text);
                //if (!string.IsNullOrEmpty(plabel_Filename.Text)
                //    && !MyDirectoryExists(Path.GetDirectoryName(plabel_Filename.Text), 4000))
                //{
                //    string docpath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                //    saveFileDialog1.FileName = docpath + "\\" + Path.GetFileName(plabel_Filename.Text);
                //}


                using (StreamWriter file = new StreamWriter(saveFileDialog1.FileName, false /*, Encoding.UTF8*/))   //false: file ovewrite
                {
                    // if (isUnixFile) file.NewLine = "\n";  //unix style LF
                    file.NewLine = "\n";  //win  LF 
                    file.WriteLine("#EXTCPlayListM3U::M3U");

                    for (int i = 0; i < entries.Count; i++)
                    {
                        if (string.IsNullOrEmpty(entries[i].Name) && string.IsNullOrEmpty(entries[i].Link)) continue;
                        // # remove, "," remove
                        entries[i].Name = entries[i].Name ?? string.Empty;  //replace NULL with empty
                        entries[i].Link = entries[i].Link ?? string.Empty;

                        entries[i].Name = entries[i].Name.Replace("#", " ").Replace(",", " ").Replace(":", " -");
                        file.WriteLine("#EXTINF:0," + entries[i].Name);
                        file.WriteLine(entries[i].Link);
                    }
                }
                // undoStack.Clear(); redoStack.Clear(); toSave(false); ShowReUnDo(0);//reset stacks
                toSave(false, true);
                button_revert.Visible = true;
                _savenow = false;


                PopupForm("Playlist Saved", "green", 1500);

            }
            else if (saveFileDialog1.ShowDialog() == DialogResult.OK)  //open file dialog
            {

                plabel_Filename.Text = saveFileDialog1.FileName;

                using (StreamWriter file = new StreamWriter(saveFileDialog1.FileName, false /*, Encoding.UTF8*/))   //false: file ovewrite
                {
                    // if (isUnixFile) file.NewLine = "\n";  //unix style LF
                    file.NewLine = "\n";
                    file.WriteLine("#EXTCPlayListM3U::M3U");

                    for (int i = 0; i < entries.Count; i++)
                    {
                        entries[i].Name = entries[i].Name.Replace("#", " ").Replace(",", " ").Replace(":", " -");
                        file.WriteLine("#EXTINF:0," + entries[i].Name);  //ToDo # remove?
                        file.WriteLine(entries[i].Link);
                    }
                }
                // undoStack.Clear(); redoStack.Clear(); toSave(false); ShowReUnDo(0);//reset stacks
                toSave(false, true);


                string tmp = saveFileDialog1.FileName;
                for (int i = 5 - 1; i > 0; i--)
                {
                    mruItems[i] = mruItems[i - 1];
                }
                mruItems[0] = tmp;


                File.WriteAllLines(mruFile, mruItems);  //overwrite


                button_revert.Visible = true;

                Cursor.Current = Cursors.Default;
            }
        }



        private void button_moveUp_Click(object sender, EventArgs e)
        {
            if (_taglocal) button_tag.PerformClick();
            if (_taglink) button_check.PerformClick();
            MoveLine(-1);

            //test code for multiple
            //var rows = dataGridView1.SelectedRows;
            //for (int i = 0; i < rows.Count; i++)
            //{

            //}

        }

        private void button_moveDown_Click(object sender, EventArgs e)
        {
            if (_taglocal) button_tag.PerformClick();
            if (_taglink) button_check.PerformClick();
            MoveLine(1);
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (_taglocal) button_tag.PerformClick();
            if (_taglink) button_check.PerformClick();

            if (dataGridView1.RowCount > 0)
            {
                // dataGridView1.Rows.Insert(dataGridView1.CurrentCell.RowIndex);
                entries.Add(new PlayEntry(Name: "Name", Link: "Link"));
                dataGridView1.Rows[entries.Count - 1].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = entries.Count - 1;

            }
            else
            {
                dataGridView1.DataSource = entries;
                data.Add("Name");
                data.Add("Link");
                entries.Add(new PlayEntry(Name: "Name", Link: "Link"));

                label_central.SendToBack();

            }
            //DataGridView1_CellValidated(null, null);

            toSave(true);
        }





        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            if (_taglocal) button_tag.PerformClick();
            //  if (_taglink) button_check.PerformClick();

            //int rowIndexOfItemUnderMouseToDrop;
            //Point clientPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));
            //rowIndexOfItemUnderMouseToDrop = dataGridView1.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            //if (e.Effect == DragDropEffects.Move)
            //{
            //    dataGridView1.Rows.RemoveAt(rowIndexFromMouseDown);
            //    dataGridView1.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rw);
            //}





            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                nfs_server = Settings.Default.server;
                // rpi_ip = Properties.Settings.Default.rpi;

                rDrive = Settings.Default.replaceDrive;  //bool if replace necessary
                string entryName = "ERROR: Windows path or unknown IP";
                string dirName, shortName, driveName, extName, UNCfileName;

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                foreach (string fileName in files)
                {

                    this.path = fileName;

                    dirName = Path.GetDirectoryName(fileName);
                    shortName = Path.GetFileName(fileName);
                    driveName = Path.GetPathRoot(fileName);
                    extName = Path.GetExtension(fileName);

                    UNCfileName = NativeMethods.UNCPath(path);

                    if (extName.Equals(".m3u"))
                    {
                        button_revert.Visible = true;

                        //ToDo more than one .m3u file??
                        if (dataGridView1.RowCount == 0)
                        {
                            if (importDataset(fileName, false))
                            {
                                dataGridView1.Columns[0].Width = COLWIDTH;  // Name column 

                                if (!TestDup(fileName))
                                {
                                    for (int i = 5 - 1; i > 0; i--)
                                    {
                                        mruItems[i] = mruItems[i - 1];
                                    }
                                    mruItems[0] = fileName;
                                }
                                File.WriteAllLines(mruFile, mruItems);  //overwrite
                            }
                            break;
                        }
                        else  //imoprt and add
                        {
                            importDataset(fileName, true);

                            // dataGridView1.Columns[0].Width = 500;  // Name column 
                            toSave(true);
                            break;
                        }
                    }

                    toSave(true);

                    //  entryName = "";
                    if (checkBox_unix.Checked && rDrive)  //unix and replace drive true
                    {
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


                            if (!string.IsNullOrEmpty(rpi_ip))
                            {
                                rpi_ip = rpi_ip.Replace("/", "\\");

                                if (UNCfileName.Contains(rpi_ip))
                                {
                                    string rest = UNCfileName.Replace(rpi_ip, "");
                                    rest = rest.Replace("\\\\\\", "\\");


                                    entryName = "/storage/" + rest;
                                    entryName = entryName.Replace("\\Videos", "videos").Replace("\\", "/"); //bug in 1.9.3

                                }
                            }
                        }
                    }
                    else
                    {
                        entryName = fileName;
                    }



                    if (dataGridView1.RowCount > 0)
                    {
                        entries.Add(new PlayEntry(Name: shortName, Link: entryName));
                        dataGridView1.Rows[entries.Count - 1].Selected = true;
                        dataGridView1.FirstDisplayedScrollingRowIndex = entries.Count - 1;
                    }
                    else
                    {
                        dataGridView1.DataSource = entries;
                        data.Add("Name");
                        data.Add("Link");
                        entries.Add(new PlayEntry(Name: shortName, Link: entryName));
                    }
                }

                //if shift pressed, do merge

                if (ModifierKeys == Keys.Shift)
                {
                    button_dup.PerformClick();
                    button_delLine.PerformClick();
                }
            }

            label_central.SendToBack();
            // DataGridView1_CellValidated(null, null);


            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            //Point clientPoint = dataGridView1.PointToClient(new Point(e.X, e.Y));

            //// Get the row index of the item the mouse is below. 
            //rowIndexOfItemUnderMouseToDrop = dataGridView1.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            //// If the drag operation was a move then remove and insert the row.
            //if (e.Effect == DragDropEffects.Move)
            //{
            //    DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
            //    dataGridView1.Rows.RemoveAt(rowIndexFromMouseDown);
            //    dataGridView1.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);

            //}



        }

        private void button_del_all_Click(object sender, EventArgs e)
        {

            if (_taglocal) button_tag.PerformClick();
            if (_taglink) button_check.PerformClick();

            if (dataGridView1.RowCount > 0)
            {
                switch (MessageBox.Show("Delete List?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.None))  //MessageBoxIcon.Exclamation makes sound
                {
                    case DialogResult.Yes:
                        // "Yes" processing
                        entries.Clear();
                        toSave(false);
                        plabel_Filename.Text = "";
                        button_revert.Visible = false;
                        break;

                    case DialogResult.No:
                        // "No" processing
                        break;
                }
            }
        }

        private void button_revert_Click(object sender, EventArgs e)
        {
            if (_taglocal) button_tag.PerformClick();
            if (_taglink) button_check.PerformClick();
            //message box -> delete all -> open filename
            switch (MessageBox.Show("Reload File?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.None))
            {
                case DialogResult.Yes:
                    //undoStack.Clear(); redoStack.Clear(); toSave(false); ShowReUnDo(0);//reset stacks
                    toSave(false, true);
                    importDataset(plabel_Filename.Text, false);

                    break;

                case DialogResult.No:

                    break;
            }

        }
        #endregion

        #region RightClick
        private void cutTSMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0) return;

            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            //copy selection to whatever
            if (dataGridView1.CurrentCell.Value != null && dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {

                if (!_foundtext)
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = true;

                try
                {
                    // Add the selection to the clipboard.

                    Clipboard.SetDataObject(this.dataGridView1.GetClipboardContent());
#if DEBUG
                    Console.WriteLine(Clipboard.GetText());   //Name[tab]Link[CR][LF]
#endif
                    // button_delLine.PerformClick();
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            entries.RemoveAt(row.Index);
                        }
                        toSave(true);
                    }

                }
                catch (System.Runtime.InteropServices.ExternalException ex)
                {
                    PopupForm("Copy/paste operation failed", "red", 2000);
#if DEBUG
                    MessageBox.Show("Copy/paste operation failed. " + ex.Message, "Copy/Paste", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                }
            }

        }

        private void copyTSMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0 || dataGridView1.IsCurrentCellInEditMode == true) return;

            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            //copy selection to whatever
            if (dataGridView1.CurrentCell.Value != null && dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {

                if (!_foundtext)
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = true;

                try
                {
                    // Add the selection to the clipboard.

                    Clipboard.SetDataObject(this.dataGridView1.GetClipboardContent());
#if DEBUG
                    Console.WriteLine(Clipboard.GetText());   //Name[tab]Link[CR][LF]
#endif
                }
                catch (System.Runtime.InteropServices.ExternalException ex)
                {
                    //  MessageBox.Show("The Clipboard could not be accessed. Please try again.");
                    PopupForm("Copy/paste operation failed", "red", 2000);
#if DEBUG
                    MessageBox.Show("Copy/paste operation failed. " + ex.Message, "Copy/Paste", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                }
                catch (Exception ex)
                {
                    PopupForm("Copy/paste operation failed", "red", 2000);
#if DEBUG
                    MessageBox.Show("Copy/paste2 operation failed. " + ex.Message, "Copy/Paste", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#endif
                }
            }
        }

        private void pasteTSMenuItem_Click(object sender, EventArgs e)
        {
            // https://stackoverflow.com/questions/2089689/row-copy-paste-functionality-in-datagridviewwindows-application

            if (dataGridView1.RowCount == 0)
            {
                dataGridView1.DataSource = entries;
                data.Add("Name");
                data.Add("Link");
                undoStack.Clear(); redoStack.Clear(); ShowReUnDo(0);//reset stacks
                label_central.SendToBack();
            }

            DataObject o = (DataObject)Clipboard.GetDataObject();


            if (Clipboard.ContainsText())
            {

                try
                {
                    string[] pastedRows = Regex.Split(o.GetData(DataFormats.UnicodeText).ToString().TrimEnd("\r\n".ToCharArray()), "\r\n");
                    foreach (string pastedRow in pastedRows)
                    {
                        string[] pastedRowCells = pastedRow.Split(new char[] { '\t' });

                        if (pastedRowCells.Length == 1) return;  //for copy paste only one cell

                        for (int i = 0; i < pastedRowCells.Length; i++)
                        {
                            if (pastedRowCells[i] != "")
                            {
                                //ToDo remove empty rows??
                                //bug cannot cut /import last row  wenn celle selectiert war?  
                                entries.Add(new PlayEntry(Name: pastedRowCells[i], Link: pastedRowCells[i + 1]));
                                i++;
                            }
                        }
                    }
                    toSave(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Paste operation failed. " + ex.Message, "Copy/Paste", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private async void playTSMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0) return;
            string jLink;
            bool once = true;

            if (dataGridView1.SelectedRows.Count > 1)
            {
                //  foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                foreach (DataGridViewRow row in dataGridView1.GetSelectedRows())  //top down
                {
                    if (once)
                    {
                        jLink = dataGridView1.Rows[row.Index].Cells[1].Value.ToString();
                        jLink = "{ \"jsonrpc\":\"2.0\",\"method\":\"Player.Open\",\"params\":{ \"item\":{ \"file\":\"" + jLink + "\"} },\"id\":0}";

                        //  if (!await ClassKodi.Run2(jLink)) continue;  //don't know exactly what I wan't to do with the bool

                        _ = await ClassKodi.Run(jLink);

                        Thread.Sleep(4000);  //Kodi needs a delay between Player.open and Playlist.Add, Internet speed dependent?

                        once = false;
                        continue;
                    }


                    jLink = dataGridView1.Rows[row.Index].Cells[1].Value.ToString();
                    jLink = "{ \"id\":0,\"jsonrpc\":\"2.0\",\"method\":\"Playlist.Add\",\"params\": {\"item\":{\"file\":\"" + jLink + "\"},\"playlistid\":1}}";  //OK

                    if (!await ClassKodi.Run(jLink)) continue;

                    //x = await ClassKodi.Run(jLink);
                    //if (!x) break;
                }
            }
            else
            {
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = true;
                jLink = dataGridView1.CurrentRow.Cells[1].Value.ToString();

                jLink = "{ \"jsonrpc\":\"2.0\",\"method\":\"Player.Open\",\"params\":{ \"item\":{ \"file\":\"" + jLink + "\"} },\"id\":0}";

                //  if (PingHost(rpi_ip,22))
                _ = await ClassKodi.Run(jLink);

                // if (!await ClassKodi.Run(jLink)) break;
            }

        }

        private async void queueTSMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0) return;

            string jLink;
            if (dataGridView1.SelectedRows.Count > 1)
            {
                //  foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                foreach (DataGridViewRow row in dataGridView1.GetSelectedRows())  //top down
                {

                    jLink = dataGridView1.Rows[row.Index].Cells[1].Value.ToString();
                    jLink = "{ \"id\":0,\"jsonrpc\":\"2.0\",\"method\":\"Playlist.Add\",\"params\": {\"item\":{\"file\":\"" + jLink + "\"},\"playlistid\":1}}";  //OK
                    if (!await ClassKodi.Run(jLink)) continue;

                    //x = await ClassKodi.Run(jLink);
                    //if (!x) break;
                }
            }
            else
            {
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = true;
                jLink = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                jLink = "{ \"id\":0,\"jsonrpc\":\"2.0\",\"method\":\"Playlist.Add\",\"params\": {\"item\":{\"file\":\"" + jLink + "\"},\"playlistid\":1}}";  //OK

                _ = await ClassKodi.Run(jLink);

            }

            // wait for OK, delay next?  Run link array

            //  jLink = "{ \"jsonrpc\":\"2.0\",\"method\":\"Player.Open\",\"params\":{ \"item\":{ \"file\":\"" + jLink + "\"} },\"id\":0}";  //OK
            // string jLink1="{ \"jsonrpc\": \"2.0\", \"method\": \"Playlist.Clear\", \"params\": { \"playlistid\": 1 }, \"id\": 0 }";  //OK
        }

        private void downloadYTFileTSMenuItem_Click(object sender, EventArgs e)
        {

            //if (!_youtube_dl)
            //{
            //    _youtube_dl = YT_dl();
            //    if (!_youtube_dl)
            //    {
            //        PopupForm("youtube_dl not found", "red", 3000);
            //        // _youtube_dl = true;
            //    }
            //    return;
            //}

            //int counter = 0;
            //dataGridView1.FirstDisplayedScrollingRowIndex = counter; //dataGridView1.Rows(counter).Index;
            //dataGridView1.CurrentCell = dataGridView1[0, counter];

            if (dataGridView1.RowCount == 0) return;

            if (btn_refind.Visible == true) { button_search_Click(null, null); }


            UIVisible(false); //hide buttons


            if (ModifierKeys == Keys.Control && panel1.Visible == false)  //download with last options
            {
                RWSettings("read"); //gets values and write

                if (comboBox_download.SelectedIndex > 0
                    && comboBox_download.SelectedIndex < comboBox_download.Items.Count)  //to avoid no path
                    StartDownload();

                UIVisible(true);

            }
            else if (panel1.Visible == false)  // first click open panel , get saved values 
            {
                RWSettings("read");

                ShowPanel(true);

            }

        }



        private void openLinkLocationTSMenuItem_Click(object sender, EventArgs e)
        {
            //get link -> open expolorer
            Cursor.Current = Cursors.WaitCursor;

            if (dataGridView1.Rows.Count == 0) return;

            var linkcell = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();

            // string browserUrl = "";

            if (linkcell.Contains(YTPLUGIN))
            {
                string[] key = linkcell.Split('=');  //variant normal or YT playlist link
                if (key.Length > 1)
                {
                    var browserUrl = YTURL + key[1];
                    Process.Start(browserUrl);
                    return;
                }
            }

            // var folderPath = "";


            else if (linkcell.Contains("plugin")) return; //goodbye

            else if (linkcell.StartsWith("nfs:"))
            {
                linkcell = linkcell.Replace("nfs:", "").Replace("/", @"\");
                //  folderPath = Path.GetDirectoryName(linkcell);
            }
            //else if (linkcell.Contains(@":\"))
            //{
            //    folderPath = Path.GetDirectoryName(linkcell);
            //}
            // if (LaunchFolderView(linkcell)) ;
            //if (Directory.Exists(folderPath))  
            if (MyFileExists(linkcell, 5000))

                Process.Start("explorer.exe", string.Format("/select,\"{0}\"", linkcell));

            else PopupForm("File not found ", "red", 3000);

            // Process.Start("explorer.exe ", folderPath);
            Cursor.Current = Cursors.Default;

        }

        private void sendToHtttpTSMenuItem_Click(object sender, EventArgs e)
        {
            //get link col -> cut string -> make YT link -> copy to clipboard
            if (dataGridView1.RowCount == 0) return;

            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            //copy selection to whatever
            if (dataGridView1.CurrentCell.Value != null && dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                //select row
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = true;

                try
                {
                    // Add the selection to the clipboard.

                    Clipboard.SetDataObject(this.dataGridView1.GetClipboardContent());
#if DEBUG
                    Console.WriteLine(Clipboard.GetText());   //Name[tab]Link[CR][LF]
#endif
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The Clipboard could not be accessed. " + ex.Message, "Copy/Paste", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }

            DataObject ClipO = (DataObject)Clipboard.GetDataObject();

            if (Clipboard.ContainsText() && ClipO.GetData(DataFormats.Text).ToString().Contains(YTPLUGIN))
            {
                // Set cursor as hourglass
                Cursor.Current = Cursors.WaitCursor;

                try
                {
                    string[] pastedRows = Regex.Split(ClipO.GetData(DataFormats.Text).ToString().TrimEnd("\r\n".ToCharArray()), "\r\n");
                    Clipboard.Clear();

                    foreach (string pastedRow in pastedRows)
                    {
                        string[] pastedRowCells = pastedRow.Split(new char[] { '\t' });

                        for (int i = 0; i < pastedRowCells.Length; i++)
                        {
                            // cut string
                            string[] key = pastedRowCells[i + 1].Split('=');  //variant normal or YT playlist link
                            if (key.Length > 1)     //if link has no '='
                            {

                                Clipboard.SetText(YTURL + key[1]);
                                Thread.Sleep(3000); //block UI wait for JDownloader

                            }
                            i++;
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("The Clipboard could not be accessed. " + ex.Message, "Copy/Paste", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Set cursor as default arrow
                Cursor.Current = Cursors.Default;

            }
            else
            {
                //popup no YT Link
                PopupDelay("No YT Link", "red", 1500);
            }

        }

        #endregion

        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;

            //drag drop rows
            //if (dataGridView1.SelectedRows.Count > 0)
            //{
            //    e.Effect = DragDropEffects.Move;
            //}


        }

        private void button_search_Click(object sender, EventArgs e)
        {
            if (_taglocal) button_tag.PerformClick();

            if (_isIt)
            {
                _isIt = !_isIt;
                textBox_find.Visible = true; btn_clearfind.Visible = true; btn_clearfind.BringToFront(); btn_refind.Visible = true; btn_refind.BringToFront();
                this.ActiveControl = textBox_find;
            }
            else
            {
                _isIt = !_isIt;
                textBox_find.Visible = false; btn_clearfind.Visible = false; btn_refind.Visible = false;
            }
        }

        private void textBox_selectAll_Click(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.SelectAll();
        }

        private void textBox_find_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_find.Text))
            {
                dataGridView1.ClearSelection();
                dataGridView1.Refresh(); return;
            }

            var colS = Settings.Default.colSearch;  //which col to search

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.ClearSelection();
                _foundtext = false;

                string _name = "";
                List<string> _searchlist = new List<string>();

                if (textBox_find.Text.ToLower().Contains(' '))
                {
                    string[] _search = textBox_find.Text.ToLower().Split(' ');

                    for (int i = 0; i < _search.Length; i++)
                        if (!string.IsNullOrEmpty(_search[i])) _searchlist.Add(_search[i].Trim());

                }
                else
                {
                    _searchlist.Add(textBox_find.Text.ToLower().Trim());
                }


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null)
                        _name = row.Cells[colS].Value.ToString().ToLower();


                    if (!_searchlist.All(x => _name.Contains(x)))  //logical AND
                        continue;

                    dataGridView1.Rows[row.Index].Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;

                    _foundtext = true;
                    textBox_find.ForeColor = Color.Black;
                }

                if (!_foundtext)//text red 
                    textBox_find.ForeColor = Color.Red;
            }

            dataGridView1.Refresh();
        }

        private void button_dup_Click(object sender, EventArgs e)
        {
            if (_taglocal) button_tag.PerformClick();
            if (_taglink) button_check.PerformClick();

            var colD = Settings.Default.colDupli;

            dataGridView1.ClearSelection();

            if (dataGridView1.Rows.Count > 0)
            {

                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    for (int a = 1; a < dataGridView1.Rows.Count - row; a++)
                    {
                        if (dataGridView1.Rows[row].Cells[colD].Value.Equals(dataGridView1.Rows[row + a].Cells[colD].Value))
                        {

                            dataGridView1.Rows[row + a].Selected = true;

                            dataGridView1.FirstDisplayedScrollingRowIndex = row + a;

                        }
                    }
                }
            }

            if (ModifierKeys == Keys.Shift)
            {
                button_delLine.PerformClick();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //  if (e.Handled) return;

            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.F:    //find string
                        //ShowFindBox(null);
                        button_search.PerformClick();
                        break;

                    case Keys.C:    //copy row
                        copyTSMenuItem.PerformClick();
                        break;

                    case Keys.V:    //insert row
                        pasteTSMenuItem.PerformClick();
                        break;

                    case Keys.P:    //play on kodi
                        playTSMenuItem.PerformClick();
                        break;

                    case Keys.Q:    //queu on Kodi  
                        queueTSMenuItem.PerformClick();
                        break;

                    case Keys.S:
                        _savenow = true;
                        button_save.PerformClick();
                        break;

                    case Keys.N:
                        Settings.Default.nostart = true;
                        Settings.Default.Save();
                        RWSettings("write");
                        var info = new ProcessStartInfo(Application.ExecutablePath);
                        Process.Start(info);
                        // button_del_all.PerformClick();
                        break;

                    case Keys.X:    //cut row
                        cutTSMenuItem.PerformClick();
                        break;

                    case Keys.L:    //open link in explorer
                        openLinkLocationTSMenuItem_Click(sender, null);
                        //   openLinkLocationToolStripMenuItem.PerformClick();
                        break;

                    case Keys.G:    //search Name with google
                        searchGoogletoolStriptem.PerformClick();
                        break;

                    case Keys.Add:    //change font size
                        zoomf += 0.1F;
                        ZoomGrid(zoomf);
                        break;

                    case Keys.Oemplus:      //change font size
                        zoomf += 0.1F;
                        ZoomGrid(zoomf);
                        break;

                    case Keys.Subtract:    //change font size
                        zoomf -= 0.1F;
                        ZoomGrid(zoomf);
                        break;

                    case Keys.OemMinus:     //change font size
                        zoomf -= 0.1F;
                        ZoomGrid(zoomf);
                        break;

                    case Keys.O:
                        label_open_Click(sender, e);
                        break;

                    case Keys.D1:
                        MoveLine(-1);
                        break;

                    case Keys.D2:
                        MoveLine(1);
                        break;
                }
            }
            if (e.KeyCode == Keys.Delete && dataGridView1.IsCurrentCellInEditMode == false
               /* && textBox_find.Focused == false*/)
            {
                button_delLine.PerformClick();
            }
            if (e.KeyCode == Keys.F2)
            {
                dataGridView1.BeginEdit(true);
            }
            if (e.Modifiers == Keys.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                        cms1Send2Clip.PerformClick();
                        break;
                }
            }
            // e.Handled = true;
        }

        /// <summary>
        /// change font size of datagrid
        /// </summary>
        /// <param name="f">change factor float</param>
        private void ZoomGrid(float f)
        {
            // https://stackoverflow.com/questions/18385927/change-aspect-zoom

            dataGridView1.Font = new Font(dataGridView1.Font.FontFamily,
                                         FONTSIZE * f, dataGridView1.Font.Style);

            Properties.Settings.Default.ZoomFactor = f;

            //  dataGridView1.RowTemplate.Height = (int)(ROWHEIGHT * f);

        }

        /// <summary>
        /// tag links on local files with green
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_tag_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) return;

            if (!_taglocal)
            {
                _taglocal = true;
                button_tag.BackColor = Color.Green;

            }

            else if (_taglocal)
            {
                _taglocal = false;
                button_tag.BackColor = Color.MidnightBlue;
                dataGridView1.ClearSelection();
                return;
            }


            var values = new[] { "plugin", "http" };


            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                var _content = dataGridView1.Rows[item.Index].Cells[1].Value.ToString();
                //dataGridView1.Rows[item.Index].Cells[1].Value.ToString().Contains(".m")

                if (values.Any(_content.Contains) /*&& !_taglocal*/)
                {
                    // dataGridView1.Rows[item.Index].Cells[1].Style.BackColor = System.Drawing.Color.LightGreen; //item.Cells = System.Drawing.Color.Black;
                    dataGridView1.Rows[item.Index].Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = item.Index;

                }
                //else if (_taglocal)
                //{
                //    // dataGridView1.Rows[item.Index].Cells[1].Style.BackColor = System.Drawing.Color.White; //item.Cells = System.Drawing.Color.Black;
                //    dataGridView1.Rows[item.Index].Selected = false;

                //}

            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_taglocal)
            {
                button_tag.BackColor = Color.MidnightBlue;
                _taglocal = false;
            }
            if (panelMRU.Visible) panelMRU.Visible = false;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            toSave(true);
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_taglink) button_check.PerformClick();
            toSave(true);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                label_open_Click(sender, e);
                //  button_open.PerformClick();
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                //  if (ModifierKeys == Keys.Shift) queueTSMenuItem.PerformClick();
                playTSMenuItem.PerformClick();
            }
            else if (ModifierKeys == (Keys.Control | Keys.Shift))
            {
                queueTSMenuItem.PerformClick();
            }
            else
            {
                if (dataGridView1.RowCount > 0 && _vlcfound) button_vlc.PerformClick();
            }

        }


        /// <summary>
        /// move the marked line up or down
        /// </summary>
        /// <param name="direction">-1 up 1 down</param>
        public void MoveLine(int direction)
        {
            dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = true;

            if (dataGridView1.SelectedCells.Count > 0 && dataGridView1.SelectedRows.Count > 0)  //whole row must be selected
            {
                var row = dataGridView1.SelectedRows[0];
                var maxrow = dataGridView1.RowCount - 1;

                if (row != null && !((row.Index == 0 && direction == -1) || (row.Index == maxrow && direction == 1)))
                {
                    // if ((row.Index == 0 && direction == -1) || (row.Index == maxrow && direction == 1)) return;  //check end of dataGridView1

                    var swapRow = dataGridView1.Rows[row.Index + direction];

                    object[] values = new object[swapRow.Cells.Count];

                    foreach (DataGridViewCell cell in swapRow.Cells)
                    {
                        values[cell.ColumnIndex] = cell.Value;
                        cell.Value = row.Cells[cell.ColumnIndex].Value;
                    }

                    foreach (DataGridViewCell cell in row.Cells)
                        cell.Value = values[cell.ColumnIndex];

                    dataGridView1.Rows[row.Index + direction].Selected = true;
                    dataGridView1.Rows[row.Index].Selected = false;
                    dataGridView1.CurrentCell = dataGridView1.Rows[row.Index + direction].Cells[0];  //scroll automatic to cell
                }
                toSave(true);
            }




        }

        /// <summary>
        /// change icon and flag for saving file
        /// </summary>
        /// <param name="hasChanged">true if grid modified vs file</param>
        /// <param name="reset">reset undo/redo stack</param>
        public void toSave(bool hasChanged, bool reset = false)
        {

            if (reset)
            {
                undoStack.Clear(); redoStack.Clear(); ShowReUnDo(0);
            }

            isModified = hasChanged;

            if (hasChanged)
            {
                button_save.BackgroundImage = Resources.content_save_modified;
                DataGridView1_CellValidated(null, null);
            }
            if (!hasChanged)
                button_save.BackgroundImage = Resources.content_save_1_;

        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            ShowPanel(false);

            button_settings.Visible = true;
            button_vlc.Visible = true;
            button_search.Visible = true;
        }

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.maxres = comboBox1.SelectedIndex;
            Settings.Default.Save();
        }

        /// <summary>
        /// shows or hide download option panel
        /// </summary>
        /// <param name="_show">true show, false hide</param>
        private void ShowPanel(bool _show)
        {
            if (!_show)
            {
                //this.dataGridView1.Location = new Point(this.dataGridView1.Location.X, 56);
                //this.dataGridView1.Height = 310;//def 310

                button_download.Visible = true;
                button_cancel.Visible = false;
                panel1.Visible = false;
                progressBar1.Visible = false;
                label_counter.Visible = false;
            }
            else  //true
            {
                comboBox_audio.SelectedIndex = Settings.Default.comboaudio;
                comboBox_video.SelectedIndex = Settings.Default.combovideo;
                //this.dataGridView1.Location = new Point(this.dataGridView1.Location.X, 110);
                //this.dataGridView1.Height = 256;//def 310
                button_download.Visible = false;
                // button_download.BackgroundImage = Resources.download_outline_green;
                panel1.Visible = true;
                button_cancel.Visible = true;


            }
        }

        private void button_download_start_Click(object sender, EventArgs e)
        {
            RWSettings("write"); //gets values and write

            BarDefault();

            StartDownload();

            ShowPanel(false);

            UIVisible(true);

        }

        #region Download


        private void StartDownload()
        {
            if (comboBox_download.SelectedIndex <= 0 /*&& checkBox_F.Checked == false*/)  //select folder new path 0 or n select -1
            {
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {

                    output = folderBrowserDialog.SelectedPath;
                    comboBox_download.Items.Add(output);

                    comboBox_download.SelectedIndex = comboBox_download.Items.Count - 1;
                    Settings.Default.combodown = comboBox_download.SelectedIndex;
                    Settings.Default.Save();
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            else if (comboBox_download.SelectedIndex >= 0 && comboBox_download.SelectedIndex < comboBox_download.Items.Count)
            {
                output = comboBox_download.Text;
            }

            string movepath = "";

            if (!string.IsNullOrEmpty(output) && NativeMethods.UNCPath(output).StartsWith(@"\\"))  //copy to network path
            {
                movepath = output;     //store downpath in movepath
                output = Path.GetTempPath();  //temp path

            }

            if (dataGridView1.SelectedRows.Count < 1)
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = true;

            bool _rLink = checkBox_rlink.Checked;  //flag to replace link

            DownloadYTFile(output, movepath, _rLink);  //Download yt file
        }


        private void DownloadYTFile(string downpath, string movepath, bool _rLink)
        {
            string playcell = "";
            bool _done = false;
            // string videofilename = "";

            WaitWindow waitmove = new WaitWindow();
            waitmove.Owner = this;
            var x = Location.X - 40 + (Width - waitmove.Width) / 2;
            var y = Location.Y + (Height - waitmove.Height) / 2;
            waitmove.Location = new Point(Math.Max(x, 0), Math.Max(y, 0));
            waitmove.StartPosition = FormStartPosition.Manual;

            int i = 1;
            //string fpsValue = textBox1.Text.Trim();
            //if (!fpsValue.StartsWith("<") && !fpsValue.StartsWith(">")) fpsValue = "";

            //ClassYTExplode yte = new ClassYTExplode();
            //yte.ValueChanged += ProgressEventHandler; 

            foreach (DataGridViewRow row in dataGridView1.GetSelectedRows())
            {

                label_counter.Text = i + " / " + dataGridView1.SelectedRows.Count; i++;

                playcell = dataGridView1.Rows[row.Index].Cells[1].Value.ToString();
                //  namecell = dataGridView1.Rows[row.Index].Cells[0].Value.ToString();

                if (playcell.Contains("plugin") && playcell.Contains("youtube"))
                {
                    string[] key = playcell.Split('=');  //variant normal or YT playlist link
                    if (key.Length > 1)
                    {
                        //if (!string.IsNullOrEmpty(ClassDownload.DownloadYTLink
                        //                (YTURL + key[1], downpath, fpsValue, out string videofilename)))
                        if (!string.IsNullOrEmpty(DownloadYTLinkEx
                                        (YTURL + key[1], downpath, out string videofilename)))
                        {
                            if (videofilename == "error") continue;  //for download error -> next foreach

                            _done = true;

#if DEBUG
                            Console.WriteLine(videofilename);   //
#endif
                            if (!string.IsNullOrEmpty(movepath) && Path.GetExtension(videofilename) != ".part")
                            {
                                var errorfilename = videofilename;
                                //don't move part files

                                if (MyDirectoryExists(movepath, 4000))
                                {
                                    waitmove.Show();
                                    waitmove.Refresh();
                                    videofilename = FileMove(videofilename, movepath);
                                    if (videofilename == "error")
                                    {
                                        waitmove.Close();
                                        continue;
                                    }
                                    waitmove.Close();
                                }
                                else  //path not avaliable
                                {
                                    DialogResult dialogSave = MessageBox.Show("Target path not avaliable Do you want to save to different path?",
                                    "Save Playlist", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (dialogSave == DialogResult.Yes)
                                    {
                                        saveFileDialog1.FileName = errorfilename;
                                        saveFileDialog1.ShowDialog();
                                    }
                                    else
                                    {
                                        return;
                                    }
                                    errorfilename = "";
                                }
                            }

                            string UNCfileName = NativeMethods.UNCPath(videofilename);

                            //with replace Link
                            if (checkBox_unix.Checked && rDrive && _rLink
                                && !UNCfileName.EndsWith(".part") /* && comboBox_video.SelectedIndex !=4*/)  //unix and replace drive true, no .part, no audio only
                            {

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

                                            playcell = "nfs://" + nfs_server + rest;
                                            dataGridView1.Rows[row.Index].Cells[1].Value = playcell.Replace("\\", "/");
                                        }
                                    }

                                    if (!string.IsNullOrEmpty(rpi_ip))
                                    {
                                        rpi_ip = rpi_ip.Replace("/", "\\");

                                        if (UNCfileName.Contains(rpi_ip))
                                        {
                                            string rest = UNCfileName.Replace(rpi_ip, "");
                                            rest = rest.Replace("\\\\\\", "\\");

                                            playcell = "/storage/" + rest;
                                            dataGridView1.Rows[row.Index].Cells[1].Value = playcell.Replace("\\Videos", "videos").Replace("\\", "/");  //Bug 1.9.3
                                        }
                                    }
                                }
                                else
                                {
                                    dataGridView1.Rows[row.Index].Cells[1].Value = UNCfileName;
                                }
                            }
                            else if (!checkBox_unix.Checked && rDrive && _rLink && !UNCfileName.Contains("part"))
                            {
                                //  dataGridView1.Rows[row.Index].Cells[1].Value = videofilename;
                            }

                            // if (_rLink) popupForm("Link replaced", "blue", 1500);
                            // no replace link
                        }
                        else
                        {
                            PopupForm("Error " + videofilename, "red", 3000);
                        }
                    }
                }
                else if (playcell.StartsWith("html"))
                {

                    if (!string.IsNullOrEmpty(ClassDownload.DownloadLink
                                       (playcell, downpath, out string videofilename)))
                    {

                    }
                }

            }

            if (_done) PopupForm("Download finished", "green", 3000);

        }


        public double Progress2
        {
            get { return _progress; }
            set
            {
                if (value != _progress)
                {
                    _progress = value;
                    ///    Console.WriteLine(_progress.ToString());
                    progressBar1.Value = (int)_progress;
                }
            }
        }


        private string DownloadYTLinkEx(string videolink, string NewPath, out string videofilename)
        {
            Cursor.Current = Cursors.WaitCursor;
            // ClassYTExplode tt = new ClassYTExplode();
            Form1 frm1 = new Form1();

            int maxres = Settings.Default.maxres;  //-> SetVideoQuality
            int cvideo = Settings.Default.combovideo; //-> SetFileContainer .mp4 | .webm

            if (!ClassDownload.CheckForFfmpeg() && maxres >= 720) return videofilename = "error";


            Task.Run(async () => { await DownloadStream(videolink, NewPath, maxres, cvideo); }).Wait();  //-> videoUrlnew   audioUrl 

            Cursor.Current = Cursors.Default;

            var _videoname = videoTitle;

            if (string.IsNullOrEmpty(_videoname))
                return videofilename = "error";
            else return videofilename = _videoname;

        }

        private async Task DownloadStream(string videoId, string NewPath, int height = 2, int fileext = 0)
        {
            _youtube = new YoutubeClient();
          //  var converter = new YoutubeConverter(_youtube); // re-using the same client instance for efficiency, not required

            if (fileext < 2)
            {
                try
                {
                    string[] filetype = { "mp4", "webm" };
                    //   Progress = 0;


                    // Get stream manifest
                    var streamManifest = await _youtube.Videos.Streams.GetManifestAsync(videoId);

                    // Select audio stream
                    //  var audioStreamInfo2 = streamManifest.GetAudio().WithHighestBitrate();
                    var audioStreamInfo = streamManifest.GetAudio()
                                           .Where(s => s.Container == ClassYTExplode.SetFileContainer(fileext))
                                           .WithHighestBitrate();

                    var videoStreamInfo2 = streamManifest.GetVideoOnly()
                                          .Where(s => s.VideoQuality <= ClassYTExplode.SetVideoQuality(height))
                                          .Where(s => s.Container == ClassYTExplode.SetFileContainer(fileext))
                                         // .Select(h => h.Url).ToList();
                                         .First()
                                          ;

                    // Combine them into a collectionb
                    var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo2 };

                    VideoInfo = await _youtube.Videos.GetAsync(videoId);  //video info
                    videoTitle = NewPath + "\\" + RemoveSpecialCharacters(VideoInfo.Title) + "." + filetype[fileext];

                    var progHandler = new Progress<double>(p => Progress2 = p * 100);


                    if (MyFileExists(videoTitle, 3000))
                    {
                        switch (MessageBox.Show("File exists, overwrite?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                        {
                            case DialogResult.Yes:
                                // Download and process them into one file
                               // await converter.DownloadAndProcessMediaStreamsAsync(streamInfos, videoTitle, filetype[fileext], progHandler);
                                await _youtube.Videos.DownloadAsync(streamInfos, new ConversionRequestBuilder(videoTitle).Build(), progHandler);
                                break;

                            case DialogResult.No:
                                //  return "error";
                                break;
                        }
                    }
                    else
                    {
                        // Download and process them into one file

                       // await converter.DownloadAndProcessMediaStreamsAsync(streamInfos, videoTitle, filetype[fileext], progHandler);
                        await _youtube.Videos.DownloadAsync(streamInfos, new ConversionRequestBuilder(videoTitle).Build(), progHandler);

                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(videoTitle + Environment.NewLine + e.Message, "Video Download", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    videoTitle = "error";
                }
                finally
                {
                    //  Progress = 0;

                }

            }

            else  //audio only
            {
                try
                {

                    VideoInfo = await _youtube.Videos.GetAsync(videoId);  //video info
                    videoTitle = NewPath + "\\" + RemoveSpecialCharacters(VideoInfo.Title) + ".mp3";
                    var progHandler = new Progress<double>(p => Progress2 = p * 100);

                    if (MyFileExists(videoTitle, 3000))
                    {
                        switch (MessageBox.Show("File exists, overwrite?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                        {
                            case DialogResult.Yes:
                                // Download and process them into one file
                              //  await converter.DownloadVideoAsync(videoId, videoTitle, progHandler);
                                await _youtube.Videos.DownloadAsync(videoId, videoTitle, progHandler);
                                break;

                            case DialogResult.No:
                                //  return "error";
                                break;
                        }
                    }
                    else
                    {
                        // Download and process them into one file
                       // await converter.DownloadVideoAsync(videoId, videoTitle, progHandler);
                        await _youtube.Videos.DownloadAsync(videoId, videoTitle, progHandler);

                    }


                }
                catch (Exception e)
                {
                    MessageBox.Show(videoTitle + Environment.NewLine + e.Message, "Audio Download", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    videoTitle = "error";
                }
            }
        }

        private static string RemoveSpecialCharacters(string path)
        {
            return Path.GetInvalidFileNameChars().Aggregate(path, (current, c) => current.Replace(c.ToString(), string.Empty));
        }

        #endregion

        private void UIVisible(bool show)
        {
            button_settings.Visible = show;
            button_vlc.Visible = show;
            button_search.Visible = show;
            button_download.Visible = show;
        }


        /// <summary>
        /// reads or writes the youtube-dl UI
        /// </summary>
        /// <param name="_rw">true read, false write</param>
        private void RWSettings(string _rw)
        {
            if (_rw == "read")
            {
                //sets UI  //bug what if index not avaliable
                comboBox_download.SelectedIndex = Settings.Default.combodown;
                comboBox1.SelectedIndex = Settings.Default.maxres;
                comboBox_audio.SelectedIndex = Settings.Default.comboaudio;
                comboBox_video.SelectedIndex = Settings.Default.combovideo;
                checkBox_rlink.Checked = Settings.Default.replaceDrive;
                //checkBox_verb.Checked = Settings.Default.verbose;
                //checkBox_F.Checked = Settings.Default.showFormats;
                //checkBox_subs.Checked = Settings.Default.allsubs;

            }
            else if (_rw == "read1st")
            {
                comboBox1.SelectedIndex = Settings.Default.maxres;
                comboBox_audio.SelectedIndex = Settings.Default.comboaudio;
                comboBox_video.SelectedIndex = Settings.Default.combovideo;
                checkBox_rlink.Checked = Settings.Default.replaceDrive;
                //checkBox_verb.Checked = Settings.Default.verbose;
                //checkBox_F.Checked = Settings.Default.showFormats;
                //checkBox_subs.Checked = Settings.Default.allsubs;
            }
            else if (_rw == "write")
            {
                //read out UI and write
                Settings.Default.combodown = comboBox_download.SelectedIndex;
                Settings.Default.comboaudio = comboBox_audio.SelectedIndex;
                Settings.Default.combovideo = comboBox_video.SelectedIndex;
                Settings.Default.maxres = comboBox1.SelectedIndex;
                Settings.Default.replaceDrive = checkBox_rlink.Checked;
                //Settings.Default.fps = checkBox_fps.Checked;
                //Settings.Default.verbose = checkBox_verb.Checked;
                //Settings.Default.allsubs = checkBox_subs.Checked;
                //Settings.Default.showFormats = checkBox_F.Checked;

                Settings.Default.combopathlist.Clear();

                foreach (object item in comboBox_download.Items)
                {
                    Settings.Default.combopathlist.Add(item.ToString());
                }


                Settings.Default.Save();
            }
        }

        /// <summary>
        /// start download YT file
        /// </summary>
        //private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    Process.Start("https://github.com/ytdl-org/youtube-dl/blob/master/README.md#format-selection");
        //}




        private void ComboBox_Click(object sender, EventArgs e)
        {
            ComboBox obj = sender as ComboBox;
            obj.DroppedDown = true;
        }

        private async void button_check_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) return;

            if (!_taglink)
            {
                _taglink = true;
                button_check.BackColor = Color.LightSalmon;
            }
            else if (_taglink)
            {
                if (ModifierKeys == Keys.Control)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (dataGridView1.Rows[row.Index].Cells[0].Style.BackColor == Color.LightSalmon)
                        {
                            dataGridView1.Rows[row.Index].Selected = true;  //                   Cells[0].Value.ToString();
                        }
                    }
                    return;
                }

                _taglink = false;
                button_check.BackColor = Color.MidnightBlue;
                colorclear();
                return;
            }

            if (_taglocal) button_tag.PerformClick();


            if (ModifierKeys == Keys.Control) _mark = true;
            else _mark = false; //select links


            Cursor.Current = Cursors.WaitCursor;

            dataGridView1.ClearSelection();

            // if (!CheckStream("http://www.google.com"))
            if (!IsDriveReady("8.8.8.8"))
            {
                if (!IsDriveReady("8.8.4.4"))
                {
                    MessageBox.Show("No internet connection found!");
                    return;
                }
            }

            button_check.Enabled = false;
            //get youtube title
            //check file exist
            if (dataGridView1.Rows.Count > 0)
            {
                colorclear();

                popup popup = new popup();

                popup.FormClosed += new FormClosedEventHandler(FormP_Closed);

                var x = Location.X + (Width - popup.Width) / 2;
                var y = Location.Y + (Height - popup.Height) / 2;
                popup.Location = new Point(Math.Max(x, 0), Math.Max(y, 0));
                popup.StartPosition = FormStartPosition.Manual;
                popup.Owner = this;  //child over parent

                popup.Show();

                Progress<string> progress = new Progress<string>();
                progress.ProgressChanged += (_, text) =>
                    popup.updateProgressBar(text);

                tokenSource = new CancellationTokenSource();
                var token = tokenSource.Token;

                await Task.Run(() => RunStreamCheck(token, progress));

                popup.Close();


            }

            button_check.Enabled = true;

            Cursor.Current = Cursors.Default;

            void colorclear()
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        dataGridView1.Rows[item.Index].Cells[j].Style.BackColor = Color.White;
                    }
                }
            }


        }

        void FormP_Closed(object sender, FormClosedEventArgs e)
        {
            popup popup = (popup)sender;

            tokenSource.Cancel();

        }


        private void RunStreamCheck(CancellationToken token, IProgress<string> progress)
        {
            string playcell = "";
            string[] knownip = Array.Empty<string>();

            string maxrows = dataGridView1.Rows.Count.ToString();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (token.IsCancellationRequested)
                {
                    // MessageBox.Show("test");
                    break;
                }

                playcell = dataGridView1.Rows[row.Index].Cells[1].Value.ToString();

                progress.Report(row.Index.ToString() + " / " + maxrows);

                if (playcell.Contains("plugin") && playcell.Contains("youtube"))
                {
                    string[] key = playcell.Split('=');  //variant normal or YT playlist link
                    if (key.Length > 1)
                    {
                        if (GetTitle_html(YTURL + key[1]) == "YouTube")
                        //  if (GetTitle_client(YTURL + key[1]) == "N/A")
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                if (_mark) dataGridView1.Rows[row.Index].Selected = true;
                                dataGridView1.Rows[row.Index].Cells[i].Style.BackColor = Color.LightSalmon;
                            }
                            dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                        }
                    }
                }
                else if (playcell.Contains("nfs") || playcell.Contains("\\\\"))
                {
                    // playcell = playcell.Replace("/", "\\").Replace("nfs:", "file:///");
                    playcell = playcell.Replace("/", "\\").Replace("nfs:", "");

                    string[] serverip = playcell.Split('\\');
                    //var nfs_ip = Regex.Match(playcell, @"\b(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\b");
                    //if (IsDriveReady(nfs_ip.Captures[0].ToString()))

                    if (knownip.Contains(serverip[2]))
                    {
                        dataGridView1.Rows[row.Index].Cells[1].Style.BackColor = Color.LightGray;

                    }
                    else if (IsDriveReady(serverip[2]))
                    {
                        FileInfo fi = new FileInfo(playcell);
                        bool exists = fi.Exists;
                        if (!exists) colorset();
                        // if (!fi.Exists) colorset();
                        //if (!File.Exists(playcell)) colorset();  //less reliable?
                    }
                    else
                    {
#if DEBUG
                        MessageBox.Show("IP not responding " + serverip[2]);
#endif
                        //FileInfo fi = new FileInfo(playcell);
                        //bool exists = fi.Exists;
                        //if (!exists)
                        if (!IsDriveReady(serverip[2]))
                        {
                            dataGridView1.Rows[row.Index].Cells[1].Style.BackColor = Color.LightGray;
                            Array.Resize(ref knownip, knownip.Length + 1);
                            knownip[knownip.Length - 1] = serverip[2];
                        }


                    }

                }
                else if (playcell.Contains(":\\"))  //C:\    http://
                {
                    if (!File.Exists(playcell)) colorset();
                }
                else if (playcell.Contains("http"))
                {
                    if (!CheckStream(playcell)) colorset();
                }

                else if (playcell.Contains("ERROR"))
                {
                    colorset();
                }

                void colorset()
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (_mark) dataGridView1.Rows[row.Index].Selected = true;
                        dataGridView1.Rows[row.Index].Cells[i].Style.BackColor = Color.LightSalmon;
                    }
                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                }

            }

        }

        private void SearchGoogletoolStriptem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) return;

            String searchRequest = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            if (searchRequest[searchRequest.Length - 4] == '.')
                searchRequest = searchRequest.Substring(0, searchRequest.Length - 4);

            //  searchRequest = searchRequest.Replace(videotypes, " ");
            // searchRequest = new System.Text.RegularExpressions.Regex("(?<=for ?).+$").Match(searchRequest).Value;

            // Process.Start("https://www.google.com/search?q=" + Uri.EscapeDataString(searchRequest));
            Process.Start(Settings.Default.SearchQuery + Uri.EscapeDataString(searchRequest));
        }

        private void DataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (ignore) { return; }
            if (undoStack.LoadItem(dataGridView1))
            {
                undoStack.Push(dataGridView1.Rows.Cast<DataGridViewRow>()
                    .Where(r => !r.IsNewRow)
                    .Select(r => r.Cells.Cast<DataGridViewCell>()
                    .Select(c => c.Value).ToArray()).ToArray());

                //  if (undoStack.Count > 1) toSave(true);
                // if (undoStack.Count > 1) ShowReUnDo(1); 

            }
            //UndoButton.Visible = undoStack.Count > 1;
            //RedoButton.Enabled = redoStack.Count > 1;
            ShowReUnDo(1);

        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 1) return;
            if (redoStack.Count == 0 || redoStack.LoadItem(dataGridView1))
            {
                redoStack.Push(dataGridView1.Rows.Cast<DataGridViewRow>()
                    .Where(r => !r.IsNewRow)
                    .Select(r => r.Cells.Cast<DataGridViewCell>()
                    .Select(c => c.Value).ToArray()).ToArray());
            }

            if (undoStack.Count > 0)
            {
                object[][] gridrows = undoStack.Pop();
                while (gridrows.ItemEquals(dataGridView1.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow).ToArray()))
                {
                    // if (undoStack.Count > 0)
                    {
                        try
                        {
                            gridrows = undoStack.Pop();
                        }
                        catch (Exception) { }
                        //TODO eception stack empty?
                    }
                }
                ignore = true;
                dataGridView1.Rows.Clear();
                for (int x = 0; x <= gridrows.GetUpperBound(0); x++)
                {
                    string[] stringArray = gridrows[x].Select(o => o?.ToString()).ToArray();   //?? syntax?

                    entries.Add(new PlayEntry(Name: stringArray[0], Link: stringArray[1]));

                }

                ignore = false;

                //UndoButton.Enabled = undoStack.Count > 0;
                //RedoButton.Enabled = redoStack.Count > 0;
                ShowReUnDo(0);
                //if (!isModified) toSave(true);

            }

        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) return;
            if (undoStack.Count == 0 || undoStack.LoadItem(dataGridView1))
            {
                undoStack.Push(dataGridView1.Rows.Cast<DataGridViewRow>()
                    .Where(r => !r.IsNewRow)
                    .Select(r => r.Cells.Cast<DataGridViewCell>()
                    .Select(c => c.Value).ToArray()).ToArray());
            }
            if (redoStack.Count > 0)
            {
                object[][] gridrows = redoStack.Pop();  // exception!!


                while (gridrows.ItemEquals(dataGridView1.Rows.Cast<DataGridViewRow>().Where(r => !r.IsNewRow).ToArray()))
                {
                    gridrows = redoStack.Pop();
                }
                ignore = true;
                dataGridView1.Rows.Clear();
                for (int x = 0; x <= gridrows.GetUpperBound(0); x++)
                {
                    string[] stringArray = gridrows[x].Select(o => o?.ToString()).ToArray();   //? nullable

                    entries.Add(new PlayEntry(Name: stringArray[0], Link: stringArray[1]));
                }

                ignore = false;

                //RedoButton.Enabled = redoStack.Count > 0;
                //UndoButton.Enabled = undoStack.Count > 0;

                ShowReUnDo(0);

            }

        }

        private void ShowReUnDo(int x)
        {
            if (undoStack.Count > x)
            {
                UndoButton.Enabled = true;
                UndoButton.BackgroundImage = Resources.undo;
                //    button_save.BackgroundImage = Resources.content_save_modified;
                //    isModified = true;

            }
            else
            {
                UndoButton.Enabled = false;
                UndoButton.BackgroundImage = Resources.undo_fade;
                //button_save.BackgroundImage = Resources.content_save_1_;
                //isModified = false;

            }
            if (redoStack.Count > x)
            {
                RedoButton.Enabled = true;
                RedoButton.BackgroundImage = Resources.redo;
            }
            else
            {
                RedoButton.Enabled = false;
                RedoButton.BackgroundImage = Resources.redo_fade;
            }

            //if (!isModified)
            //{
            //    button_save.BackgroundImage = Resources.content_save_modified;
            //    isModified = true;
            //}

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

            //check if the clipboard is filled with a row, enable insert
            if (dataGridView1.Rows.Count == 0)
            {
                for (int i = 0; i < contextMenuStrip1.Items.Count; i++)
                {
                    contextMenuStrip1.Items[i].Enabled = false;
                }

                contextMenuStrip1.Items["cms1NewWindow"].Enabled = true;

            }
            else
            {
                for (int i = 0; i < contextMenuStrip1.Items.Count; i++)
                {
                    contextMenuStrip1.Items[i].Enabled = true;
                }
            }
        }


        /// <summary>
        /// download YT file
        /// </summary>
        /// <param name="downpath">download path</param>
        /// <param name="movepath">path to move if download to network path</param>
        /// <param name="_rLink">overwrite link</param>



        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // e.Control.ContextMenu = new ContextMenu();
            e.Control.ContextMenuStrip = contextMenuStrip2;
        }

        private void BarDefault()
        {
            progressBar1.Visible = true;
            label_counter.Visible = true;
            progressBar1.Maximum = 100;
            progressBar1.Minimum = 0;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
        }


        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //if (e.RowIndex >= 0 & e.ColumnIndex >= 0 /*& IsSelected*/)
            //{
            //    e.Handled = true;
            //    e.PaintBackground(e.CellBounds, true);

            //    string sw = textBox_find.Text;

            //    if (!string.IsNullOrEmpty(sw))
            //    {
            //        string val = (string)e.FormattedValue;
            //        int sindx = val.ToLower().IndexOf(sw.ToLower());
            //        if (sindx >= 0)
            //        {
            //            Rectangle hl_rect = new Rectangle();
            //            hl_rect.Y = e.CellBounds.Y + 2;
            //            hl_rect.Height = e.CellBounds.Height - 5;

            //            string sBefore = val.Substring(0, sindx);
            //            string sWord = val.Substring(sindx, sw.Length);
            //            Size s1 = TextRenderer.MeasureText(e.Graphics, sBefore, e.CellStyle.Font, e.CellBounds.Size);
            //            Size s2 = TextRenderer.MeasureText(e.Graphics, sWord, e.CellStyle.Font, e.CellBounds.Size);

            //            if (s1.Width > 5)
            //            {
            //                hl_rect.X = e.CellBounds.X + s1.Width - 5;
            //                hl_rect.Width = s2.Width - 6;
            //            }
            //            else
            //            {
            //                hl_rect.X = e.CellBounds.X + 2;
            //                hl_rect.Width = s2.Width - 6;
            //            }

            //            SolidBrush hl_brush;
            //            if (((e.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.None))
            //            {
            //                hl_brush = new SolidBrush(Color.DarkGoldenrod);
            //            }
            //            else
            //            {
            //                hl_brush = new SolidBrush(Color.Yellow);
            //            }

            //            e.Graphics.FillRectangle(hl_brush, hl_rect);

            //            hl_brush.Dispose();
            //        }
            //    }
            //    e.PaintContent(e.CellBounds);
            //}

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 /*& IsSelected*/)
            {
                e.Handled = true;
                e.PaintBackground(e.CellBounds, true);

                string[] _search = textBox_find.Text.ToLower().Split(' ');
                string sw = _search[0].Trim();

                if (!string.IsNullOrEmpty(sw))
                {
                    for (int i = 0; i < _search.Length; i++)
                    {
                        sw = _search[i].Trim();
                        PaintCells(sw, i);
                    }
                }
                e.PaintContent(e.CellBounds);
            }

            void PaintCells(string sw, int s_length)
            {
                Color[] colors = new Color[] { Color.Orange, Color.Yellow, Color.GreenYellow };

                string val = (string)e.FormattedValue;
                int sindx = val.ToLower().IndexOf(sw.ToLower());

                if (sindx >= 0)
                {
                    Rectangle hl_rect = new Rectangle();
                    hl_rect.Y = e.CellBounds.Y + 2;
                    hl_rect.Height = e.CellBounds.Height - 5;

                    string sBefore = val.Substring(0, sindx);
                    string sWord = val.Substring(sindx, sw.Length);
                    Size s1 = TextRenderer.MeasureText(e.Graphics, sBefore, e.CellStyle.Font, e.CellBounds.Size);
                    Size s2 = TextRenderer.MeasureText(e.Graphics, sWord, e.CellStyle.Font, e.CellBounds.Size);

                    if (s1.Width > 5)
                    {
                        hl_rect.X = e.CellBounds.X + s1.Width - 5;
                        hl_rect.Width = s2.Width - 6;
                    }
                    else
                    {
                        hl_rect.X = e.CellBounds.X + 2;
                        hl_rect.Width = s2.Width - 6;
                    }

                    SolidBrush hl_brush = default(SolidBrush);
                    if ((e.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.None)
                    {
                        hl_brush = new SolidBrush(Color.DarkGoldenrod);
                    }
                    else if (s_length < 3)
                    {
                        hl_brush = new SolidBrush(colors[s_length]);
                    }
                    else
                    {
                        hl_brush = new SolidBrush(Color.Yellow);
                    }

                    e.Graphics.FillRectangle(hl_brush, hl_rect);

                    hl_brush.Dispose();
                }
            }


        }


        private void editCellCopy_Click(object sender, EventArgs e)
        {

            if (dataGridView1.EditingControl is TextBox textBox)
            {
                if (!string.IsNullOrEmpty(textBox.SelectedText) /* != ""*/) Clipboard.SetText(textBox.SelectedText);
            }
        }

        private void editCellPaste_Click(object sender, EventArgs e)
        {
            string s = Clipboard.GetText();
            if (dataGridView1.EditingControl is TextBox)
            {
                var textBox = (TextBox)dataGridView1.EditingControl;
                textBox.SelectedText = s;
            }
        }


        private void panel2_VisibleChanged(object sender, EventArgs e)
        {
            //read mru
            mruItems.Clear();

            mruItems = File.ReadAllLines(mruFile).ToList();

            labels = new List<Label> { label1, label2, label3, label4, label5 };
            int i = 0;

            foreach (var label in labels)
            {
                if (i < mruItems.Count)
                {
                    try
                    {
                        FileInfo fi = new FileInfo(mruItems[i]);
                        label.Text = fi.Name.Replace(".m3u", "");
                    }
                    catch
                    {
                        continue;
                    }
                }
                else label.Text = "";
                i++;
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (panelMRU.Visible) panelMRU.Visible = false;
        }

        private void editF2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.BeginEdit(true);
        }

        private void deleteEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;

                    if (sourceControl.Name == "label1") { mruItems[0] = "file1"; sourceControl.Text = "file"; }
                    if (sourceControl.Name == "label2") { mruItems[1] = "file2"; sourceControl.Text = "file"; }
                    if (sourceControl.Name == "label3") { mruItems[2] = "file3"; sourceControl.Text = "file"; }
                    if (sourceControl.Name == "label4") { mruItems[3] = "file4"; sourceControl.Text = "file"; }
                    if (sourceControl.Name == "label5") { mruItems[4] = "file5"; sourceControl.Text = "file"; }
                }
            }

            File.WriteAllLines(mruFile, mruItems);  //overwrite
            button_revert.Visible = true;
            panelMRU.Visible = false;
        }

        private void cmsDeletePathItem_Click(object sender, EventArgs e)
        {
            if (comboBox_download.SelectedIndex != 0)
                comboBox_download.Items.Remove(comboBox_download.SelectedItem);

            comboBox_download.SelectedIndex = 0;
            Settings.Default.combodown = 0;

        }


        private void button_path_Click_old(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {

                output = folderBrowserDialog.SelectedPath;
                comboBox_download.Items.Add(output);

                comboBox_download.SelectedIndex = comboBox_download.Items.Count - 1;
                Settings.Default.combodown = comboBox_download.SelectedIndex;
                Settings.Default.Save();

                //downPath = lastPath;  //NewPath to store the path
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }

        }

        private void button_path_Click(object sender, EventArgs e)
        {
            // string newFolder;

            //using (FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
            //{
            //    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            //    {
            //        output = betterFolderBrowser1.SelectedPath;
            //        comboBox_download.Items.Add(output);

            //        comboBox_download.SelectedIndex = comboBox_download.Items.Count - 1;
            //        Settings.Default.combodown = comboBox_download.SelectedIndex;
            //        Settings.Default.Save();

            //    }
            //    else
            //    {
            //        return;
            //    }


            //}

            //https://stackoverflow.com/questions/705409/how-do-i-open-a-folderbrowserdialog-at-the-selected-folder

            // FolderBrowserDialog diag = new FolderBrowserDialog();
            //// diag.Description = "Select New ";
            // diag.SelectedPath = Application.StartupPath;

            // if (diag.ShowDialog() == DialogResult.OK)
            // {
            //     output = folderBrowserDialog.SelectedPath;
            //     comboBox_download.Items.Add(output);

            //     comboBox_download.SelectedIndex = comboBox_download.Items.Count - 1;
            //     Settings.Default.combodown = comboBox_download.SelectedIndex;
            //     Settings.Default.Save();

            // }

            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {

                output = folderBrowserDialog.SelectedPath;
                comboBox_download.Items.Add(output);

                comboBox_download.SelectedIndex = comboBox_download.Items.Count - 1;
                Settings.Default.combodown = comboBox_download.SelectedIndex;
                Settings.Default.Save();

            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Settings.Default.F2Size.Width == 0 || Settings.Default.F2Size.Height == 0
                 || Settings.Default.nostart)
            {
                // first start
                this.Size = new Size(1140, 422);
            }
            else
            {
                if (Settings.Default.ZoomFactor != 0) ZoomGrid(Settings.Default.ZoomFactor);
                this.Location = Settings.Default.F2Location;
                this.Size = Settings.Default.F2Size;
            }
            Settings.Default.nostart = false;
            Settings.Default.Save();

        }

        private void cms1NewWIndow_Click(object sender, EventArgs e)
        {
            Settings.Default.nostart = true;
            Settings.Default.Save();
            RWSettings("write");
            var info = new ProcessStartInfo(Application.ExecutablePath);
            Process.Start(info);

        }

        private void ShowFindBox(char? letter)   //?: nullable
        {
            if (_isIt || letter.HasValue)
            {
                _isIt = !_isIt;
                textBox_find.Visible = true;
                // textBox_find.Text = "";
                textBox_find.Text += letter;
                textBox_find.Focus();
                textBox_find.SelectionStart = textBox_find.Text.Length;
                textBox_find.SelectionLength = 0;

                this.ActiveControl = textBox_find;
            }
            else if (!_isIt && !letter.HasValue)
            {
                _isIt = !_isIt;
                textBox_find.Clear();
                textBox_find.Visible = false;
            }
            //  open box, when not empty fire search
            if (textBox_find.Text != "")
            {
                textBox_find_TextChanged(null, EventArgs.Empty);
            }




        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox_find_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            // if (e.Handled) return;

        }

        private void btn_clearfind_Click(object sender, EventArgs e)
        {
            textBox_find.Clear();
            textBox_find.Focus();
        }

        private void btn_refind_Click(object sender, EventArgs e)
        {
            textBox_find_TextChanged(sender, e);

        }

        private void editCellCut_Click(object sender, EventArgs e)
        {
            if (dataGridView1.EditingControl is TextBox)
            {
                var textBox = (TextBox)dataGridView1.EditingControl;
                if (textBox.SelectedText != "") Clipboard.SetText(textBox.SelectedText);
                textBox.SelectedText = "";
            }
        }


    }

    /// <summary>
    /// DataGridView Method extensions
    /// </summary>
    public static class DataGridViewExtensions
    {
        /// <summary>
        /// reverse order of selected rows for foreach
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<DataGridViewRow> GetSelectedRows(this DataGridView source)
        {
            for (int i = source.SelectedRows.Count - 1; i >= 0; i--)
                yield return source.SelectedRows[i];
        }

        /// <summary>
        /// double buffer on for large files speed up
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="setting"></param>
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            //http://bitmatic.com/c/fixing-a-slow-scrolling-datagridview

            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }



}

