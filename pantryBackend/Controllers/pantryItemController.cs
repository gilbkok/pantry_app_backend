using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pantryBackend.Data;
using pantryBackend.Models;

namespace pantryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class pantryItemController : ControllerBase
    {
        private readonly pantryDbContext _pantryItemsContext;
        public pantryItemController(pantryDbContext pantryItemContext)
        {
            _pantryItemsContext = pantryItemContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<pantryItems>> GetAllItems()
        {
            return _pantryItemsContext.pantryItems;
        }
        [HttpGet]
        [Route("{pid}")]

        public async Task<IActionResult> GetItem([FromRoute] int pid)
        {
            var item = await _pantryItemsContext.pantryItems.FirstOrDefaultAsync(x => x.pid == pid);
            if (item != null)
            {
                return Ok(item);
            }
            return NotFound("pantry item not found");
        }

        [HttpPost]

        public void PostItems(pantryItems Items)
        {
            _pantryItemsContext.pantryItems.Add(Items);
            _pantryItemsContext.SaveChanges();

        }

        [HttpPut]
        [Route("{pid}")]

        public async Task<IActionResult> updateItem(pantryItems items, int pid)
        {
            var foundItem= await _pantryItemsContext.pantryItems.FirstOrDefaultAsync(x => x.pid == items.pid);
            if (foundItem != null)
            {
                foundItem.weight = items.weight;
                foundItem.tCalories = items.tCalories;
                foundItem.iName = items.iName;
                foundItem.image=items.image;
                await _pantryItemsContext.SaveChangesAsync();
                return Ok(foundItem);
            }
            return NotFound("pantry item not found");

            }
        [HttpDelete]
        [Route("{pid}")]
        public async Task<IActionResult> deleteItem([FromRoute] int pid)
        {
            var foundItem = await _pantryItemsContext.pantryItems.FirstOrDefaultAsync(x => x.pid == pid);
            if (foundItem != null)
            {
                _pantryItemsContext.Remove(foundItem);
                _pantryItemsContext.SaveChanges();
                return Ok(foundItem);
            }
            return NotFound("opreration failed");
        }

    }
}

