using HospitalLargaVida.Backend.DAL.Enums;

namespace HospitalLargaVida.Backend.DAL.Models
{
    public class Agenda
    {
        public int IdAgenda { get; set; }
        public DateTime AppointmentDate { get; set; }

        public StatusAgenda statusAgenda { get; set; }

        public string DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public Appointment Appointment { get; set; }
    }
}