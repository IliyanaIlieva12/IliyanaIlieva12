using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace tattoo.Data
{
    public class Employer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public int DocumentNum { get; set; }
        public ICollection<Rezervation> Rezervations { get; set; }
    }
}