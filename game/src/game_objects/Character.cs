using System;

namespace game {
    public class Character : IGo, ILook {
        public Zone CurrentZone { get; private set; }
        public uint Health {get; private set;}
        public bool Alive {get; private set;}

        public Character(Zone current_zone, uint health) {
            this.CurrentZone = current_zone;
            this.Health = health;
            this.Alive = true;
        }

        public void Go(Zone zone) {
            this.CurrentZone = zone;
        }

        public string Look(Direction dir) => CurrentZone.Look(dir);

        public void TakeDamage(uint dmg) {
            if (Health >= dmg) {
                Health -= dmg;
            }
            else {
                Health = 0;
                Die();
            }
        }

        public void Die() {
            Alive = false;
        }
    }
}
