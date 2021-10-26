using System;

namespace ArenaApplication
{
    class Knight : Fighter
    {
        public Weapon Spear = new(100);
        public int Damage => Spear.GetDamage()*Strength/100;

        public override int Attack() {
            var r = new Random().Next(3);
            return r switch
            {
                (0) => r * Damage,
                (1) => r * Damage / 2,
                (2) => r * Damage,
                _ => 0,
            };
        }
    }
}