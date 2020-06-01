using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace PlaylistEditor
{

    public delegate void ValueChangedEventHandler(double value);
    public class ClassYTExplode
    {
        public static IReadOnlyList<VideoOnlyStreamInfo> VideoOnlyStreamInfos;
        public static IReadOnlyList<MuxedStreamInfo> MuxedStreamInfos;

        public static string videoUrlnew;
        public static string videoTitle;
        public static string audioUrl;
        public static Video VideoInfo;
        public Form1 frm1;

        private static YoutubeClient _youtube;
      //  public double _progress;

        //public double Progress
        //{
        //    get { return _progress; }
        //    set { _progress = value;
        //       // Console.WriteLine(_progress.ToString());
        //        test(_progress);
        //        ValueChanged?.Invoke(value);
        //    }
        //    //get => _progress;
        //    //private set => _progress;
        //}

       // public event ValueChangedEventHandler ValueChanged;


        public async Task PullInfo(string videoId)
        {
            _youtube = new YoutubeClient();
            try
            {
                VideoInfo = await _youtube.Videos.GetAsync(videoId);  //video info
                videoTitle = VideoInfo.Title;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Get Arguments failed. " + ex.Message, "Get Arguments", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        public static VideoQuality SetVideoQuality(int height = 2)
        {
            VideoQuality quality = VideoQuality.High1080;

            switch (height)
            {
                case 0:
                    quality = VideoQuality.High2160;
                    break;

                case 1:
                    quality = VideoQuality.High1440;
                    break;

                case 2:
                    quality = VideoQuality.High1080;
                    break;

                case 3:
                    quality = VideoQuality.High720;
                    break;

                case 4:
                    quality = VideoQuality.Medium480;
                    break;

                case 5:
                    quality = VideoQuality.Medium360;
                    break;

            }

            return quality;

        }

        public static Container SetFileContainer(int fileext)
        {
            Container extension = Container.Mp4;

            switch (fileext)
            {
                case 0:
                    extension = Container.Mp4;
                    break;

                case 1:
                    extension = Container.WebM;
                    break;

                    //case 2:
                    //    extension = Container.Tgpp;
                    //    break;
            }
            return extension;
        }

        public async Task PullDASH(string videoId, int height = 2)
        {
            _youtube = new YoutubeClient();
            videoUrlnew = "";

            try
            {
                // Enter busy state
                //IsBusy = true;
                //IsProgressIndeterminate = true;


                // Reset data
                //Video = null;
                //Channel = null;
                //MuxedStreamInfos = null;
                //AudioOnlyStreamInfos = null;
                //VideoOnlyStreamInfos = null;
                //ClosedCaptionTrackInfos = null;

                //  List<string> VideoOnlyStreamInfos =

                // Normalize video id
                //var videoId = new VideoId(Query!);

                // Get data
                var streamManifest = await _youtube.Videos.Streams.GetManifestAsync(videoId);
                //  var trackManifest = await _youtube.Videos.ClosedCaptions.GetManifestAsync(videoId);

                //   var   Video = await _youtube.Videos.GetAsync(videoId);  //video info

                //Channel = await _youtube.Channels.GetByVideoAsync(videoId);
                //MuxedStreamInfos = streamManifest.GetMuxed().ToArray();
                //AudioOnlyStreamInfos = streamManifest.GetAudioOnly().ToArray();
                VideoOnlyStreamInfos = streamManifest.GetVideoOnly().ToArray();
                //ClosedCaptionTrackInfos = trackManifest.Tracks;
                //var streamInfo = streamManifest
                //                 .GetVideoOnly()
                //                 .Where(s => s.Container == Container.Mp4)
                //                 .WithHighestVideoQuality();
                var testinfo = streamManifest.GetVideoOnly()
                    //  .Where(s => s.VideoQuality <= VideoQuality.High1080)
                    .Where(s => s.VideoQuality <= SetVideoQuality(height))
                    .Where(t => t.Container == Container.Mp4)
                    .Select(h => h.Url).ToList();

                videoUrlnew = testinfo[0];

                //      var streamInfoA = streamManifest.GetAudioOnly().WithHighestBitrate();
                var streamInfoA = streamManifest.GetAudio()
                            .Where(s => s.Container == Container.Mp4)
                            .WithHighestBitrate();

                audioUrl = streamInfoA.Url;

            }
            catch (Exception e)
            {
                //#if DEBUG
                MessageBox.Show("Get DASH Arguments failed. " + e.Message, "Get DASH Arguments", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //#endif

            }

            finally
            {
                // Exit busy state
                //IsBusy = false;
                //IsProgressIndeterminate = false;
            }
        }

        public static async Task PullNoDASH(string videoId, int height = 2)
        {
            _youtube = new YoutubeClient();

            try
            {

                // Get data
                var streamManifest = await _youtube.Videos.Streams.GetManifestAsync(videoId);
                //  MuxedStreamInfos = streamManifest.GetMuxed().ToArray();
                MuxedStreamInfos = streamManifest.GetMuxed()
                    .Where(s => s.VideoQuality <= SetVideoQuality(height))
                    .ToArray();
                //AudioOnlyStreamInfos = streamManifest.GetAudioOnly().ToArray();
                // VideoOnlyStreamInfos = streamManifest.GetVideoOnly().ToArray();
                //ClosedCaptionTrackInfos = trackManifest.Tracks;
                //var streamInfo = streamManifest
                //                 .GetVideoOnly()
                //                 .Where(s => s.Container == Container.Mp4)
                //                 .WithHighestVideoQuality();


                var streamInfoA = streamManifest.GetAudioOnly().WithHighestBitrate();

                audioUrl = streamInfoA.Url;

            }
            catch (Exception e)
            {
                //#if DEBUG
                MessageBox.Show("Get DASH Arguments failed. " + e.Message, "Get DASH Arguments", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //#endif

            }

            finally
            {
                // Exit busy state
                //IsBusy = false;
                //IsProgressIndeterminate = false;
            }
        }









    }
}
