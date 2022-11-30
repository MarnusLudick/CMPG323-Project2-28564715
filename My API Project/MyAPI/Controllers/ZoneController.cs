using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.DbContexts;
using MyAPI.Models;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private IApplicationDbContext _dbcontext;

        public ZoneController(IApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var zones = await _dbcontext.Zone.ToListAsync<Zones>();
            return Ok(zones);
        }


        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var zone = await _dbcontext.Zone.Where(zoneId => zoneId.ZoneId == id).FirstOrDefaultAsync();
            return Ok(zone);
        }


        [HttpPost]
        [Route("Create/{id}")]
        public async Task<ActionResult> Create([FromBody] Zones zone)
        {
            _dbcontext.Zone.Add(zone);
            await _dbcontext.SaveChanges();
            return Ok(zone.ZoneId);
        }


        [HttpPut]
        [Route("Update/{id}")]
        public async Task<ActionResult> Update(string id, Zones zone)
        {
            var zoneUpt = await _dbcontext.Zone.Where(zoneId => zoneId.ZoneId == id).FirstOrDefaultAsync();
            if (zoneUpt == null)
                return Ok("Zone does not exist");

            zoneUpt.ZoneName = zone.ZoneName;
            zoneUpt.ZoneDescription = zone.ZoneDescription;
            zoneUpt.DateCreated = zone.DateCreated;

            await _dbcontext.SaveChanges();
            return Ok("Zone details successfully updated");
        }


        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var zoneDel = await _dbcontext.Zone.Where(zoneId => zoneId.ZoneId == id).FirstOrDefaultAsync();
            if (zoneDel == null)
                return Ok("Zone does not exist");

            _dbcontext.Zone.Remove(zoneDel);
            await _dbcontext.SaveChanges();
            return Ok("Zone successfully deleted");
        }
    }
}
