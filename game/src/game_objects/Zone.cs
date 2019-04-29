using System;
using System.Collections.Generic;
using System.Linq;

using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace game {
    public class Zone : ILook {
        public readonly string Name;
        public readonly Location Location;

        private readonly Dictionary<Direction, string> LookTexts;
        private readonly Dictionary<string, string> EventTexts;

        public Zone(string name,
                    Location loc,
                    Dictionary<Direction, string> look_texts,
                    Dictionary<string, string> event_texts)
        {
            this.Name = name;
            this.Location = loc;
            this.LookTexts = look_texts;
            this.EventTexts = event_texts;
        }

        public string Look(Direction dir) => LookTexts[dir];

        public static bool IsAdjacent(Zone a, Zone b) =>
            Location.IsAdjacent(a.Location, b.Location);

        public bool IsAdjacent(Zone b) => IsAdjacent(this, b);

        public static Option<Direction> DirectionTo(Zone a, Zone b) =>
            IsAdjacent(a, b) ?
                Location.DirectionTo(a.Location, b.Location) :
                None;

        public Option<Direction> DirectionTo(Zone b) => DirectionTo(this, b);
    }
}
