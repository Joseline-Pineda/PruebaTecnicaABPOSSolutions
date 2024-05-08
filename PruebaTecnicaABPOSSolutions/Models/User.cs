using Microsoft.AspNetCore.Identity;

namespace PruebaTecnicaABPOSSolutions.Models
{
    public class User: IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}
