using System;
using System.Linq;

namespace ArenaApplication
{
  
    
    public abstract class Gladiator
    {
        
        protected Gladiator() {
            Health = MAX_HEALTH;
        }
        public virtual Weapon Weapon { get;}
        public int Id;
        public int Health { get; set; }
        public bool HasDied => Health == 0;
        public string Name { get; set; }

        protected const int MAX_HEALTH = 100;
        public abstract int Attack();
      
    }
    class Retiarius : Gladiator
    {
        public Retiarius() : base() {

        }

        public static Weapon Net = new(10);
        public static Weapon Knife = new(30);

        public override int Attack() {
            if (GetModifier() < 1) return (int)(Knife.GetDamage() * GetModifier() + 1);
            else return (int)(Net.GetDamage() * GetModifier());
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

    class Secutor : Gladiator
    {
        public static Weapon Stiletto = new(45);

        public Secutor() : base() {
        }


        public override int Attack() {
            return (int)GetModifier() % GetDamage();
        }

        public static int GetDamage() {
            return Stiletto.GetDamage();
        }

        public double GetModifier() {
            return Health;
        }
    }

    class Mirmillo : Gladiator
    {
        public static Weapon Gladium = new(80);

        public Mirmillo() : base() {
        }

        public override int Attack() {
            return (int)(GetDamage() * GetModifier());
        }

        public static int GetDamage() {
            return Gladium.GetDamage();
        }

        public static double GetModifier() {
            return (new Random().NextDouble() / 4 + 0.25)+1;
        }
    }

    class Thraex : Gladiator
    {
        public static Weapon Scimitar = new(75);

        public Thraex() : base() {
        }

        public override int Attack() {
            return GetDamage() * (int)GetModifier();
        }
        
        public static double GetModifier() {
            return new Random().Next(3, 7) / 4 + 1;
        }
        public static int GetDamage() {
            return Scimitar.GetDamage();
        }
    }

    class Hoplomachus : Gladiator
    {
        public static Weapon Spear = new(100);

        public Hoplomachus() : base() {
        }

        public override int Attack() {
            var r = new Random().Next(3);
            return r switch
            {
                (0) => r * GetDamage(),
                (1) => r * GetDamage() / 2,
                (2) => r * GetDamage(),
                _ => 0,
            };
        }
        public static int GetDamage() {
            return Spear.GetDamage();
        }

     
    }

}