using pantryBackend.Models;

namespace pantryBackend.DTO
{
    public class GetRecipeWithItems
    {
        public int id;
        public string name;
        public string image;
        public string rSteps;
        public List<pantryItems> items;
        public List<steps> Recipesteps;

        
    }
}
