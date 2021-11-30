namespace Models.Base
{
    public class TicketModel
    {
        public int Seat { get; set; }
        public int Row { get; set; }
        public decimal Price { get; set; }
        public int StatusId { get; set; }
        public int ShowId { get; set; }
        public override string ToString()
        {
            return "Seat: " + Seat + "\n" + "Row: " + Row + "\n" + "Price: " + Price + "\n" + "StatusId: " + StatusId +
                   "\n" + "ShowId: " + ShowId;
        }
    }
}