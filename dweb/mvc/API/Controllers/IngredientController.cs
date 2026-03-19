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
    public class IngredientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IngredientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ingredient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientDTO>>> GetIngredients()
        {
            var ingredientList = new List<IngredientDTO>();
            var ingredients = await _context.Ingredients.ToListAsync();

            foreach (var ingredient in ingredients)
            {
                ingredientList.Add(new IngredientDTO()
                {
                    IngredientId = ingredient.IngredientId,
                    Name = ingredient.Name,
                    Description = ingredient.Description
                });
            }

            return ingredientList;
        }

        // GET: api/Ingredient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientDTO>> GetIngredient(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            var ingredientName = new IngredientDTO()
            {
                IngredientId = ingredient.IngredientId,
                Name = ingredient.Name,
                Description = ingredient.Description
            };

            return ingredientName;
        }

        // PUT: api/Ingredient/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(int id, IngredientDTO ingredient)
        {
            if (id != ingredient.IngredientId)
            {
                return BadRequest();
            }

            var ingredientChanged = new Ingredient()
            {
                IngredientId = ingredient.IngredientId,
                Name = ingredient.Name,
                Description = ingredient.Description
            };

            _context.Entry(ingredientChanged).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
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

        // POST: api/Ingredient
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ingredient>> PostIngredient(IngredientDTO ingredient)
        {
            var ingredientExists = await _context.Ingredients.FirstOrDefaultAsync(i => i.Name == ingredient.Name);
            if (ingredientExists != null)
            {
                return BadRequest("This ingredient already exists");
            }

            var newIngredient = new Ingredient()
            {
                Name = ingredient.Name,
                Description = ingredient.Description
            };
            
            _context.Ingredients.Add(newIngredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngredient", new { id = ingredient.IngredientId }, ingredient);
        }

        // DELETE: api/Ingredient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientExists(int id)
        {
            return _context.Ingredients.Any(e => e.IngredientId == id);
        }
    }
}
