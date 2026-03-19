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
    public class RecipeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecipeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Recipe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDTO>>> GetRecipes()
        {
            var recipeList = new List<RecipeDTO>();
            var recipes = await _context.Recipes.ToListAsync();
            foreach (var recipe in recipes)
            {
                recipeList.Add(new RecipeDTO()
                {
                    RecipeId = recipe.RecipeId,
                    Name = recipe.Name,
                    Description = recipe.Description,
                    Portions = recipe.Portions,
                    CookingTime = recipe.CookingTime,
                    PrepTime = recipe.PrepTime,
                    Preparation =  recipe.Preparation,
                });
            }

            return recipeList;
        }

        // GET: api/Recipe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDTO>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            var recipeName = new RecipeDTO()
            {
                RecipeId = recipe.RecipeId,
                Name = recipe.Name,
                Description = recipe.Description,
                Portions = recipe.Portions,
                CookingTime = recipe.CookingTime,
                Preparation = recipe.Preparation,
                PrepTime = recipe.PrepTime,
            };

            return recipeName;
        }

        // PUT: api/Recipe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, RecipeDTO recipe)
        {
            if (id != recipe.RecipeId)
            {
                return BadRequest();
            }

            var recipeChanged = new Recipe()
            {
                RecipeId = recipe.RecipeId,
                Name = recipe.Name,
                Description = recipe.Description,
                Portions = recipe.Portions,
                CookingTime = recipe.CookingTime,
                Preparation = recipe.Preparation,
                PrepTime = recipe.PrepTime,
            };

            _context.Entry(recipeChanged).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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

        // POST: api/Recipe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(RecipeDTO recipe)
        {
            var recipeExists = await _context.Recipes.FirstOrDefaultAsync(e => e.Name == recipe.Name);
            if (recipeExists != null)
            {
                return BadRequest("This recipe already exists");
            }

            var newRecipe = new Recipe()
            {
                Name = recipe.Name,
                Description = recipe.Description,
                Portions = recipe.Portions,
                CookingTime = recipe.CookingTime,
                Preparation = recipe.Preparation,
                PrepTime = recipe.PrepTime,
            };
            
            _context.Recipes.Add(newRecipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new { id = recipe.RecipeId }, recipe);
        }

        // DELETE: api/Recipe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.RecipeId == id);
        }
    }
}
