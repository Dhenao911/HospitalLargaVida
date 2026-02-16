using HospitalLargaVida.Backend.DAL.Dtos.DoctorDto;
using HospitalLargaVida.Backend.DAL.Models;

namespace HospitalLargaVida.Backend.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<DoctorDetailsDto> CreateDoctorAsync(CreateDoctorDto doctordTO);

        Task DeleteDoctorAsync(string doctorId);

        Task<ICollection<DoctorDetailsDto>> GetAllDoctorsAsync();

        Task<DoctorDetailsDto> GetDoctorByIdAsync(string doctorId);

        Task<DoctorDetailsDto> UpdateDoctorAsync(UpdateDoctorDto doctordTO,String doctorId);

    }
}