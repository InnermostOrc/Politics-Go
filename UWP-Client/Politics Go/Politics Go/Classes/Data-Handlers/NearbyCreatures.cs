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
    public class NearbyCreatures
    {
        public async Task<List<Creature>> FetchCreatures(BasicGeoposition center)
        {
            /*List<Creature> creatures = new List<Creature>();
            creatures.Add(new Creature()
            {
                Name = "Donald Trump", //Name of the creature
                ID = 001, //The creature ID not instance ID
                ImageWidth = 38, //Image width
                ImageHeight = 64, //Image height
                JustNearby = false,
                ImageSourceUri = new Uri("ms-appx:///Assets/Creatures/001_map.png", UriKind.RelativeOrAbsolute), //Creature image
                Location = new Geopoint(new BasicGeoposition() //Creature's location
                {
                    Latitude = 57.547073, //Creatures Latitide
                    Longitude = -2.954040 //Creatures Longitude
                })
            });
            creatures.Add(new Creature()
            {
                Name = "Minister Trump", //Name of the creature
                ID = 002, //The creature ID not instance ID
                ImageWidth = 34, //Image width
                ImageHeight = 64, //Image height
                JustNearby = false,
                ImageSourceUri = new Uri("ms-appx:///Assets/Creatures/002_map.png", UriKind.RelativeOrAbsolute), //Creature image
                Location = new Geopoint(new BasicGeoposition() //Creature's location
                {
                    Latitude = 57.543244, //Creature's Latitide
                    Longitude = -2.951601 //Creature's Longitude
                })
            });
            creatures.Add(new Creature()
            {
                Name = "Lord Trump", //Name of the creature
                ID = 003, //The creature ID not instance ID
                ImageWidth = 30, //Image width
                ImageHeight = 64, //Image height
                JustNearby = false,
                ImageSourceUri = new Uri("ms-appx:///Assets/Creatures/003_map.png", UriKind.RelativeOrAbsolute), //Creature image
                Location = new Geopoint(new BasicGeoposition() //Creature's location
                {
                    Latitude = 57.543745, //Creature's Latitide
                    Longitude = -2.951515 //Creature's Longitude
                })
            });*/

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri("http://api.nerdthings.ga/Politics_Go/MapObjects/nearbyCreatures.php?lat=" + center.Latitude + "&long=" + center.Longitude));
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