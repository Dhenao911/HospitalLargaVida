using Microsoft.AspNetCore.Identity;

namespace HospitalLargaVida.Backend.DAL.Models
{
    public class User : IdentityUser

    {
        // Propiedades adicionales para la entidad User si es necesario
        public string document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }
    }
}