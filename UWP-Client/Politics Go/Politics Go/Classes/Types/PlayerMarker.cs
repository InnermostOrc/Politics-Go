using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace Politics_Go.Classes
{
    public class PlayerMarker
    {
        public PlayerMarker()
        {
            this.NormalizedAnchorPoint = new Point(0.5, 1);
        }
        public Geopoint Location { get; set; }
        public Uri ImageSourceUri { get; set; }
        public Point NormalizedAnchorPoint { get; set; }
    }
}
