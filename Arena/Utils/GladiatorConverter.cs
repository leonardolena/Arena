
using Arena.Models;
using ArenaApplication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arena.Utils
{
    static class GladiatorConverter {

        public static Queue<Happening> Chronicles;

        private static readonly Dictionary<string, Gladiator> _ctors = new() {
            { "Hoplomachus", new Hoplomachus() },
            { "Secutor", new Secutor() },
            { "Mirmillo", new Mirmillo() },
            { "Retiarius", new Retiarius() },
            { "Thraex", new Thraex() },
        };

        public static void Validate(GladiatorEntity ge) {
            if (string.IsNullOrEmpty(ge.Name)) { throw new SubmitException() ; }
            ge.Type = ge.Type[0..1].ToUpper() + ge.Type[1..].ToLower();
            if(!_ctors.ContainsKey(ge.Type))throw new SubmitException();                      
        }

        private static Gladiator Convert(GladiatorEntity obj) {
            Gladiator g = _ctors.GetValueOrDefault(obj.Type);
            g.Name = obj.Name;
            g.Id = obj.Id;
            return g;
        }

        public static Gladiator GetGladiator() {
            var r = new Random().Next(6);
            var g = _ctors.Values.ToArray()[r];
            g.Name = Guid.NewGuid().ToString();
            return g;
        }

        public static GladiatorEntity Convert(Gladiator g) {
            return new GladiatorEntity
            {
                Name = g.Name,
                Type = g.GetType().ToString().Split('.').Last(),
            };        
        }

        public static List<Gladiator> Recover(IEnumerable<GladiatorEntity> gladiators) {
            var list = new List<Gladiator>();
            foreach(var g in gladiators) {
                list.Add(Convert(g));
            }
            return list;
        }
        
        public static void RaiseAttack(Gladiator bearer,Gladiator triggerer) {
            var h = new Happening
            {
                Time = System.Diagnostics.Stopwatch.GetTimestamp(),
                Event = "Attack",
                TriggererGladiatorId = triggerer.Id,
                BearerGladiatorId = bearer.Id,
                RemainingHp = bearer.Health,
            };
            
            Chronicles.Enqueue(h);
        }


        public static void RaiseWin(Gladiator g) {
            var h = new Happening
            {
                Event = "Win",
                Time = System.Diagnostics.Stopwatch.GetTimestamp(),
                BearerGladiatorId = g.Id,
            };
            Chronicles.Enqueue(h);
        }
        public static void RaiseDeath(Gladiator g) {
            var h = new Happening
            {
                Event = "Death",
                Time = System.Diagnostics.Stopwatch.GetTimestamp(),
                BearerGladiatorId = g.Id,
            };
            Chronicles.Enqueue(h);
        }
        public static void RaiseStop() {
            var h = new Happening 
            {
                Event = "Stop", 
                Time = System.Diagnostics.Stopwatch.GetTimestamp() 
            };
            Chronicles.Enqueue(h);
        }

        public static bool Check(GladiatorEntity x, List<KeyValuePair<string, string>> ge) {
            var keys = ge.Select(k => k.Key).ToArray();
            bool r = false;
            var c = x.GetType().GetProperties().Select(pi => pi.Name).Where(n => keys.Contains(n));
            foreach (var s in c) {
                r |= x.GetType().GetProperty(s).GetValue(x, null).ToString() == ge.Single(v => v.Key == s).Value;
            }
            return r;

        }
    }
}

