using System.ComponentModel;
using Exiled.API.Interfaces;

namespace OccasionalBlackout
{
    public class Config : IConfig
    {
        [Description("Wether or not the plugin should be enabled")]
        public bool IsEnabled { get; set; } = true;
        [Description("Should a cassie message be sent whenever a blackout happens")]
        public bool ShouldAnnounceMessage { get; set; } = true;
        [Description("Message for cassie to announce whenever a blackout happens")]
        public string AnnounceMessage { get; set; } = "pitch_0.8 generator failure .G3 pitch_1.1 jam_025_5 lights will be shut pitch_0.8 off jam_025_2 until generators jam_030_4 are back up and operational";
        [Description("Delay after cassie message is sent and when blackout happens")]
        public int DelayMessageAndEvent { get; set; } = 15;
        [Description("Chance for blackouts to happen during rounds")]
        public int Chance { get; set; } = 25;
        [Description("Should cassie announcements for blackout rounds be enabled")]
        public bool BlackoutRoundAnnounce { get; set; } = true;
        [Description("Should cassie announce blackout round")]
        public string BlackoutRoundAnnouncementMessage { get; set; } = "Facility generator malfunction";
        [Description("Minimum time between blackouts (in seconds)")]
        public int MinDelayTime { get; set; } = 180;
        [Description("Maximum time between blackouts (in seconds)")]
        public int MaxDelayTime { get; set; } = 720;
        [Description("Minimum time a blackout will last")]
        public int MinTime { get; set; } = 30;
        [Description("Maximum time a blackout will last")]
        public int MaxTime { get; set; } = 120;
        [Description("Should cassie announce lights on")]
        public bool ShouldAnnounceBackMessage { get; set; } = true;
        [Description("Message for cassie to announce whenever a blackout happens")]
        public string AnnounceBackMessage { get; set; } = "pitch_0.95 facility is back in operational mode";
        public bool Debug { get; set; } = false;
    }
}