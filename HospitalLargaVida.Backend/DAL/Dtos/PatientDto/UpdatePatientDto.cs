namespace HospitalLargaVida.Backend.DAL.Dtos.PatientDto
{
    public class UpdatePatientDto
    {
        public int Age { get; set; }// Edad del paciente

        public double Height { get; set; }// Altura del paciente

        public double Weight { get; set; }// Peso del paciente

        public string Email { get; set; }// Correo electronico del paciente

        public string PhoneNumber { get; set; }// Numero de telefono del paciente
    }
}