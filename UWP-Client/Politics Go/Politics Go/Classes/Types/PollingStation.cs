using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace Politics_Go.Classes
{
    public class PollingStation
    {
        public PollingStation()
        {
            this.NormalizedAnchorPoint = new Point(0.5, 1);
            this.ImageSourceUri = new Uri("ms-appx:///Assets/UI/PollingStationActive.png", UriKind.RelativeOrAbsolute);
        }
        public string DisplayName { get; set; }
        public int ID { get; set; }
        public Geopoint Location { get; set; }
        public Uri ImageSourceUri { get; set; }
        public Point NormalizedAnchorPoint { get; set; }
    }
}
