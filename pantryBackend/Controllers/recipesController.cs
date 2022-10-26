using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using pantryBackend.Data;
using pantryBackend.DTO;
using pantryBackend.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security;

namespace pantryBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class recipesController: ControllerBase
    {
        private readonly pantryDbContext _recipesContext;
        public recipesController(pantryDbContext recipesContext)
        {
            _recipesContext = recipesContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<recipes>> GetAllItems()
        {
            return _recipesContext.recipe;
        }

        [HttpGet]
        [Route("{rid}")]

        public async Task<ActionResult> GetRecipe([FromRoute] int rid)
        {
            
            var recipe = await _recipesContext.recipe.FirstOrDefaultAsync(x => x.rid == rid);
          
            if (recipe != null)
            {
                return Ok(recipe);
            }
            return NotFound("pantry item not found");

        }
        [HttpGet("GetRecipeItems")]
        public async Task<ActionResult<GetRecipeWithItems>> GetRecipeItems(int rid)
        {
            var recipe = await _recipesContext.recipe
                                .Where(c => c.rid == rid)
                                .FirstOrDefaultAsync();
            GetRecipeWithItems completeRecipe = new GetRecipeWithItems();

            //return Ok(recipe.pantryItem);
            if (recipe != null)
            {
                completeRecipe.id = (int)recipe.rid;
                completeRecipe.name = recipe.rName;
                completeRecipe.image = recipe.rImage;
                completeRecipe.rSteps = recipe.rSteps;


                completeRecipe.items = await _recipesContext.pantryItemsrecipes.Where(p => p.reciperid == rid).Select(p => p.pantryItem).ToListAsync();


                return Ok(completeRecipe);
            }


            return NotFound() ;
        }

        [HttpPost]

        public async Task<ActionResult<recipes>> PostRecipe(recipes recipes)
        {
            //recipes recipe = new recipes();
            //recipe.rName = name;
            //recipe.rImage = image;
            
            //foreach(var item in items)
            //{
            //    pantryItems checkedPantry = await _recipesContext.pantryItems.Where(p => p.pid == item).FirstOrDefaultAsync();

            //    if(checkedPantry == null)
            //    {
            //        return NotFound();
            //    }
            //}

            //recipe.pantryItem = (ICollection<pantryItems>?)items;
            _recipesContext.recipe.Add(recipes);
            _recipesContext.SaveChanges();
            return Ok(recipes);

        }

        [HttpPut]

       public async Task<ActionResult<recipes>> PutRecipe(int id, RecipeUpdateModel data)
       {

           recipes recipe = _recipesContext.recipe.Where(r => r.rid == id).FirstOrDefault();
           recipes newRecipe = new recipes();

           if(recipe == null)
           {
               return NotFound();
           }

           //recipe.pantryItem = (ICollection<pantryItems>?)items;

           // UPdate name and image

           if(!string.IsNullOrEmpty(data.name))
               recipe.rName = data.name;
           if(!string.IsNullOrEmpty(data.image))
               recipe.rImage= data.image;
           if (!string.IsNullOrEmpty(data.rSteps))
               recipe.rSteps = data.rSteps;

           // update items
           var items = data.items;
           //List<pantryItemsrecipes> all = new List<pantryItemsrecipes>();

           // 1 => [1, 2]
           // params => [3,4]
           List<pantryItemsrecipes> oldItems = await _recipesContext.pantryItemsrecipes.Where(p => p.reciperid == id).ToListAsync();
           if(oldItems.Count > 0)
               _recipesContext.RemoveRange(oldItems);

           List<pantryItemsrecipes> newItems = new List<pantryItemsrecipes>();
            if(items!=null)
            {
                foreach (var item in items)
                {
                    pantryItems checkItem = _recipesContext.pantryItems.Where(p => p.pid == item).FirstOrDefault();
                    if (checkItem != null)
                    {
                        newItems.Add(new pantryItemsrecipes(item, id));
                    }
                }

                _recipesContext.AddRange(newItems);
            }
           
           _recipesContext.SaveChanges();
           return Ok(recipe);

       }

     

        [HttpPost]
        [Route("/addItems")]
        public async Task<ActionResult<recipes>> AddItemsToRecipe(int idRecipe, List<int> items)
        {
            recipes recipe = _recipesContext.recipe.Find(idRecipe);
            if (recipe == null) { return NotFound(); }

            List<pantryItemsrecipes> all = new List<pantryItemsrecipes>();


            foreach (var item in items)
            {
                pantryItems checkedPantry =  await _recipesContext.pantryItems.Where(p => p.pid == item).FirstOrDefaultAsync();

                if (checkedPantry != null)

                {
                    all.Add(new pantryItemsrecipes(item, idRecipe));
                    
                }
            }
            
            _recipesContext.AddRange(all);

            _recipesContext.SaveChangesAsync();
            return Ok(recipe);
        }
        [HttpPost]
        [Route("/addSteps")]
        public async Task<ActionResult<recipes>> AddStepsToRecipe(int idRecipe, List<int> recipesteps)
        {
            recipes recipe = _recipesContext.recipe.Find(idRecipe);
            if (recipe == null) { return NotFound(); }

            List<stepsrecipes> all = new List<stepsrecipes>();


            foreach (var step in recipesteps)
            {
                var checkedstep = await _recipesContext.steps.Where(p => p.sid == step).FirstOrDefaultAsync();

                if (checkedstep != null)

                {
                    all.Add(new stepsrecipes(step, idRecipe));

                }
            }

            _recipesContext.AddRange(all);

            _recipesContext.SaveChangesAsync();
            return Ok(recipe);
        }
        [HttpDelete]
        [Route("{rid}")]
        public async Task<IActionResult> deleteItem([FromRoute] int rid)
        {
            var foundItem = await _recipesContext.recipe.FirstOrDefaultAsync(x => x.rid == rid);
            if (foundItem != null)
            {
                _recipesContext.Remove(foundItem);
                _recipesContext.SaveChanges();
                return Ok(foundItem);
            }
            return NotFound("Failed");


        }   
    }
}
