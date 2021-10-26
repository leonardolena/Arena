using ArenaApplication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arena.Models
{
    public class AppDbContext: DbContext
    {
        
        
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Happening> Happenings{get;set;}
        public DbSet<GladiatorEntity> Gladiators{get;set;}
        public Task<int> Request(GladiatorEntity ge) {
            if (Gladiators.Find(ge.Id) == null) Gladiators.Add(ge);
            return SaveChangesAsync();
        }
        public async Task<int> Request(IEnumerable<Happening> hh) {
            await Happenings.AddRangeAsync(hh);
            return await SaveChangesAsync();
        }
        public Task<int> Delete(GladiatorEntity ge) {
            Gladiators.Remove(ge);
            return SaveChangesAsync();
        }
        public List<GladiatorEntity> GetList() {
            return Gladiators.ToList();
        }
        public Task<List<Happening>> GetList(int n) {
            return Happenings.OrderBy(h => h.Time).TakeLast(n).ToListAsync();
        }
        

    }

    public class GladiatorEntity
    {
        public int Id { get; set; }
        public string Type { get; internal set; }
        public string Name { get; internal set; }
    }

    public class Happening
    {
        public long Time { get; set; } 
        public string Event { get; set; }
        public int BearerGladiatorId { get; set; }
        public int RemainingHp { get; set; }
        public int TriggererGladiatorId { get; set; }



    }

}
