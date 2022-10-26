using System.ComponentModel.DataAnnotations;

namespace pantryBackend.Models
{
    public class recipes
    {
        [Key]
        public int? rid { get; set; }
        public string rName { get; set; }
        public string rImage { get; set; }
        public string? rSteps { get; set; }
        
        public virtual ICollection<pantryItemsrecipes>? pantryItem{ get; set; }

        public virtual ICollection<stepsrecipes>? step { get; set; }
    
    }
}
