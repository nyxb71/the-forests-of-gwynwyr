using System;
using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace game {

    public enum Direction {
        north, south, east, west
    }

    public class Location {
        /* coordinates grow to the right and up

            (0,2) (1,2) (2,2)
            (0,1) (1,1) (2,1)
            (0,0) (1,0) (2,0)
        */

        public readonly int x, y;

        public Location(int a, int b) {
            (x, y) = (a, b);
        }

        // https://snipplr.com/view/64354/check-if-coordinates-are-adjacent/
        public static bool IsAdjacent(Location a, Location b) =>
         ((Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y)) == 1) ?
                true : false;

        public static Option<Direction> DirectionTo(Location a, Location b) {
            if (a.x == b.x && a.y == b.y) {
                return None;
            }

            if (a.y == b.y) {
                return (b.x > a.x) ?
                    Direction.east : Direction.west;
            }

            if (a.x == b.x) {
                return (b.y > a.y) ?
                    Direction.north : Direction.south;
            }

            return None;
        }

    }
}
