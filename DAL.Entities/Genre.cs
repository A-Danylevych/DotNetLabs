using System.Collections.Generic;

namespace DAL.Entities
{
    public class Genre
    {
        public Genre()
        {
            Shows = new List<Show>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Show> Shows { get; set; }
    }
}