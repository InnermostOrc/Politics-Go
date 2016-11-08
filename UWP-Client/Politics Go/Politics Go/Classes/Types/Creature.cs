using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace Politics_Go.Classes
{
    public class Creature
    {
        public Creature()
        {
            this.NormalizedAnchorPoint = new Point(0.5, 1);
            if (this.ID == 1)
            {
                this.Name = "Donald Trump";
            }
            else if (this.ID == 2)
            {
                this.Name = "Minister Trump";
            }
            else if (this.ID == 3)
            {
                this.Name = "Lord Trump";
            }
        }
        public int UID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public Geopoint Location { get; set; }
        public Uri ImageSourceUri { get; set; }
        public Point NormalizedAnchorPoint { get; set; }
        //public int ImageWidth { get; set; }
        //public int ImageHeight { get; set; }
        //public bool JustNearby { get; set; }
    }
}
