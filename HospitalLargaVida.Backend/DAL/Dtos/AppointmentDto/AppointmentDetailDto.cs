namespace HospitalLargaVida.Backend.DAL.Dtos.AppointmentDto
{
    public class AppointmentDetailDto
    {
        public int AppointmentId { get; set; }

        public string NamePatient { get; set; }

        public string NameDoctor { get; set; }

        public string Specialty { get; set; }// Especialidad del doctor

        public DateTime AppointmentDate { get; set; }
    }
}