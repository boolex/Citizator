using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Citizator.Model
{
    public class Place
    {
        private readonly JObject soruce;
        public Place(JObject soruce)
        {
            this.soruce = soruce;
        }
        public string name { get { return soruce["name"].ToString(); } }
        public string place_id { get { return soruce["place_id"].ToString(); } }
        public string id { get { return soruce["id"].ToString(); } }
        public string reference { get { return soruce["reference"].ToString(); } }
        public string scope { get { return soruce["scope"].ToString(); } }
        public string icon { get { return soruce["icon"].ToString(); } }
        public string vicinity { get { return soruce["vicinity"].ToString(); } }
        public IEnumerable<string> types { get { return soruce["vicinity"].Select(x => x.ToString()); } }
        public Geometry geometry { get { return new Geometry(JObject.Parse(soruce["geometry"].ToString())); } }
    }

    public class Geometry
    {
        private readonly JObject source;
        public Geometry(JObject source)
        {
            this.source = source;
        }
        private JObject location
        {
            get { return JObject.Parse(source["location"].ToString()); }
        }
        public string lat
        {
            get
            {
                return location["lat"].ToString().Replace(",", ".");
            }
        }
        public string lng
        {
            get
            {
                return location["lng"].ToString().Replace(",", ".");
            }
        }
        public string formattedLocation
        {
            get
            {
                return string.Format("{0},{1}", lat, lng);
            }
        }
        /*
         "geometry": {
    "location": {
      "lat": 49.422983,
      "lng": 26.987133099999991
    },
    "viewport": {
      "northeast": {
        "lat": 49.4638529,
        "lng": 27.093279
      },
      "southwest": {
        "lat": 49.357251,
        "lng": 26.8972381
      }
    }
  }
         */
    }

    public class Photo
    {
        private readonly JObject source;
        public Photo(JObject source)
        {
            this.source = source;
        }
        /*
         *  "photos": [
    {
      "height": 431,
      "html_attributions": [
        "\<a href=\\\"https://maps.google.com/maps/contrib/107447613389815807970/photos\\\"\>ÐœÐ°Ñ€Ð¸Ð½Ð° ÐŸÑƒÑÑ‚Ð¾Ð²Ð°\<\/a\>"
      ],
      "photo_reference": "CmRaAAAA7S_xBNA9fD5DAaTrycx8gPPpYaH_AYB9ZL-TBku1kZyoe_XajFiwcnFZ3TDAZpIsrb0KvxA2zrQMhA7_IoLCjM89wouviKh3D1lNQ_HPhPKRBHlgq9825x4WjlhsAOp3EhBzryS1Bqo9lLFtt7x7itnpGhQkwm0X4lOhL56iQ7lVwSS4tvhlVQ",
      "width": 588
    }
  ],
         */
    }
}
