namespace Models.Base
{
    public class AuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}