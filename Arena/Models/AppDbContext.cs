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
        public AppDbContext(DbContextOptions options) : base(options) {
        }
        public DbSet<Happening> Happenings { get; set; }
        public DbSet<FighterEntity> Gladiators { get; set; }
        public async void Request(FighterEntity ge) {
            if (Gladiators.Find(ge.Id) == null) {
                Gladiators.Add(ge);
                await SaveChangesAsync();
            }
        }
        public async void Request(Happening h) {
            await Happenings.AddAsync(h);
            await SaveChangesAsync();
        }
        public async void Delete (FighterEntity ge) {
            Gladiators.Remove(ge);
            await SaveChangesAsync();
        }
        public List<FighterEntity> GetList() {
            return Gladiators.ToList();
        }
        public Task<List<Happening>> GetList(int n) {
            return Happenings.OrderBy(h => h.Time).TakeLast(n).ToListAsync();
        }
    }
}
