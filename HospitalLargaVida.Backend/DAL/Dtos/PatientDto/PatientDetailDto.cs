using HospitalLargaVida.Backend.DAL.Models;

namespace HospitalLargaVida.Backend.DAL.Dtos.PatientDto
{
    public class PatientDetailDto
    {
        public string PatientId { get; set; } //Cedula del paciente

        public string NamePatient { get; set; }// Nombre del paciente

        public int Age { get; set; }// Edad del paciente

        public double Height { get; set; }// Altura del paciente

        public double Weight { get; set; }// Peso del paciente

        public string Email { get; set; }// Correo electronico del paciente

        public string PhoneNumber { get; set; }// Numero de telefono del paciente

        public ICollection<Appointment> Appointments { get; set; }// Lista de citas del pacientem


    }
}
}