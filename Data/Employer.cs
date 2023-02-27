namespace tattoo.Data
{
    public class Employer
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Phone { get; set; }
        public int DocumentNum { get; set; }
        public ICollection<Rezervation> Rezervations { get; set; }
    }
}
