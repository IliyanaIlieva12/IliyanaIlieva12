using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace tattoo.Data
{
    public class Tattoo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategorieId { get; set; }
        public Categorie Categories { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public DateTime RegisterON { get; set; }
        public ICollection<Rezervation> Rezervations { get; set; }
    }
}
