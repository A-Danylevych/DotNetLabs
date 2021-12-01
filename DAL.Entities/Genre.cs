using System.Collections;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Show> Shows { get; set; }
    }
}