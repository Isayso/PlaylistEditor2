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
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using PlaylistEditor.Properties;

namespace PlaylistEditor
{

    public partial class settings : Form
    {
        public string serverName;
        public bool isLinux ;
        public bool replaceDrive ;
        static int unicode = Settings.Default.hotkey;
        static int unicode2 = Settings.Default.hotkey2;
        static char character = (char)unicode;
        static char character2 = (char)unicode2;
        string hotText = character.ToString();
        string hotText2 = character2.ToString();
        bool _youtube_dl = false; // File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe");




        public settings()
        {
            InitializeComponent();
          
            textBox1.Text = Settings.Default.server;
            textBox2.Text = Settings.Default.rpi;
            textBox_Port.Text = Settings.Default.port;
            textBox_Username.Text = Settings.Default.username;
            //textBox_output.Text = Properties.Settings.Default.output;

            //if (!string.IsNullOrEmpty(NativeMethods.GetFullPathFromWindows("youtube-dl.exe")) ||
            //  File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe"))
            //{
            //    _youtube_dl = true; // File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe");

            //}

            //password
            if (Settings.Default.cipher != null && Settings.Default.entropy != null)
            {
                byte[] plaintext = ProtectedData.Unprotect(Settings.Default.cipher, Settings.Default.entropy,
                  DataProtectionScope.CurrentUser);
                textBox_Password.Text = ClassHelp.ByteArrayToString(plaintext);
            }
            else
            {
                textBox_Password.Text = "";
            }


            // textBox_Password.Text = Properties.Settings.Default.password;
           // string vlcpath = Properties.Settings.Default.vlcpath;

            //if (!string.IsNullOrEmpty(vlcpath))
            //{
            //    tabPage4.Visible = true;  //vlc page
            //}
            //else if (string.IsNullOrEmpty(vlcpath))  //first run
            //{
            //    vlcpath = ClassHelp.GetVlcPath();
            //    if (string.IsNullOrEmpty(vlcpath)) tabPage4.Visible = false; //no vlc installed
            //}

           // _youtube_dl = ClassHelp.YT_dl();
            button_update.Visible = false;

            //if (_youtube_dl)
            //{
            //    button_update.Visible = true;
            //}
            //else
            //{
            //    button_update.Visible = false;
            //}



            comboBox1.SelectedIndex = Settings.Default.colSearch;
            comboBox2.SelectedIndex = Settings.Default.colDupli;
            comboBox_res.SelectedIndex = Settings.Default.maxres;
            //comboBox_audio.SelectedIndex = Properties.Settings.Default.comboaudio;
            //comboBox_video.SelectedIndex = Properties.Settings.Default.combovideo;
            checkBox_kodi.Checked = Settings.Default.kodi_hotkey;
           

            checkBox3.Checked = Settings.Default.useDash;
         //   if (!_youtube_dl) checkBox3.Checked = false;
            checkBox2.Checked = Settings.Default.replaceDrive;
          //  checkBox_verb.Checked = Properties.Settings.Default.verbose;

            comboBox_download.Items.Clear();

            foreach (object item in Settings.Default.combopathlist)
            {
                comboBox_download.Items.Add(item);
            }

            comboBox_download.SelectedIndex = 0;


            textBox_hot.Text = hotText;
            textBox_hot2.Text = hotText2;
            setHotkeyInt();
            
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
           // NativeMethods.UnregisterHotKey(this.Handle, 1);
            getHotkeyInt();

            Settings.Default.server = textBox1.Text;

            //  Properties.Settings.Default.verbose = checkBox_verb.Checked;
            Settings.Default.replaceDrive = checkBox2.Checked;
            Settings.Default.useDash = checkBox3.Checked;
            Settings.Default.rpi = textBox2.Text;
            Settings.Default.port = textBox_Port.Text;
            Settings.Default.username = textBox_Username.Text;
            Settings.Default.kodi_hotkey = checkBox_kodi.Checked;
            //  Properties.Settings.Default.output = textBox_output.Text;

            string passtext = textBox_Password.Text;

           

            // Data to protect. Convert a string to a byte[] using Encoding.UTF8.GetBytes().
            byte[] plaintext = Encoding.Default.GetBytes(textBox_Password.Text); ;
           

            // Generate additional entropy (will be used as the Initialization vector)
            byte[] entropy = new byte[20];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(entropy);
            }

            byte[] ciphertext = ProtectedData.Protect(plaintext, entropy,
                DataProtectionScope.CurrentUser);

            //https://stackoverflow.com/questions/1766610/how-to-store-int-array-in-application-settings
            Settings.Default.cipher = ciphertext;
            Settings.Default.entropy = entropy;

            Settings.Default.combopathlist.Clear();

            foreach (object item in comboBox_download.Items)
            {
                Settings.Default.combopathlist.Add(item.ToString());
            }


            //var parent = (Form1)Owner;
            //parent.comboBox1.SelectedIndex = comboBox_res.SelectedIndex;

            //  write preferences settings
            Settings.Default.Save();

          
        }

       

      

