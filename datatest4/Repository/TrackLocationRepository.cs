using datatest4.Data;
using datatest4.Interfaces;
using datatest4.Models;
using Microsoft.EntityFrameworkCore;

namespace datatest4.Repository
{
    public class TrackLocationRepository : ITrackLocationRepository
    {
        private readonly ApplicationDbContext _context;
        private static Walk Walk;
        public TrackLocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TrackLocation>> GetAllTracks()
        {
            return await _context.TrackLocations.ToListAsync();
        }

        public IEnumerable<Walk> GetAllWalks(IEnumerable<TrackLocation> trackLocations, AppUser user)
        {
            var previousTrack = trackLocations.FirstOrDefault();
            var walks = new List<Walk>();
           
            bool isFirst = true; 
            
            foreach (var tracklocation in trackLocations)
            {
                if (user.Imei == tracklocation.Imei && tracklocation.TypeSource == 1)
                {
                    if (isFirst)
                    {
                        Walk = new Walk() 
                        { 
                            FirstDateTrack = tracklocation.DateTrack, 
                            FirstLatitude = tracklocation.Latitude, 
                            FirstLongitude = tracklocation.Longitude 
                        };
                        
                        isFirst = false;
                    }

                    if ((tracklocation.DateTrack - previousTrack.DateTrack).TotalMinutes > 30)
                    {
                        Walk.LastLatitude = tracklocation.Latitude;
                        Walk.LastLongitude = tracklocation.Longitude;
                        Walk.LastDateTrack = tracklocation.DateTrack;
                        walks.Add(Walk);
                        isFirst = true;
                    }
                    
                    previousTrack = tracklocation;
                }
            }
            return walks;
        }
    }
}
