using HospitalLargaVida.Backend.DAL.Enums;

namespace HospitalLargaVida.Backend.DAL.Dtos.AgendaDto
{
    public class AgendaDetailsDto
    {
        public int IdAgenda { get; set; }

        public string NameDoctor { get; set; }

        public string Specialty { get; set; }// Especialidad del doctor
        public DateTime AppointmentDate { get; set; }
        public StatusAgenda statusAgenda { get; set; }
    }
}