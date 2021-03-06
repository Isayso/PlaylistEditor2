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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode;
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
                videoUrlnew = "noDash";
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