        //private void checkBox2_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (!checkBox_link.Checked)
        //    {
        //        textBox1.Visible = false;
        //        textBox2.Visible = false;
              
        //    }
        //    else
        //    {
        //        textBox1.Visible = true;
        //        textBox2.Visible = true;
                
        //    }

        //}

        private void getHotkeyInt()
        {
           
            //bin from checkboxes?? 
            int spec_a = checkBox_a.Checked ? 1 : 0;
            int spec_c = checkBox_c.Checked ? 2 : 0;
            int spec_s = checkBox_s.Checked ? 4 : 0;
            int spec_w = checkBox_w.Checked ? 8 : 0;
           
            byte[] charByte = Encoding.ASCII.GetBytes(textBox_hot.Text.ToString());
#if DEBUG
            Console.WriteLine(charByte[0]);
#endif
            int spec_key = spec_a + spec_c + spec_s + spec_w;
            Settings.Default.specKey = spec_key;
            Settings.Default.hotkey = charByte[0];
            //         NativeMethods.RegisterHotKey(this.Handle, 1, spec_key, charByte[0]);  //ALT-Y

            int spec_a2 = checkBox_a2.Checked ? 1 : 0;
            int spec_c2 = checkBox_c2.Checked ? 2 : 0;
            int spec_s2 = checkBox_s2.Checked ? 4 : 0;
            int spec_w2 = checkBox_w2.Checked ? 8 : 0;

            byte[] charByte2 = Encoding.ASCII.GetBytes(textBox_hot2.Text.ToString());
#if DEBUG
            Console.WriteLine(charByte2[0]);
#endif
            int spec_key2 = spec_a2 + spec_c2 + spec_s2 + spec_w2;
            Settings.Default.specKey2 = spec_key2;
            Settings.Default.hotkey2 = charByte2[0];

        }

        private void setHotkeyInt()
        {
            checkBox_a.Checked = false;
            checkBox_c.Checked = false;
            checkBox_s.Checked = false;
            checkBox_w.Checked = false;

            //Modifier keys codes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8  must be added
            var spec_key = Settings.Default.specKey;
            var binary = Convert.ToString(spec_key, 2);
            binary = binary.PadLeft(4, '0');
            char a = binary[3]; if (a.Equals('1')) checkBox_a.Checked = true;
            char c = binary[2]; if (c.Equals('1')) checkBox_c.Checked = true;
            char s = binary[1]; if (s.Equals('1')) checkBox_s.Checked = true;
            char w = binary[0]; if (w.Equals('1')) checkBox_w.Checked = true;

            var hotlabel = (char)Settings.Default.hotkey;
            textBox_hot.Text = hotlabel.ToString();

            checkBox_a2.Checked = false;
            checkBox_c2.Checked = false;
            checkBox_s2.Checked = false;
            checkBox_w2.Checked = false;

            //Modifier keys codes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8  must be added
            var spec_key2 = Settings.Default.specKey2;
            var binary2 = Convert.ToString(spec_key2, 2);
            binary2 = binary2.PadLeft(4, '0');
            char a2 = binary2[3]; if (a2.Equals('1')) checkBox_a2.Checked = true;
            char c2 = binary2[2]; if (c2.Equals('1')) checkBox_c2.Checked = true;
            char s2 = binary2[1]; if (s2.Equals('1')) checkBox_s2.Checked = true;
            char w2 = binary2[0]; if (w2.Equals('1')) checkBox_w2.Checked = true;

            var hotlabel2 = (char)Settings.Default.hotkey2;
            textBox_hot2.Text = hotlabel2.ToString();

        }


        private void textBox_hot_Click(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.SelectAll();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.colSearch = comboBox1.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.colDupli = comboBox2.SelectedIndex;
        }

        private void textBox_Password_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_res_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.maxres = comboBox_res.SelectedIndex;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }








        private void button_edit_Click(object sender, EventArgs e)
        {
            if (comboBox_download.SelectedIndex !=0)
            comboBox_download.Items.Remove(comboBox_download.SelectedItem);

            comboBox_download.SelectedIndex = 0;
            Settings.Default.combodown = 0;           

        }

        private void Button_update_Click(object sender, EventArgs e)
        {
            string filename = NativeMethods.GetFullPathFromWindows("youtube-dl.exe");

            if (string.IsNullOrEmpty(filename)/* && string.IsNullOrEmpty(NewPath)*/)
            {
                filename = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\youtube-dl.exe";
            }

            
            ProcessStartInfo ps = new ProcessStartInfo();

            ps.ErrorDialog = false;
            ps.FileName = "CMD";
            ps.Arguments = "/K " + filename + " -U";
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


        }

        private void ComboBox_all_Click(object sender, EventArgs e)
        {
            ComboBox obj = sender as ComboBox;
            obj.DroppedDown = true;
        }

        private void checkBox_kodi_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_kodi.Checked)
            {
                panel1.Visible = true;
            }
            else
            {
                panel1.Visible = false;
            }
        }
    }
}
