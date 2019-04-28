namespace game {
    public struct Location {
        public readonly int x, y;

        public Location(int a, int b) {
            (x, y) = (a, b);
        }
    }

    public enum Direction {
        north, south, east, west
    }

}
