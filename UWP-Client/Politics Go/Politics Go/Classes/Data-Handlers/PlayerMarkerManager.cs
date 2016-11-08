using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace Politics_Go.Classes
{
    public class PlayerMarkerManager
    {
        public List<PlayerMarker> FetchMarker(Geopoint center)
        {
            List<PlayerMarker> marker = new List<PlayerMarker>();
            marker.Add(new PlayerMarker()
            {
                ImageSourceUri = new Uri("ms-appx:///Assets/PinMarker.png", UriKind.RelativeOrAbsolute),
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = center.Position.Latitude,
                    Longitude = center.Position.Longitude
                })
            });
            return marker;
        }
    }
}
