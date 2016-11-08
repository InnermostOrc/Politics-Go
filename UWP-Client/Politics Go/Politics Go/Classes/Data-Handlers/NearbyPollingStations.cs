using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Devices.Geolocation;

namespace Politics_Go.Classes
{
    public class NearbyPollingStations
    {
        public async Task<List<PollingStation>> FetchPOLLs(BasicGeoposition center)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri("http://api.nerdthings.ga/Politics_Go/MapObjects/pollingStations.php?lat=" + center.Latitude + "&long=" + center.Longitude));
            //var jsonString = response.ToString();
            var jsonString = await response.Content.ReadAsStringAsync();
            if (jsonString.Length > 0) {
            JsonArray root = JsonValue.Parse(jsonString).GetArray();
            List<PollingStation> polls = new List<PollingStation>();
            for (uint i = 0; i < root.Count; i++)
            {
                string name = root.GetObjectAt(i).GetNamedString("Name");
                string id = root.GetObjectAt(i).GetNamedString("ID");
                string lat = root.GetObjectAt(i).GetNamedString("Lat");
                string longi = root.GetObjectAt(i).GetNamedString("Longi");
                polls.Add(new PollingStation()
                {
                    DisplayName = name,
                    ID = int.Parse(id),
                    Location = new Geopoint(new BasicGeoposition()
                    {
                        Latitude = Convert.ToDouble(lat),
                        Longitude = Convert.ToDouble(longi)
                    })
                });
            };
            return polls;
            }
            else
            {
                return null;
            }
        }
    }
}
