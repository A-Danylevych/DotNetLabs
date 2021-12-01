using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Models.Base
{
    public class GenreModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}