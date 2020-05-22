using System;
using System.Collections.Generic;
using System.IO;
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

                //case 2:
                //    extension = Container.Tgpp;
                //    break;
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

        public static async Task DownloadStream(string videoId, string NewPath, int height = 2, int fileext=0)
        {
            _youtube = new YoutubeClient();
            var converter = new YoutubeConverter(_youtube); // re-using the same client instance for efficiency, not required

            if (fileext < 2)
            {
                try
                {
                    string[] filetype = { "mp4", "webm" };

                    // Get stream manifest
                    var streamManifest = await _youtube.Videos.Streams.GetManifestAsync(videoId);

                    // Select audio stream
                    var audioStreamInfo = streamManifest.GetAudio().WithHighestBitrate();

        /*            { 327, new ItagDescriptor(Container.WebM, AudioEncoding.Aac, null, null)},
        { 338, new ItagDescriptor(Container.WebM, AudioEncoding.Opus, null, null)},
        { 339, new ItagDescriptor(Container.WebM, AudioEncoding.Vorbis, null, null)}
        */
                    // Select video stream
                    //  var videoStreamInfo = streamManifest.GetVideo().FirstOrDefault(s => s.VideoQualityLabel == "1080p60");
                    //var videoStreamInfo = streamManifest.GetVideoOnly()
                    //  .FirstOrDefault(s => s.VideoQuality <= SetVideoQuality(height));

                    var videoStreamInfo2 = streamManifest.GetVideoOnly()
                                          .Where(s => s.VideoQuality <= SetVideoQuality(height))
                                          .Where(t => t.Container == SetFileContainer(fileext))
                                         // .Select(h => h.Url).ToList();
                                         .First()
                                          ;

                    // Combine them into a collection
                    var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo2 };

                    VideoInfo = await _youtube.Videos.GetAsync(videoId);  //video info
                    videoTitle = NewPath + "\\" + RemoveSpecialCharacters(VideoInfo.Title) + "." + filetype[fileext];

                    if (ClassHelp.MyFileExists(videoTitle, 3000))
                    {
                        switch (MessageBox.Show("File exists, overwrite?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                        {
                            case DialogResult.Yes:
                                // Download and process them into one file
                                await converter.DownloadAndProcessMediaStreamsAsync(streamInfos, videoTitle, filetype[fileext]);
                                break;

                            case DialogResult.No:
                                //  return "error";
                                break;
                        }
                    }
                    else
                    {
                        // Download and process them into one file
                        await converter.DownloadAndProcessMediaStreamsAsync(streamInfos, videoTitle, filetype[fileext]);

                    }


                }
                catch (Exception e)
                {
                    MessageBox.Show(videoTitle + Environment.NewLine + e.Message, "Video Download", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    videoTitle = "error";
                }


            }

            else  //audio only
            {
                try
                {

                    VideoInfo = await _youtube.Videos.GetAsync(videoId);  //video info
                    videoTitle = NewPath + "\\" + RemoveSpecialCharacters(VideoInfo.Title) + ".mp3";

                    if (ClassHelp.MyFileExists(videoTitle, 3000))
                    {
                        switch (MessageBox.Show("File exists, overwrite?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                        {
                            case DialogResult.Yes:
                                // Download and process them into one file
                                await converter.DownloadVideoAsync(videoId, videoTitle);
                                break;

                            case DialogResult.No:
                                //  return "error";
                                break;
                        }
                    }
                    else
                    {
                        // Download and process them into one file
                        await converter.DownloadVideoAsync(videoId, videoTitle);

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




    }
}
