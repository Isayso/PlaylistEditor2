using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace PlaylistEditor
{
    class ClassYTExplode
    {
        public static IReadOnlyList<VideoOnlyStreamInfo> VideoOnlyStreamInfos;

        public static string videoUrlnew;
        public static string videoTitle;
        public static string audioUrl;
        public static Video VideoInfo;

        private static YoutubeClient _youtube;

        public static async Task PullInfo(string videoId)
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

        private static VideoQuality SetVideoQuality(int height = 2)
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

        private static Container SetFileContainer(int fileext)
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

                case 2:
                    extension = Container.Tgpp;
                    break;
            }
            return extension;
        }

        public static async Task PullDASH(string videoId, int height = 2)
        {
            _youtube = new YoutubeClient();



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

        public static async Task DownloadStream(string videoId, string NewPath, int height = 2)
        {

            _youtube = new YoutubeClient();
            var converter = new YoutubeConverter(_youtube); // re-using the same client instance for efficiency, not required


            try
            {

                // Get stream manifest
                var streamManifest = await _youtube.Videos.Streams.GetManifestAsync(videoId);

                // Select audio stream
                var audioStreamInfo = streamManifest.GetAudio().WithHighestBitrate();

                // Select video stream
                //  var videoStreamInfo = streamManifest.GetVideo().FirstOrDefault(s => s.VideoQualityLabel == "1080p60");
                var videoStreamInfo = streamManifest.GetVideoOnly()
                  .FirstOrDefault(s => s.VideoQuality <= SetVideoQuality(height));

                //var videoStreamInfo = streamManifest.GetVideoOnly()
                //                      .Where(s => s.VideoQuality <= ClassHelp.SetVideoQuality(height))
                //                      .Where(t => t.Container == Container.Mp4)
                //                      .Select(h => h.Url).ToList();
                //  var test = streamManifest.get


                // Combine them into a collection
                var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };

                VideoInfo = await _youtube.Videos.GetAsync(videoId);  //video info
                videoTitle = (VideoInfo.Title + ".mp4").Replace("/", "");


                // Download and process them into one file
                await converter.DownloadAndProcessMediaStreamsAsync(streamInfos, videoTitle, "mp4");

            }
            catch (Exception ex)
            {
                MessageBox.Show("error " + ex.Message, "Stream Download", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }





    }
}
