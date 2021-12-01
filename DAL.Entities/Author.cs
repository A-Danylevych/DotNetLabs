﻿using System.Collections.Generic;

namespace DAL.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Show> Shows { get; set; }
    }
}