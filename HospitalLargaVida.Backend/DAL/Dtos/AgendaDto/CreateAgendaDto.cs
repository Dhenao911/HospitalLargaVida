namespace HospitalLargaVida.Backend.DAL.Dtos.AgendaDto
{
    public class CreateAgendaDto
    {
        public string DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}