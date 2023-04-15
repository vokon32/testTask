
namespace datatest4.Models
{
    public class Walk
    {
        public int Id { get; set; }
        public decimal FirstLatitude { get; set; }
        public decimal FirstLongitude { get; set; }
        public decimal LastLatitude { get; set; }
        public decimal LastLongitude { get; set; }
        public DateTime FirstDateTrack { get; set; }
        public DateTime LastDateTrack { get; set; }
        
        private double distance { get; set; }
        public double Distance
        {
            get
            {
                distance = Math.Round(CalculateDistance(FirstLatitude, FirstLongitude, LastLatitude, LastLongitude),2);
                return distance; 
            }

        }
        
        private double duration { get; set; }
        public double Duration
        {
            get
            {
                duration = Math.Round((LastDateTrack - FirstDateTrack).TotalMinutes,2);
                return duration;
            }
        }
      
        private static double CalculateDistance(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
        {
            const decimal radiusOfEarthInKm = 6371;

            var latDistance = DegreeToRadians((double)(lat2 - lat1));
            var lonDistance = DegreeToRadians((double)(lon2 - lon1));

            var a = Math.Sin(latDistance / 2) * Math.Sin(latDistance / 2) +
                    Math.Cos(DegreeToRadians((double)lat1)) * Math.Cos(DegreeToRadians((double)lat2)) *
                    Math.Sin(lonDistance / 2) * Math.Sin(lonDistance / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distance = (double)radiusOfEarthInKm * c;

            return distance;
        }

        private static double DegreeToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}
