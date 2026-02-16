using HospitalLargaVida.Backend.DAL.Models;

namespace HospitalLargaVida.Backend.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task CreateDoctorAsync(Doctor doctor);

        Task DeleteDoctorAsync(Doctor doctor);

        Task<ICollection<Doctor>> GetAllDoctorsAsync();

        Task<Doctor> GetDoctorByIdAsync(string doctorId);

        Task UpdateDoctorAsync(Doctor doctor);

    }
}