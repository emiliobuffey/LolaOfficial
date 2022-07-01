using System.ComponentModel.DataAnnotations;

namespace LolaOfficial.Shared{
    public class User{
        public int Id { get; set; }
        //[Required]
        public string? Email { get; set; } = null;
        public string? Password { get; set; } = null;
    }

}