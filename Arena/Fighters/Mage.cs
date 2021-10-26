using System;

namespace ArenaApplication
{
    class Mage : Fighter
    {
        public Weapon Spell= new(85);
        public int Damage => Spell.GetDamage();

        public override int Attack() {
            return (int)GetModifier()*Damage;
        }

        public static double GetModifier() {
            var r = new Random().Next(10) switch
            {
                < 5 => 0,
                5 => 5,
                > 5 => new Random().NextDouble()
            };
            return r;          
        }
    }

}