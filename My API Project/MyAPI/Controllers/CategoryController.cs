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
    public class CategoryController : ControllerBase
    {
        private IApplicationDbContext _dbcontext;

        public CategoryController(IApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var category = await _dbcontext.Category.ToListAsync<Categories>();
            return Ok(category);
        }


        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var category = await _dbcontext.Category.Where(catId => catId.CategoryId == id).FirstOrDefaultAsync();
            return Ok(category);
        }


        [HttpPost]
        [Route("Create/{id}")]
        public async Task<ActionResult> Create([FromBody] Categories category)
        {
            _dbcontext.Category.Add(category);
            await _dbcontext.SaveChanges();
            return Ok(category.CategoryId);
        }


        [HttpPut]
        [Route("Update/{id}")]
        public async Task<ActionResult> Update(string id, Categories category)
        {
            var categoryUpt = await _dbcontext.Category.Where(catId => catId.CategoryId == id).FirstOrDefaultAsync();
            if (categoryUpt == null)
                return Ok("Category does not exist");

            categoryUpt.CategoryName = category.CategoryName;
            categoryUpt.CategoryDescription = category.CategoryDescription;
            categoryUpt.DateCreated = category.DateCreated;

            await _dbcontext.SaveChanges();
            return Ok("Category details successfully updated");
        }


        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var categoryDel = await _dbcontext.Category.Where(catId => catId.CategoryId == id).FirstOrDefaultAsync();
            if (categoryDel == null)
                return Ok("Category does not exist");

            _dbcontext.Category.Remove(categoryDel);
            await _dbcontext.SaveChanges();
            return Ok("Category successfully deleted");
        }
    }
}
