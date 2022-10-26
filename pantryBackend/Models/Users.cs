using System.ComponentModel.DataAnnotations;

namespace pantryBackend.Models
{
    public class Users
    {
        [Key]
        public int uid { get; set; }
        public string password { get; set; }

        public string firstName { get; set; }
    }
}
