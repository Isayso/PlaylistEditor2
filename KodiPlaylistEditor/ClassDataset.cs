using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistEditor
{
    class ClassDataset
    {
        public enum ValidVideoType { Invalid, YT, YList, BitC, Html, Rmbl, Vim, Daily, Lbry }

        public string[] _videotypes;
        public string[] VideoTypes
        {
            get
            {
                return _videotypes = new[] {
                    ".m4v", ".3g2", ".3gp",".nsv",".tp",".ts",".ty",".strm",".pls",".rm",
                    ".rmvb",".mpd",".m3u",".m3u8",".ifo",".mov",".qt",".divx",".xvid",".bivx",".vob",".nrg",".pva",
                    ".wmv",".asf",".asx",".ogm",".m2v",".avi",".dat",".mpg",".mpeg",".mp4",".mkv",".mk3d",".avc",
                    ".vp3",".svq3",".nuv",".viv",".dv",".fli",".flv",".001",".wpl",".vdr",".dvr-ms",".xsp",".mts",
                    ".m2t",".m2ts",".evo",".ogv",".sdp",".avs",".rec",".url",".pxml",".vc1",".h264",".rcv",".rss",
                    ".mpls",".webm",".bdmv",".wtv",".trp",".f4v"};

            }
        }


    }
}
