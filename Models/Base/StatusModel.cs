using System.ComponentModel.DataAnnotations;

namespace Models.Base
{
    public class StatusModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}