using System;
using System.Collections;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Show
    {
        public Show()
        {
            Tickets = new List<Ticket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Date { get; set; }
        
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}