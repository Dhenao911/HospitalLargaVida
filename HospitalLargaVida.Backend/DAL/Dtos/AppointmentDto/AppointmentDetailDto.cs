namespace HospitalLargaVida.Backend.DAL.Dtos.AppointmentDto
{
    public class AppointmentDetailDto
    {
        public int AppointmentId { get; set; }

        public string NamePatient { get; set; }

        public string NameDoctor { get; set; }

        public DateTime AppointmentDate { get; set; }


    }
}
