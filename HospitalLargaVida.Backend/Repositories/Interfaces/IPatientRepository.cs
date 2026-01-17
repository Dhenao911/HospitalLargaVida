using HospitalLargaVida.Backend.DAL.Models;

namespace HospitalLargaVida.Backend.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<bool> CreatePatientAsync(Patient patient);

        Task<bool> DeletePatientAsync(string patientId);

        Task<ICollection<Patient>> GetAllPatient();

        Task<Patient> GetPatientByIdAsync(string patientId);

        Task<bool> UpdatePatientAsync(Patient patient);

        Task<bool> ExistsByEmailAsync(string email);
    }
}