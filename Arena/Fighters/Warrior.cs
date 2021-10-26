using System;

namespace ArenaApplication
{
    class Warrior : Fighter
    {
        public Weapon Scimitar = new(75);
        public int Damage => Scimitar.GetDamage()*Strength/100;

        public override int Attack() {
            return Damage * (int)GetModifier();
        }
        
        public static double GetModifier() {
            return new Random().Next(3, 7) / 4 + 1;
        }
    }

}