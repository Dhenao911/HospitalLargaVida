namespace HospitalLargaVida.Backend.DAL.Dtos.DoctorDto
{
    public class CreateDoctorDto
    {
        public string DoctorId { get; set; } // Cedula del doctor

        public string NameDoctor { get; set; }// Nombre del doctor

        public string Specialty { get; set; }// Especialidad del doctor
    }
}
