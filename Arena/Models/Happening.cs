namespace Arena.Models
{
    public class Happening
    {
        public long Time { get; set; } 
        public string Event { get; set; }
        public string BearerGladiator { get; set; }
        public int DamageInflicted { get; set; }
        public string TriggererGladiator { get; set; }

        public override string ToString() {
            return Event switch
            {
                "Attack" => $"{TriggererGladiator} has inflicted {DamageInflicted} to {BearerGladiator}",
                "Win" => $"{BearerGladiator} has won",
                "Death" => $"{BearerGladiator} has died",
                "Stop" => $"Arena has been stopped. Everybody is safe now",
                _ => ""
            };
        }
    }

}
