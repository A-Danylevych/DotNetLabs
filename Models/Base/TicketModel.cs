using System.ComponentModel.DataAnnotations;

namespace Models.Base
{
    public class TicketModel
    {
        public int Id { get; set; }
        [Required]
        public int Seat { get; set; }
        [Required]
        public int Row { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int ShowId { get; set; }
        public override string ToString()
        {
            return "Seat: " + Seat + "\n" + "Row: " + Row + "\n" + "Price: " + Price + "\n" + "StatusId: " + StatusId +
                   "\n" + "ShowId: " + ShowId;
        }
    }
}