using System.Linq;

namespace ArenaApplication
{


    public abstract class Fighter
    {
        protected const int MAX_HEALTH = 100;
        
        protected Fighter() {
            Health = MAX_HEALTH;
        }
        public int Strength = 100;
        public virtual Weapon Weapon { get; }
        public int Health { get; set; }
        public bool HasDied => Health <= 0; 
        public string Name { get; set; }
        public abstract int Attack();     
    }

}