﻿using System.Collections.Generic;
using Cimbalino.Phone.Toolkit.Extensions;
using Cimbalino.Phone.Toolkit.Services;
using MediaBrowser.Model.Dlna;
using MediaBrowser.Model.Dlna.Profiles;
using System.Xml.Serialization;

namespace MediaBrowser.Dlna.Profiles
{
    [XmlRoot("Profile")]
    public class WindowsPhoneProfile : DefaultProfile
    {
        private WindowsPhoneProfile()
        {
            Name = "Windows Phone";
        }

        public static WindowsPhoneProfile GetProfile(float maxBitrate = 2000000, bool isHls = false)
        {
            var screenInfo = new ScreenInfoService();
            var profile = new WindowsPhoneProfile();
            var transcodingProfiles = new List<TranscodingProfile>
            {
                new TranscodingProfile
                {
                    Container = "mp3",
                    AudioCodec = "mp3",
                    Type = DlnaProfileType.Audio
                },
                new TranscodingProfile
                {
                    Container = "mp4",
                    VideoCodec = "h264",
                    AudioCodec = "aac",
                    Type = DlnaProfileType.Video,
                    Context = EncodingContext.Static
                }
            };

            if (isHls)
            {
                transcodingProfiles.Add(new TranscodingProfile
                {
                    Protocol = "hls",
                    Container = "ts",
                    VideoCodec = "h264",
                    AudioCodec = "aac",
                    Type = DlnaProfileType.Video,
                    Context = EncodingContext.Streaming
                });
            }

            transcodingProfiles.Add(new TranscodingProfile
            {
                Container = "mp4",
                VideoCodec = "h264",
                AudioCodec = "aac",
                Type = DlnaProfileType.Video,
                Context = EncodingContext.Streaming
            });

            profile.TranscodingProfiles = transcodingProfiles.ToArray();
            
            profile.DirectPlayProfiles = new[]
            {
                new DirectPlayProfile
                {
                    Container = "mp4,mov",
                    VideoCodec = "h264",
                    AudioCodec = "aac,mp3",
                    Type = DlnaProfileType.Video
                },

                new DirectPlayProfile
                {
                    Container = "mp4,avi",
                    VideoCodec = "mpeg4,msmpeg4",
                    AudioCodec = "aac,mp3",
                    Type = DlnaProfileType.Video
                },

                new DirectPlayProfile
                {
                    Container = "asf",
                    VideoCodec = "wmv2,wmv3,vc1",
                    AudioCodec = "wmav2,wmapro,wmavoice",
                    Type = DlnaProfileType.Video
                },

                new DirectPlayProfile
                {
                    Container = "asf",
                    AudioCodec = "wmav2,wmapro,wmavoice",
                    Type = DlnaProfileType.Audio
                },

                new DirectPlayProfile
                {
                    Container = "mp4,aac",
                    AudioCodec = "aac",
                    Type = DlnaProfileType.Audio
                },

                new DirectPlayProfile
                {
                    Container = "mp3",
                    AudioCodec = "mp3",
                    Type = DlnaProfileType.Audio
                },

                new DirectPlayProfile
                {
                    Container = "jpeg,png,gif,bmp",
                    Type = DlnaProfileType.Photo
                }
            };

            profile.CodecProfiles = new[]
            {
                new CodecProfile
                {
                    Type = CodecType.Video,
                    Codec="h264",
                    Conditions = new []
                    {
                        new ProfileCondition
                        {
                            Condition = ProfileConditionType.LessThanEqual,
                            Property = ProfileConditionValue.Width,
                            Value = screenInfo.Size.Height.ToStringInvariantCulture()
                        },
                        new ProfileCondition
                        {
                            Condition = ProfileConditionType.LessThanEqual,
                            Property = ProfileConditionValue.Height,
                            Value = screenInfo.Size.Width.ToStringInvariantCulture()
                        },
                        new ProfileCondition
                        {
                            Condition = ProfileConditionType.LessThanEqual,
                            Property = ProfileConditionValue.VideoBitrate,
                            Value = maxBitrate.ToStringInvariantCulture(), //"1000000",
                            IsRequired = false
                        },
                        new ProfileCondition
                        {
                            Condition = ProfileConditionType.LessThanEqual,
                            Property = ProfileConditionValue.VideoFramerate,
                            Value = "24",
                            IsRequired = false
                        },
                        new ProfileCondition
                        {
                            Condition = ProfileConditionType.LessThanEqual,
                            Property = ProfileConditionValue.VideoLevel,
                            Value = "3"
                        },
                        new ProfileCondition(ProfileConditionType.EqualsAny, ProfileConditionValue.VideoProfile, "baseline|constrained baseline"),
                        new ProfileCondition
                        {
                            Condition = ProfileConditionType.NotEquals,
                            Property = ProfileConditionValue.IsAnamorphic,
                            Value = "true"
                        }
                    }
                },

                new CodecProfile
                {
                    Type = CodecType.Video,
                    Conditions = new []
                    {
                        new ProfileCondition
                        {
                            Condition = ProfileConditionType.LessThanEqual,
                            Property = ProfileConditionValue.Width,
                            Value = "800"
                        },
                        new ProfileCondition
                        {
                            Condition = ProfileConditionType.LessThanEqual,
                            Property = ProfileConditionValue.Height,
                            Value = "480"
                        },
                        new ProfileCondition
                        {
                            Condition = ProfileConditionType.LessThanEqual,
                            Property = ProfileConditionValue.VideoBitrate,
                            Value = "1000000",
                            IsRequired = false
                        },
                        new ProfileCondition
                        {
                            Condition = ProfileConditionType.LessThanEqual,
                            Property = ProfileConditionValue.VideoFramerate,
                            Value = "24",
                            IsRequired = false
                        },
                        new ProfileCondition
                        {
                            Condition = ProfileConditionType.NotEquals,
                            Property = ProfileConditionValue.IsAnamorphic,
                            Value = "true"
                        }
                    }
                },

                new CodecProfile
                {
                    Type = CodecType.VideoAudio,
                    Conditions = new []
                    {
                        new ProfileCondition
                        {
                            Condition = ProfileConditionType.LessThanEqual,
                            Property = ProfileConditionValue.AudioBitrate,
                            Value = "128000"
                        },

                        new ProfileCondition
                        {
                            Condition = ProfileConditionType.LessThanEqual,
                            Property = ProfileConditionValue.AudioChannels,
                            Value = "2"
                        }
                    }
                },

                new CodecProfile
                {
                    Type = CodecType.Audio,
                    Conditions = new []
                    {
                        new ProfileCondition
                        {
                            Condition = ProfileConditionType.LessThanEqual,
                            Property = ProfileConditionValue.AudioBitrate,
                            Value = "128000"
                        }
                    }
                }
            };

            return profile;
        }
    }
}