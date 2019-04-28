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

        // TODO: make this an event
        public string OnEntry() => EventTexts["Entry"];

        public void Look(Direction dir) {
            // send look event
        }

        // https://snipplr.com/view/64354/check-if-coordinates-are-adjacent/
        public static bool IsAdjacent(Location a, Location b) =>
         ((Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y)) == 1) ?
                true : false;

        public static bool IsAdjacent(Zone a, Zone b) =>
            IsAdjacent(a.Location, b.Location);

        public bool IsAdjacent(Zone b) => IsAdjacent(this, b);

        public Option<Direction> DirectionTo(Zone a, Zone b) {
            if (!IsAdjacent(a, b)) {
                return None;
            }

            if (a.Location.y == b.Location.y) {
                return (b.Location.x > a.Location.x) ?
                    Direction.east : Direction.west;
            }

            if (a.Location.x == b.Location.x) {
                return (b.Location.y < a.Location.y) ?
                    Direction.north : Direction.south;
            }

            return None;
        }

        public Option<Direction> DirectionTo(Zone b) => DirectionTo(this, b);
    }
}
