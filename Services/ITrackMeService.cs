using POC.Tracking.Models;

namespace POC.Tracking.Services
{
    public interface ITrackMeService
    {
        TrackMe TrackMe(dynamic location);
    }
}