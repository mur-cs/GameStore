using System.ComponentModel.DataAnnotations;

namespace GameStore.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        public string UserName { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
        public User()
        {

        }
    }
}
