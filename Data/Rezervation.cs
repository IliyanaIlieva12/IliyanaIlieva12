namespace tattoo.Data
{
    public class Rezervation
    {
      public int Id { get; set; }
        public string CostumerId { get; set; }
        public Customer Customers { get; set; }
        public string TattoId { get; set; }
        public Tattoo Tattoos { get; set; }
        public  DateTime Time { get; set; }
        public string EmployerId { get; set; }  
        public Employer Employers { get; set; }
    }
}
