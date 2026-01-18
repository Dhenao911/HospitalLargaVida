using HospitalLargaVida.Backend.DAL.Enums;

namespace HospitalLargaVida.Backend.DAL.Dtos.AgendaDto
{
    public class AgendaDetailsDto
    {
        public int IdAgenda { get; set; }
        public DateTime AppointmentDate { get; set; }
        public StatusAgenda statusAgenda { get; set; }
    }
}