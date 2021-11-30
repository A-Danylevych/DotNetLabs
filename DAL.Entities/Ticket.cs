namespace DAL.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public int Seat { get; set; }
        public int Row { get; set; }
        public decimal Price { get; set; }
        
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        
        public int ShowId { get; set; }
        public virtual Show Show { get; set; }
        
    }
}