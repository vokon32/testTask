using datatest4.Models;

namespace datatest4.Interfaces
{
    public interface ITrackLocationRepository
    {
        Task<IEnumerable<TrackLocation>> GetAllTracks();
        IEnumerable<Walk> GetAllWalks(IEnumerable<TrackLocation> trackLocations, AppUser user);
        double GetPassedDistancePerDay(IEnumerable<Walk> walks, AppUser user, DateTime date);

    }
}
