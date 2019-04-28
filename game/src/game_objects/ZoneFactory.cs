using System;
using System.Collections.Generic;
using System.Linq;

namespace game
{
    public static class ZoneFactory
    {
        public static List<Zone> GenerateZones(int n) {
            var zones = new List<Zone>();
            var default_look_texts = new Dictionary<Direction, string>() {
                { Direction.north, "look_north" },
                { Direction.south, "look_south" },
                { Direction.east, "look_east" },
                { Direction.west, "look_west" }
            };

            var default_event_texts = new Dictionary<string, string>() {
                {"entry", "entry event"},
                {"exit", "exit event"},
            };

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    Console.WriteLine($"GENERATED ZONE: {i},{j}");
                    zones.Add(new Zone(
                        "Zone" + i + j,
                        new Location(i, j),
                        default_look_texts,
                        default_event_texts
                    ));
                }
            }

            return zones;
        }


        // "You see a small glade before you, vermillion moss covers the rocky surface of the forest floor. Shafts of sunlight peek through a dense canopy of figs and beech trees",


    }
}
