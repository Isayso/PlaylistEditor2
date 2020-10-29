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
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



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
                    NotificationBox.Show("Kodi response: OK", 1300, NotificationMsg.OK);


#if DEBUG
                    MessageBox.Show(responseText);
                    Console.WriteLine(responseText);
                    Console.ReadLine();
#endif
                }
                else if (responseText.Contains("error") /*&& link.Contains("Playlist.Add")*/)
                {
                    NotificationBox.Show("Kodi response: ERROR", 1300, NotificationMsg.ERROR);

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


    }
}
