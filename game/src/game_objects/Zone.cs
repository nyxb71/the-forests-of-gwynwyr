using System;
using System.Collections.Generic;
using System.Linq;

using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace game {
    public class Zone {
        public readonly string Name;
        public readonly Location Loc;

        private readonly Dictionary<Direction, string> LookTexts;
        private readonly Dictionary<string, string> EventTexts;

        public Zone(string name,
                    Location loc,
                    Dictionary<Direction, string> look_texts,
                    Dictionary<string, string> event_texts)
        {
            this.Name = name;
            this.Loc = loc;
            this.LookTexts = look_texts;
            this.EventTexts = event_texts;
        }

        public string OnEntry() => EventTexts["Entry"];
        public string OnLook(Direction dir) => LookTexts[dir];

        public static bool IsAdjacent(Zone a, Zone b) {
            // https://snipplr.com/view/64354/check-if-coordinates-are-adjacent/
            return (Math.Abs(a.Loc.x - b.Loc.x) +
                    Math.Abs(a.Loc.y - b.Loc.y)) == 1 ?
                    true : false;
        }

        public bool IsAdjacent(Zone b) {
            return IsAdjacent(this, b);
        }
    }
}
