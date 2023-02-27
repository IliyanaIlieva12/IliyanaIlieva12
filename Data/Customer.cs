using Microsoft.AspNetCore.Identity;

namespace tattoo.Data
{
    public class Customer:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisterON { get; set; }
        public ICollection<Rezervation> Rezervations { get; set; }
    }
}
