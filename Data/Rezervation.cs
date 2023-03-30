namespace tattoo.Data
{
    public class Rezervation
    {
      public int Id { get; set; }
        public string CustomerId { get; set; }
        public Customer Customers { get; set; }
        public int TattooId { get; set; }
        public Tattoo Tattoos { get; set; }
        public int EmployerId { get; set; }  
        public Employer Employers { get; set; }
        public DateTime Time { get; set; }
    }
}
