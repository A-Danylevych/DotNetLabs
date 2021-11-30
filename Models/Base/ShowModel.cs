using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.Base
{
    public class ShowModel
    {
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public ICollection<int> GenresIds { get; set; }
        public DateTimeOffset Date { get; set; }
        
        public override string ToString()
        {
            return "Name: " + Name + "\n" + "AuthorId: " + AuthorId + "\n" + "GenresIds: " 
                   + GetString(GenresIds) + "\n" + "Date: " + Date;
        }

        private static string GetString(IEnumerable<int> ids)
        {
            return ids.Aggregate("", (current, id) => current + (id + ", "));
        }
    }
}