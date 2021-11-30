using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Show
    {
        public Show()
        {
            Genres = new List<Genre>();
            Tickets = new List<Ticket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Date { get; set; }
        
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}