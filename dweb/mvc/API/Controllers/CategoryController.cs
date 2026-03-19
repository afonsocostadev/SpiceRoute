using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using API.Models.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categoryList = new List<CategoryDTO>();
            
            var categories = await _context.Categories.ToListAsync();
            foreach (var category in categories)
            {
                categoryList.Add( new CategoryDTO()
                {
                    Name = category.Name
                });
            }

            return categoryList;
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            
            if (category == null)
            {
                return NotFound();
            }

            var categoryName = new CategoryDTO
            {
                CategoryId =  category.CategoryId,
                Name = category.Name
            };
                    
            
            return categoryName;
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryDTO category)
        {
            
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            var categoryChanged = new Category()
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };

            _context.Entry(categoryChanged).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryDTO category)
        {
            var categoryExists = await _context.Categories.FirstOrDefaultAsync(c => c.Name.Equals(category.Name));
            if (categoryExists != null)
            {
                return BadRequest("This category already exists");
            }
            var newCategory = new Category() { Name = category.Name };
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = newCategory.CategoryId }, category);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
