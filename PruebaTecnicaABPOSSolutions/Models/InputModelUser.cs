using static Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal.ExternalLoginModel;

namespace PruebaTecnicaABPOSSolutions.Models
{
    public class InputModelUser: InputModel
    {
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
    }
}
