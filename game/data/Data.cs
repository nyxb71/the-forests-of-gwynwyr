using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace game
{
    public static class GameData
    {
        public static List<Zone> LoadZones(string zones_path) =>
            JsonConvert.DeserializeObject<List<Zone>>(File.ReadAllText(zones_path));
    }
}
