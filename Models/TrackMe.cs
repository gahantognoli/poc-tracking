namespace POC.Tracking.Models
{
    public class TrackMe
    {
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public dynamic Location { get; set; }
    }
}