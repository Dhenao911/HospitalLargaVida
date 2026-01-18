using System.ComponentModel.DataAnnotations;

namespace HospitalLargaVida.Backend.DAL.Dtos.PatientDto
{
    public class UpdatePatientDto

    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime DateOfBirth { get; set; }//Fecha Nacimiento

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(0.5, 250, ErrorMessage = "el campo {0} no permite numeros negativos")]
        public double Height { get; set; }// Altura del paciente

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Range(1, 500, ErrorMessage = "el campo {0} no permite numeros negativos")]
        public double Weight { get; set; }// Peso del paciente

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(100, ErrorMessage = "El campo {0} tiene un maximo de {1} caracteres")]
        [EmailAddress(ErrorMessage = "El campo {0} no tiene el formato valido")]
        public string Email { get; set; }// Correo electronico del paciente

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(100, ErrorMessage = "El campo {0} tiene un maximo de {1} caracteres")]
        public string PhoneNumber { get; set; }// Numero de telefono del paciente
    }
}