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
    public class CatchableCreatures
    {
        public async Task<List<Creature>> FetchCreatures(BasicGeoposition center)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri("http://api.nerdthings.ga/Politics_Go/MapObjects/catchableCreatures.php?lat=" + center.Latitude + "&long=" + center.Longitude));
            //var jsonString = response.ToString();
            var jsonString = await response.Content.ReadAsStringAsync();
            if (jsonString.Length > 0)
            {
                JsonArray root = JsonValue.Parse(jsonString).GetArray();
                List<Creature> creatures = new List<Creature>();
                for (uint i = 0; i < root.Count; i++)
                {
                    string uid = root.GetObjectAt(i).GetNamedString("Uid");
                    string id = root.GetObjectAt(i).GetNamedString("ID");
                    string name = root.GetObjectAt(i).GetNamedString("Name");
                    string lat = root.GetObjectAt(i).GetNamedString("Lat");
                    string longi = root.GetObjectAt(i).GetNamedString("Longi");
                    creatures.Add(new Creature()
                    {
                        UID = int.Parse(uid),
                        ID = int.Parse(id),
                        Name = name,
                        Location = new Geopoint(new BasicGeoposition()
                        {
                            Latitude = Convert.ToDouble(lat),
                            Longitude = Convert.ToDouble(longi)
                        }),
                        ImageSourceUri = new Uri("ms-appx:///Assets/Creatures/" + id + "_map.png")
                    });
                };
                return creatures;
            }
            else
            {
                return null;
            }
        }
    }
}
