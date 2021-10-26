using Arena.Models;
using Arena.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ArenaApplication.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ArenaController : ControllerBase
    {

        private static Arena _arena;
        private static AppDbContext _db;
        private readonly ILogger<ArenaController> _logger;

        public ArenaController(ILogger<ArenaController> logger,AppDbContext db,Arena arena) {
            _logger = logger;
            _arena = arena;
            _db = db;               
        }

        [HttpGet]
        public void Start() {
            if (_arena.HasStarted) return;
            if (_arena.IsStopped) _arena.Recover(GladiatorConverter.Recover(_db.GetList()));
            _arena.UserAction = false;
            _arena.Start();           
        }

        [HttpGet] 
        public async Task<List<Happening>> LastEvents([FromHeader] int n=0) {
            await _db.Request(GladiatorConverter.Chronicles);
            GladiatorConverter.Chronicles = null;
            if (n > 0) return await _db.GetList(n);
            else throw new QueryException();
        }
        [HttpGet]
        public List<GladiatorEntity> AliveGladiators() {
            var last= GladiatorConverter.Chronicles.Last(e => e.Event == "Attack").TriggererGladiatorId;
            GladiatorConverter.Chronicles = null;
            var entity = _db.Gladiators.Single(g => g.Id == last);
            var list= _arena.GetQueue().Select(g => GladiatorConverter.Convert(g)).ToList();
            list.Add(entity);
            return list;
        }

        [HttpPut]
        public async void Insert([FromBody] GladiatorEntity gladiator) {
            GladiatorConverter.Validate(gladiator);
            await _db.Request(gladiator);     
        }

        [HttpPost]
        public async void Stop() {
            _arena.Stop();
            await _db.Request(GladiatorConverter.Chronicles);
            GladiatorConverter.Chronicles = null;
        }

        [HttpDelete] 
        public async void Delete(List<KeyValuePair<string,string>> kv) {
            try {
                var found = _db.Gladiators.Single(x => GladiatorConverter.Check(x, kv));
                await _db.Delete(found);
            } catch (Exception ex){
                _logger.LogInformation(ex, message: "Error occurred during Delete");
                throw new QueryException();
            }
        }

        [HttpPatch]
        public void Update(List<KeyValuePair<string, string>> qe, [FromBody] List<KeyValuePair<string, string>> arg) {
            try {
                var found = _db.Gladiators.Single(x => GladiatorConverter.Check(x, qe));
                if (arg.Select(k => k.Key).Contains("Name")) {
                    found.Name = arg.Single(k => k.Key == "Name").Value;
                }
                if (arg.Select(k => k.Key).Contains("Type")) {
                    found.Name = arg.Single(k => k.Key == "Type").Value;
                }
                _db.SaveChanges();
            } catch(Exception ex) {
                _logger.LogInformation(ex,message: "Error occurred during Update");
                throw new QueryException();
            }         
        }
    }
}
