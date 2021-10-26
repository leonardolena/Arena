namespace ArenaApplication
{
   
    public class Weapon
    {
        public Weapon(int damage) {
            _damage = damage;
        }
        public int GetDamage() { return _damage; }
        private readonly int _damage;
    }
}