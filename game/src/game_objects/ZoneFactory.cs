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
                { Direction.north, "You look north" },
                { Direction.south, "You look south" },
                { Direction.east,  "You look east" },
                { Direction.west,  "You look west" }
            };

            var default_event_texts = new Dictionary<string, string>() {
                {"entry", "entry event"},
                {"exit", "exit event"},
            };

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    zones.Add(new Zone(
                        $"Z({i},{j})",
                        new Location(i, j),
                        default_look_texts,
                        default_event_texts
                    ));
                }
            }

            return zones;
        }
    }
}
