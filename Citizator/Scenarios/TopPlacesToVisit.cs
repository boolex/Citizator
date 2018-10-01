using System.Collections.Generic;
namespace Citizator.Scenarios
{
    using Citizator.Model;
    public class TopPlacesToVisit
    {
        public TopPlacesToVisit(
            IMapsPlatform mapsPlatform,
            string location,
            int count)
        {
            
        }
        private List<Place> places;
        public List<Place> Places
        {
            get { return places ?? (places = FindPlaces()); }
        }
        public List<Place> FindPlaces()
        {
            return null;
        }
    }
}
