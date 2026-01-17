using HospitalLargaVida.Backend.DAL.Dtos.PatientDto;

namespace HospitalLargaVida.Backend.Services.Interfaces
{
    public interface IPatientService
    {
        Task<PatientDetailDto> CreatePatientAsync(CreatePatientDto patientDto);

        Task<bool> DeletePatientAsync(string patientId);

        Task<ICollection<PatientDetailDto>> GetAllPatient();

        Task<PatientDetailDto> GetPatientByIdAsync(string patientId);

        Task<PatientDetailDto> UpdatePatientAsync(UpdatePatientDto patientDto,string PatientId);

    }

    //604 
}
