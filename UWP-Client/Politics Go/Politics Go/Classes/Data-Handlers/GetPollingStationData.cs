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
    class GetPollingStationData
    {
        public async Task<String> FetchPollingStationName(string id)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri("http://api.nerdthings.ga/Politics_Go/MapObjects/getPollingStationData.php?id=" + id));
            var jsonString = await response.Content.ReadAsStringAsync();
            if (jsonString.Length > 0)
            {
                JsonArray root = JsonValue.Parse(jsonString).GetArray();
                string name = root.GetObjectAt(0).GetNamedString("Name");
                return name;
            }
            else
            {
                return null;
            }
        }
}
}
