using System;

namespace game
{
    public class Player : Character
    {
        public uint Experience { get; private set; }
        public uint Level { get; private set; }

        public Player(Zone current_zone, uint health) :
            base(current_zone, health) => this.Experience = 0;

        public uint CalcLevel(uint xp) => (uint)Math.Floor(Math.Sqrt(xp));

        public void AddExperience(uint xp) {
            Experience += xp;

            var new_level = CalcLevel(Experience);
            if (new_level != Level) {
                Level = new_level;
                // send level up event
            }
        }
    }
}
