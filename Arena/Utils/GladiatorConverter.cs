
using Arena.Models;
using ArenaApplication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arena.Utils
{
    static class FighterConverter
    {
        private static readonly Dictionary<string, Fighter> _ctors = new() {
            { "Knight", new Knight() },
            { "Assassin", new Assassin() },
            { "Mage", new Mage() },
            { "Orc", new Orc() },
            { "Warrior", new Warrior() },
        };

        public static void Validate(FighterDTO ge) {
            if (string.IsNullOrEmpty(ge.Name)) { 
                throw new SubmitException();
            }
            ge.Type = ge.Type[0..1].ToUpper() + ge.Type[1..].ToLower();
            if(!_ctors.ContainsKey(ge.Type)) {
                throw new SubmitException();
            }                     
        }

        public static FighterEntity Convert(FighterDTO dto) {
            var fe = new FighterEntity 
            {
                Name = dto.Name,
                Type = dto.Type,
            };
            if(dto.Strength > 50 && dto.Strength < 150) {
                fe.Strength = dto.Strength;
            } else {
                fe.Strength = 100;
            }
            return fe;
        }
        
        private static Fighter Convert(FighterEntity obj) {
            Fighter g = _ctors.GetValueOrDefault(obj.Type);
            g.Name = obj.Name;
            g.Health *= obj.Strength/100;
            g.Strength= obj.Strength;
            return g;
        }

        public static Fighter GetGladiator() {
            var r = new Random().Next(6);
            var g = _ctors.Values.ToArray()[r];
            g.Name = Guid.NewGuid().ToString();
            return g;
        }

        public static FighterEntity Convert(Fighter g) {
            return new FighterEntity
            {
                Name = g.Name,
                Type = g.GetType().ToString().Split('.').Last(),
            };        
        }

        public static List<Fighter> Recover(IEnumerable<FighterEntity> gladiators) {
            var list = new List<Fighter>();
            foreach(var g in gladiators) {
                list.Add(Convert(g));
            }
            return list;
        }
        
        public static bool Check(FighterEntity x, FighterDTO qe) {
            bool r = false;       
            r |= x.Id == qe.Id;    
            r |= x.Name == qe.Name;
            r |= x.Type == qe.Type;      
            r |= x.Strength == x.Strength;      
            return r;
        }
    }
}