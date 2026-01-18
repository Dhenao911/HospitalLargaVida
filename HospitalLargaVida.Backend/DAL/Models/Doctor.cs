namespace HospitalLargaVida.Backend.DAL.Models
{
    public class Doctor
    {
        public string DoctorId { get; set; } // Cedula del doctor

        public string NameDoctor { get; set; }// Nombre del doctor

        public string Specialty { get; set; }// Especialidad del doctor

        public ICollection<Agenda> Agendas { get; set; }
    }
}