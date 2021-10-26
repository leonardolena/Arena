using Arena.Models;
using ArenaApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arena.Utils
{
    public class HappeningManager
    {

        public static event Action<Happening> NewHappening;

        public static void RaiseAttack(Fighter bearer, Fighter triggerer, int damage) {
            var h = new Happening
            {
                Time = System.Diagnostics.Stopwatch.GetTimestamp(),
                Event = "Attack",
                TriggererGladiator = $"{triggerer.GetType().ToString().Split('.').Last()}, {triggerer.Name}",
                BearerGladiator = $"{bearer.GetType().ToString().Split('.').Last()}, {bearer.Name}",
                DamageInflicted = damage,
            };
            NewHappening.Invoke(h);
        }

        public static void RaiseWin(Fighter g) {
            var h = new Happening
            {
                Event = "Win",
                Time = System.Diagnostics.Stopwatch.GetTimestamp(),
                BearerGladiator = $"{g.GetType().ToString().Split('.').Last()},{g.Name}",
            };
            NewHappening.Invoke(h);
        }

        public static void RaiseDeath(Fighter g) {
            var h = new Happening
            {
                Event = "Death",
                Time = System.Diagnostics.Stopwatch.GetTimestamp(),
                BearerGladiator = $"{g.GetType().ToString().Split('.').Last()},{g.Name}",
            };
            NewHappening.Invoke(h);
        }

        public static void RaiseStop() {
            var h = new Happening
            {
                Event = "Stop",
                Time = System.Diagnostics.Stopwatch.GetTimestamp()
            };
            NewHappening.Invoke(h);
        }
    }
}