using System;

namespace ArenaApplication
{
    class Orc : Fighter
    {
        public Weapon Broadsword = new(80);
        public int Damage => Broadsword.GetDamage()*Strength/100;

        public override int Attack() {
            return (int)(Damage * GetModifier());
        }
        
        public static double GetModifier() {
            return new Random().NextDouble() / 4 + 1.25;
        }
    }
}