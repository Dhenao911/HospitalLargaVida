using HospitalLargaVida.Backend.DAL.Enums;
using HospitalLargaVida.Backend.DAL.Models;

namespace HospitalLargaVida.Backend.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<bool> CreateAppointmentAsync(Appointment appointment);

        Task<bool> DeleteAppointmentAsync(int id);

        Task<Appointment> GetAppointmentByIdAsync(int id);

        Task<ICollection<Appointment>> GetDoctorAppointmentByIdAsunc(string doctorId);

        Task<ICollection<Appointment>> GetAllAppointmentsAsync();

        Task<bool> UpdateAppointmentAsync(Appointment appointment);
    }
}