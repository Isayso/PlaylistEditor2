using PlaylistEditor.Properties;
using System;
using System.Linq;
using static PlaylistEditor.ClassDataset;
using static PlaylistEditor.ClassHelp;



namespace PlaylistEditor
{

    internal class ClassImport
    {
        private static string PLUG = "plugin://";

        private static readonly string YTPLUGIN = PLUG + Settings.Default.YTPLUGIN,
            VIPLUGIN = PLUG + Settings.Default.VIPLUGIN,
            LBRYPLUGIN = PLUG + Settings.Default.LBRYPLUGIN,
            RBLPLUGIN = PLUG + Settings.Default.RBLPLUGIN,
            DMPLUGIN1 = PLUG + Settings.Default.DMPLUGIN1,
            BCPLUGIN = PLUG + Settings.Default.BCPLUGIN,
            DMPLUGIN2 = "&mode=playVideo"; //;mode=playVideo&quot"; //plugin.video.dailymotion_com/?url=

        private static string ytPluginLink = "";
        private static string YTURL = "https://www.youtube.com/watch?v=";


        public static string GetDailyPlugin(string yt_Link)
        {

            string[] key_em = yt_Link.Split('/');
            return DMPLUGIN1 + key_em[key_em.Length - 1] + DMPLUGIN2;

        }


        public static string GetVimeoPlugin(string yt_Link)
        {
            //https://player.vimeo.com/video/510059443

            string[] key_em = yt_Link.Split('/');
            return VIPLUGIN + key_em[key_em.Length - 1];

        }

        public static string GetLbryPlugin(string yt_Link)
        {
            //https://odysee.com/@A_TODO_ROCK:5/Rammstein---Du-Hast-(Official-Video):d
            //https://odysee.com/$/embed/Odysee-Exclusive---Covid-19-Asymptomatic-Transmission-Small-Video-/ed70dcaab657e03154a9a89743273131b8419871?&autoplay=1&auto_play=true
            //https://odysee.com/nz-scientist-examines-pfizer-jab-under-the-microscope:621c1f345273491c809420409f8298610c4ad7f0?src=embed

            if (yt_Link.Contains("embed"))
            {
                //yt_Link = yt_Link.Replace("https://odysee.com/$/embed/", "");
                string[] key_en = yt_Link.Split('/');
                return LBRYPLUGIN + key_en[5];
            }
            else
                return LBRYPLUGIN + yt_Link.Split('/').Last();

        }

        public static string GetRumblePlugin(string yt_Link)
        {
            //https://rumble.com/vf5wzp-episode-833-the-house-that-fauci-built-the-ccp-the-who-and-the-nih-in-wuhan.html

            string[] key_em = yt_Link.Split('/');
            return RBLPLUGIN + key_em[key_em.Length - 1] + "&mode=4&play=2";

        }

        public static string GetBCPlugin(string yt_Link)
        {
            //https://rumble.com/vf5wzp-episode-833-the-house-that-fauci-built-the-ccp-the-who-and-the-nih-in-wuhan.html
            string url = yt_Link;

            url = url.Trim('/').Replace("https://", "");
            string[] key = url.Split('/');
            key[0] = key[1] + "/" + key[2]; //.Split('/').Last();

            return BCPLUGIN + key[0].Replace("video/", "");

        }

        /// <summary>
        /// imports html and local links
        /// </summary>
        /// <param name="yt_Link"></param>
        public static string GetHTMLPlugin(string yt_Link)
        {
            string url = yt_Link;

            if (url.StartsWith("\\\\"))
            {
                ytPluginLink = "nfs:" + url.Replace("\\", "/").Trim();
            }
            else if (url.Contains(@":\"))
            {
                ytPluginLink = url;
            }
            else  //html
            {
                ytPluginLink = url;
            }

            return ytPluginLink;

        }

        public static string GetYTPlugin(string yt_Link)
        {
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

            string url = "";
            if (yt_Link.Contains("youtube.com") || yt_Link.Contains("www.youtube-nocookie.com") || yt_Link.Contains("youtu.be"))
            {
                if ((yt_Link.Contains("embed") || yt_Link.Contains("youtu.be/")) && !yt_Link.Contains("=youtu.be/"))  //variant embed link
                {
                    string[] key_em = yt_Link.Split('?');
                    key_em[0] = key_em[0].Split('/').Last();
                    ytPluginLink = YTPLUGIN + key_em[0];
                    // yt_Link = "https://www.youtube.com/watch?v=" + key_em[0];
                    url = YTURL + key_em[0];
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
                        url = YTURL + key[2];

                    }
                }

                else if (yt_Link.Contains("music.youtube"))
                {
                    string[] key = yt_Link.Split('=');  //variant normal or YT playlist link
                    if (key.Length > 1)     //if channel has no '='
                    {
                        if (key[1].Contains('&'))
                            key[1] = key[1].Split('&').First();

                        ytPluginLink = YTPLUGIN + key[1];
                        url = YTURL + key[1];

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
                        url = YTURL + key[1];


                    }
                }


                if (string.IsNullOrEmpty(ytPluginLink))
                {
                    ytPluginLink = "Link N/A";
                }


                // Is Data Text?

                // if (yLink.GetDataPresent(DataFormats.Text) && ytPluginLink != "Link N/A")
//                if (!string.IsNullOrEmpty(yt_Link) && ytPluginLink != "Link N/A")
//                {
//                    string name = GetTitle_client(url);  //new client

//#if DEBUG
//                    Console.WriteLine(name);
//#endif
//                    AddLink2Grid(name, ytPluginLink);
//                }
//                else
//                {
//                    NotificationBox.Show("Wrong input. Use full YouTube link", 2000, NotificationMsg.ERROR);
//                }
            }
            return ytPluginLink;


        }

        public static string Convert2Kodi(string yt_Link)
        {
            string convLink ="";

            VideoType linktype = ValidLinkCheck(yt_Link);

            switch (linktype)
            {
                case VideoType.Invalid:
                    return "";

                case VideoType.YT:
                case VideoType.YList:
                case VideoType.YMusic:
                    convLink = GetYTPlugin(yt_Link);
                    break;

                case VideoType.Vim:
                    convLink = GetVimeoPlugin(yt_Link);
                    break;

                case VideoType.Rmbl:
                    convLink = GetRumblePlugin(yt_Link);
                    break;

                case VideoType.Lbry:
                    convLink = GetLbryPlugin(yt_Link);
                    break;

                case VideoType.Daily:
                    convLink = GetDailyPlugin(yt_Link);
                    break;

                case VideoType.BitC:
                    convLink = GetBCPlugin(yt_Link);
                    break;

                case VideoType.Html:
                    convLink = GetHTMLPlugin(yt_Link);
                    break;
            }

            return convLink;

        }


    }
}
