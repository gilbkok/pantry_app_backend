using System.ComponentModel.DataAnnotations;

namespace pantryBackend.Models
{
    public class pantryItemsrecipes
    {
        
        public int pantryItempid { get; set; }

       public pantryItems pantryItem { get; set; }
       
        public int reciperid { get; set; }  
        public recipes recipe { get; set; }

        public pantryItemsrecipes(int pantryItempid, int reciperid)
        {
            this.pantryItempid = pantryItempid;
            this.reciperid = reciperid;
        }
    }
}
