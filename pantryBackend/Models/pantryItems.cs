using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pantryBackend.Models
{
    public class pantryItems
    {
        [Key] 
        public int? pid { get; set; }
        public string iName { get; set; }
        public string image { get; set; }
        public int weight { get; set; }
        public int tCalories { get; set; }
        public ICollection<pantryItemsrecipes>? recipe { get; set; }

    }
}
