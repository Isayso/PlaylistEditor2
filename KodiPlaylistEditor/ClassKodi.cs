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
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlaylistEditor.Properties;



namespace PlaylistEditor
{
    class ClassKodi
    {
        private static readonly HttpClient _Client = new HttpClient();
       
      
        public static async Task<bool> Run(string link)
        {
            string kodiIP = Settings.Default.rpi;
            string kodiUser = Settings.Default.username;
            string kodiPort = Settings.Default.port;
            //  string kodiPass = Properties.Settings.Default.password; https://stackoverflow.com/questions/12657792/how-to-securely-save-username-password-local
            byte[] plaintext = null;
            string kodiPass = "";

            if (Settings.Default.cipher != null && Settings.Default.entropy != null)
            {
                plaintext = ProtectedData.Unprotect(Settings.Default.cipher, Settings.Default.entropy,
                                                    DataProtectionScope.CurrentUser);
                kodiPass = ClassHelp.ByteArrayToString(plaintext);
            }
           


            //var values = new Dictionary<string, string>
            //{
            //    { kodiUser,kodiPass}

            //};

            string values = kodiUser + ":" + kodiPass;
            string url = "http://" + kodiIP + ":" + kodiPort + "/jsonrpc?request=";


            //url = "http://192.168.178.91:8080/jsonrpc"; //?request=";

            try
            {
                var response = await Request(HttpMethod.Post, url, link, values);
                string responseText = await response.Content.ReadAsStringAsync();

                if (responseText.Contains("OK") /*&& link.Contains("Playlist.Add")*/)
                {
                    // Form pop = new popup2();
                    ClassHelp.PopupForm("Kodi response: OK", "green", 1300);
                   
#if DEBUG
                    MessageBox.Show(responseText);
                    Console.WriteLine(responseText);
                    Console.ReadLine();
#endif
                }
                else if (responseText.Contains("error") /*&& link.Contains("Playlist.Add")*/)
                {
                    ClassHelp.PopupForm("Kodi response: ERROR", "red", 1300);
                    return false;
                }

                kodiPass = "";  //to be safe
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kodi not responding. " + ex.Message, "Play", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }


        /// <summary>
        /// Makes an async HTTP Request
        /// </summary>
        /// <param name="pMethod">Those methods you know: GET, POST, HEAD, etc...</param>
        /// <param name="pUrl">Very predictable...</param>
        /// <param name="pJsonContent">String data to POST on the server</param>
        /// <param name="pHeaders">If you use some kind of Authorization you should use this</param>
        /// <returns></returns>
        static async Task<HttpResponseMessage> Request(HttpMethod pMethod, string pUrl, string pJsonContent, string values /*Dictionary<string, string> pHeaders*/)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = pMethod;
            httpRequestMessage.RequestUri = new Uri(pUrl);
            //foreach (var head in pHeaders)
            //{
            //    httpRequestMessage.Headers.Add(head.Key, head.Value);
            //}
            var byteArray = Encoding.ASCII.GetBytes(values/*"my_client_id:my_client_secret"*/);
            var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            httpRequestMessage.Headers.Authorization = header;



            switch (pMethod.Method)
            {
                case "POST":
                    HttpContent httpContent = new StringContent(pJsonContent, Encoding.UTF8, "application/json");
                                       
                        httpRequestMessage.Content = httpContent;
                        break;
               
            }

            return await _Client.SendAsync(httpRequestMessage);
        }


        public bool RunOnKodi(string Link)
        {
            return false;
        }


        public static async Task<bool> Run2(string link)
        {
            string kodiIP = Settings.Default.rpi;
            string kodiUser = Settings.Default.username.Trim();
            string kodiPort = Settings.Default.port;
            //  string kodiPass = Properties.Settings.Default.password; https://stackoverflow.com/questions/12657792/how-to-securely-save-username-password-local
            byte[] plaintext = null;
            string kodiPass = "";

            if (Settings.Default.cipher != null && Settings.Default.entropy != null)
            {
                plaintext = ProtectedData.Unprotect(Settings.Default.cipher, Settings.Default.entropy,
                                                    DataProtectionScope.CurrentUser);
                kodiPass = ClassHelp.ByteArrayToString(plaintext);
            }

            kodiPass = kodiPass.Trim();

            var values = new Dictionary<string, string>
            {
                {kodiUser,kodiPass}

            };

            string url = "http://" + kodiIP + ":" + kodiPort + "/jsonrpc?request=";

            //url = "http://192.168.178.91:8080/jsonrpc"; //?request=";

            try
            {
                using (var webClient = new WebClient())
                {
                    // Required to prevent HTTP 401: Unauthorized messages
                    webClient.Credentials = new NetworkCredential(kodiUser, kodiPass);
                    // API Doc: http://kodi.wiki/view/JSON-RPC_API/v6
                    //  var json = "{\"jsonrpc\":\"2.0\",\"method\":\"GUI.ShowNotification\",\"params\":{\"title\":\"This is the title of the message\",\"message\":\"This is the body of the message\"},\"id\":1}";
                    string response = webClient.UploadString($"http://{kodiIP}:{kodiPort}/jsonrpc", "POST", link);


                    if (response.Contains("OK") /*&& link.Contains("Playlist.Add")*/)
                    {
                        // Form pop = new popup2();
                        ClassHelp.PopupForm("Kodi response: OK", "ok", 1300);

#if DEBUG
                        MessageBox.Show(response);
                        Console.WriteLine(response);
                        Console.ReadLine();
#endif
                    }
                    else if (response.Contains("error") /*&& link.Contains("Playlist.Add")*/)
                    {
                        ClassHelp.PopupForm("Kodi response: ERROR", "error", 1300);
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kodi not responding. " + ex.Message, "Play", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            // https://stackoverflow.com/questions/22392362/making-a-json-rpc-http-call-using-c-sharp
           
        }


    }
}
