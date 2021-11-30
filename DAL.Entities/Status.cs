using System.Collections.Generic;

namespace DAL.Entities
{
    public class Status
    {
        public Status()
        {
            Tickets = new List<Ticket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}