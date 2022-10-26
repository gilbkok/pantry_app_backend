using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pantryBackend.Data;
using pantryBackend.Models;

namespace pantryBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class recipes1Controller : ControllerBase
    {
        private readonly pantryDbContext _context;

        public recipes1Controller(pantryDbContext context)
        {
            _context = context;
        }

        // GET: api/recipes1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<recipes>>> Getrecipe()
        {
            return await _context.recipe.ToListAsync();
        }

        // GET: api/recipes1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<recipes>> Getrecipes(int id)
        {
            var recipes = await _context.recipe.FindAsync(id);

            if (recipes == null)
            {
                return NotFound();
            }

            return recipes;
        }

        // PUT: api/recipes1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putrecipes(int id, recipes recipes)
        {
            if (id != recipes.rid)
            {
                return BadRequest();
            }

            _context.Entry(recipes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!recipesExists(id))
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

        // POST: api/recipes1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<recipes>> Postrecipes(recipes recipes)
        {
            _context.recipe.Add(recipes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getrecipes", new { id = recipes.rid }, recipes);
        }

        // DELETE: api/recipes1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleterecipes(int id)
        {
            var recipes = await _context.recipe.FindAsync(id);
            if (recipes == null)
            {
                return NotFound();
            }

            _context.recipe.Remove(recipes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool recipesExists(int id)
        {
            return _context.recipe.Any(e => e.rid == id);
        }
    }
}
