using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace pantryBackend.Models
{
    public class stepsrecipes

      
    {
        
        public int stepid { get; set; }

        public steps step { get; set; }

        public int reciperid { get; set; }
        public recipes recipe { get; set; }

        public stepsrecipes(int stepid, int reciperid)
        {
            this.stepid = stepid;
            this.reciperid = reciperid;
        }
    }
}