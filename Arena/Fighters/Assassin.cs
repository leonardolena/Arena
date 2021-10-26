namespace ArenaApplication
{
    class Assassin : Fighter
    {
        public Weapon Stiletto = new(45);
        public int Damage => Stiletto.GetDamage()*Strength/100;

        public override int Attack() {
            return (int)GetModifier() % Damage;
        }

        public double GetModifier() {
            return Health;
        }
    }
}