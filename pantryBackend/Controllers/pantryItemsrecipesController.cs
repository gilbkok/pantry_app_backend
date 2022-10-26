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
    public class pantryItemsrecipesController : ControllerBase
    {
        private readonly pantryDbContext _context;

        public pantryItemsrecipesController(pantryDbContext context)
        {
            _context = context;
        }

        // GET: api/pantryItemsrecipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<pantryItemsrecipes>>> GetpantryItemsrecipes()
        {
            return await _context.pantryItemsrecipes.Include(p => p.pantryItem).Include(p => p.recipe).ToListAsync();
        }

        // GET: api/pantryItemsrecipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<pantryItemsrecipes>> GetpantryItemsrecipes(int id)
        {
            var pantryItemsrecipes = await _context.pantryItemsrecipes.FindAsync(id);

            if (pantryItemsrecipes == null)
            {
                return NotFound();
            }

            return pantryItemsrecipes;
        }

        // PUT: api/pantryItemsrecipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutpantryItemsrecipes(int id, pantryItemsrecipes pantryItemsrecipes)
        {
            if (id != pantryItemsrecipes.pantryItempid)
            {
                return BadRequest();
            }

            _context.Entry(pantryItemsrecipes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!pantryItemsrecipesExists(id))
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

        // POST: api/pantryItemsrecipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<pantryItemsrecipes>> PostpantryItemsrecipes(pantryItemsrecipes pantryItemsrecipes)
        {
            _context.pantryItemsrecipes.Add(pantryItemsrecipes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (pantryItemsrecipesExists(pantryItemsrecipes.pantryItempid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetpantryItemsrecipes", new { id = pantryItemsrecipes.pantryItempid }, pantryItemsrecipes);
        }

        // DELETE: api/pantryItemsrecipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletepantryItemsrecipes(int id)
        {
            var pantryItemsrecipes = await _context.pantryItemsrecipes.FindAsync(id);
            if (pantryItemsrecipes == null)
            {
                return NotFound();
            }

            _context.pantryItemsrecipes.Remove(pantryItemsrecipes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool pantryItemsrecipesExists(int id)
        {
            return _context.pantryItemsrecipes.Any(e => e.pantryItempid == id);
        }
    }
}
