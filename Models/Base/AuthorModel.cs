using System.ComponentModel.DataAnnotations;

namespace Models.Base
{
    public class AuthorModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}