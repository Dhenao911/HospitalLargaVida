using HospitalLargaVida.Backend.DAL.Dtos.AppointmentDto;
using HospitalLargaVida.Backend.DAL.Models;

namespace HospitalLargaVida.Backend.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDetailDto> CreateAppointmentAsync(CreateAppointmentDto appointmentDto);

        Task<AppointmentDetailDto> GetAppointmentByIdAsync(int id);

        Task<ICollection<AppointmentDetailDto>> GetAllAppointmentsAsync();
    }
}