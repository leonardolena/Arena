using Arena.Models;
using Arena.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arena.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GladiatorController : Controller
    {
        private AppDbContext _db;
        private ILogger _logger;

        public GladiatorController(AppDbContext db,ILogger logger) {
            _db = db;
            _logger = logger;
        }
     

        [HttpPut]
        public void Insert([FromBody] FighterDTO dto) {
            HappeningManager.NewHappening += _db.Request;
            FighterConverter.Validate(dto);
            var fe = FighterConverter.Convert(dto);
            _db.Request(fe);
        }

        [HttpPut]
        public void Update(FighterDTO qe, [FromBody] FighterDTO arg) {
            var found = _db.Gladiators.SingleOrDefault(x => FighterConverter.Check(x, qe));
            if(found is null) {
                throw new QueryException();
            }  
            if(arg.Name != null){
                found.Name = arg.Name; 
            }
            if(arg.Strength != default) {
                found.Strength = arg.Strength;
            } 
            if(arg.Type != null) {
                found.Type = arg.Type;
            } 
            _db.SaveChanges();            
        }
        [HttpDelete] 
        public void Delete(FighterDTO qe) {
            try {
                var found = _db.Gladiators.Single(x => FighterConverter.Check(x, qe));
                _db.Delete(found);
            } catch(Exception ex){
                _logger.LogInformation(ex, message: "Error occurred during Delete");
                throw new QueryException();
            }
        }


    }
}
