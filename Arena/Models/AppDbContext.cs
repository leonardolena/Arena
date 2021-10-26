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

}
