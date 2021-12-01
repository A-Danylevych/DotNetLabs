using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Models.Base
{
    public class ShowModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int GenreId { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; }
        
        public override string ToString()
        {
            return "Name: " + Name + "\n" + "AuthorId: " + AuthorId + "\n" + "GenresIds: " 
                   + GenreId + "\n" + "Date: " + Date;
        }


    }
}