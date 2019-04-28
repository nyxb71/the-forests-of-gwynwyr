namespace game {
    public struct Location {
        public readonly uint x, y;

        public Location(uint a, uint b) {
            (x, y) = (a, b);
        }
    }

    public enum Direction {
        north, south, east, west
    }

}
