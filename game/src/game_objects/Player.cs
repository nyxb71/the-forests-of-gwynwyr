using System;

namespace game
{
    public class Player : Character
    {
        public uint Experience {get; private set;}

        public void AddExperience(uint xp)
        {
            Experience += xp;
        }

        public uint Level() => (uint)Math.Floor(Math.Sqrt(Experience)) ;
    }
}
