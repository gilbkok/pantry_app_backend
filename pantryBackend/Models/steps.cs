using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pantryBackend.Models
{
    public class steps
    {
        [Key]
        public int? sid { get; set; }
        public string sname { get; set; }
        public string sdescription { get; set; }

        public virtual ICollection<stepsrecipes>? recipe { get; set; }


    }
}
