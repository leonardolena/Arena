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
            if (_arena.IsStopped) _arena.Recover(FighterConverter.Recover(_db.GetList()));
            _arena.UserAction = false;
            _arena.Start();           
        }

        [HttpGet] 
        public async Task<List<string>> LastEvents([FromHeader] int n=0) {
            if (n > 0) { 
                var list = await _db.GetList(n);
                return list.Select(h => h.ToString()).ToList();
            } else {
                throw new QueryException();
            }
        }
        [HttpGet]
        public List<FighterEntity> AliveGladiators() {
            var list= _arena.GetQueue().Select(g => FighterConverter.Convert(g)).ToList();
            return list;
        }

        [HttpPost]
        public void Stop() {
            _arena.Stop();
        }   
    }
}
