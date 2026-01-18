namespace HospitalLargaVida.Backend.DAL.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; } // Identificador unico de la cita autoincrementable

        public int AgendaId { get; set; }
        public string PatientId { get; set; }// Cedula del paciente

        public DateTime AppointmentDate { get; set; }
        public Patient Patient { get; set; }// Referencia al paciente

        public Agenda Agenda { get; set; }
    }
}