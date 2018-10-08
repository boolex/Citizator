using Citizator.Model;
using System.Collections.Generic;
namespace Citizator.Scenarios
{
    public class PlacesAround
    {
        private readonly string key;
        private readonly string location;
        private readonly int radius;
        private readonly string language;
        public PlacesAround(
            string key,
            string location,
            int radius,
            string language)
        {
            this.key = key;
            this.location = location;
            this.radius = radius;
            this.language = language;
        }

        public List<Place> Get()
        {
            var places = new List<Place>();
            var placesClient = new Citizator.Program.NearbyPlacesSearch(
                key: key,
                location: locPlace.geometry.formattedLocation,
                radius: 1000,
                language: "uk-UA,ua;q=0.8"
                );
            places.AddRange(placesClient.Result["results"].Select(y => new Place(JObject.Parse(y.ToString()))))
            if (placesClient.Result["next_page_token"] != null)
            {

            }
            return places;
        }
    }
}
