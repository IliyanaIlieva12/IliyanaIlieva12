using System.ComponentModel;

namespace tattoo.Data
{
    public class Tattoo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public Categorie Categories { get; set; }
        public ICollection<Rezervation> Rezervations { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public decimal Price { get; set; }
        public DateTime RegisterON { get; set; }
    }
}
