namespace HospitalLargaVida.Backend.DAL.Dtos.AppointmentDto
{
    public class CreateAppointmentDto
    {
        public string PatientId { get; set; }// Cedula del paciente

        public string DoctorId { get; set; }// Cedula del doctor

        public DateTime AppointmentDate { get; set; }// Fecha y hora de la cita



    }
}
