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
    public class DeviceController : ControllerBase
    {
        private IApplicationDbContext _dbcontext;

        public DeviceController(IApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var devices = await _dbcontext.Device.ToListAsync<Devices>();
            return Ok(devices);
        }

        
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var device = await _dbcontext.Device.Where(devId => devId.DeviceId == id).FirstOrDefaultAsync();
            return Ok(device);
        }

        
        [HttpPost]
        [Route("Create/{id}")]
        public async Task<ActionResult> Create([FromBody] Devices device)
        {
            _dbcontext.Device.Add(device);
            await _dbcontext.SaveChanges();
            return Ok(device.DeviceId);
        }


        [HttpPut]
        [Route("Update/{id}")]
        public async Task<ActionResult> Update(string id, Devices device)
        {
            var deviceUpt = await _dbcontext.Device.Where(devId => devId.DeviceId == id).FirstOrDefaultAsync();
            if (deviceUpt == null) 
                return Ok("Device does not exist");

            deviceUpt.Status = device.Status;
            deviceUpt.IsActive = device.IsActive;

            await _dbcontext.SaveChanges();
            return Ok("Device details successfully updated");
        }


        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var deviceDel = await _dbcontext.Device.Where(devId => devId.DeviceId == id).FirstOrDefaultAsync();
            if (deviceDel == null)
                return Ok("Device does not exist");

            _dbcontext.Device.Remove(deviceDel);
            await _dbcontext.SaveChanges();
            return Ok("Device successfully deleted");
        }
    }
}
