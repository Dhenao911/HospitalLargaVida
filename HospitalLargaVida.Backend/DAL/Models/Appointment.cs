using HospitalLargaVida.Backend.DAL.Enums;

namespace HospitalLargaVida.Backend.DAL.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; } // Identificador unico de la cita autoincrementable

        public string PatientId { get; set; }// Cedula del paciente

        public string DoctorId { get; set; }// Cedula del doctor

        public DateTime AppointmentDate { get; set; }// Fecha y hora de la cita

        public AppointmentStatus Status { get; set; }// Estado de la cita

        public Patient Patient { get; set; }// Referencia al paciente

        public Doctor Doctor { get; set; }// Referencia al doctor


    }
}