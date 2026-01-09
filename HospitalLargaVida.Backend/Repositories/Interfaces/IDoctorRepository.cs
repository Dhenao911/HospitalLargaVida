using HospitalLargaVida.Backend.DAL.Models;

namespace HospitalLargaVida.Backend.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<bool> CreateDoctorAsync(Doctor doctor);

        Task<bool> DeleteDoctorAsync(string doctorId);

        Task<ICollection<Doctor>> GetAllDoctorsAsync();

        Task<Doctor> GetDoctorByIdAsync(string doctorId);

        Task<bool> UpdateDoctorAsync(Doctor doctor);
    }
}