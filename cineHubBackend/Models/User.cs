using Microsoft.AspNetCore.Identity;

namespace cineHubBackend.Models
{
    public class User : IdentityUser
    {
      
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}