namespace game {
    public class Character : IGo, ILook {
        private Location loc;
        public uint Health {get; private set;}
        public bool Alive {get; private set;}

        public void Go(Direction dir) {
            // attempt to exit in {dir} direction
        }

        public void Look(Direction dir) {
            // look in {dir} direction
        }

        public void TakeDamage(uint dmg) {
            if (Health >= dmg) {
                Health -= dmg;
            }
            else {
                Health = 0;
                Alive = false;
            }
        }
    }
}
