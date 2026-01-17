using AutoMapper;
using HospitalLargaVida.Backend.DAL.Dtos.PatientDto;
using HospitalLargaVida.Backend.DAL.Models;
using HospitalLargaVida.Backend.Repositories.Interfaces;
using HospitalLargaVida.Backend.Services.Interfaces;

namespace HospitalLargaVida.Backend.Services.Implementaciones
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<PatientDetailDto> CreatePatientAsync(CreatePatientDto patientDto)
        {
            var patientExist = await _patientRepository.GetPatientByIdAsync(patientDto.PatientId);
            if (patientExist != null)
            {
                throw new InvalidOperationException($"El paciente con ID {patientDto.PatientId} ya existe.");
            }

            // mapear de DTO a entidad para crearun nuevo objeto paciente 

            var patientNew = _mapper.Map<Patient>(patientDto);

            // guardar en la base de datos
            var PatientCreated = await _patientRepository.CreatePatientAsync(patientNew);

            if (!PatientCreated)
            {
                throw new InvalidOperationException("No se pudo crear el paciente.");
            }

            // mapear de entidad a DTO de detalle
            return _mapper.Map<PatientDetailDto>(patientNew);
        }

        public async Task<bool> DeletePatientAsync(string patientId)
        {
            var patientExist = await _patientRepository.GetPatientByIdAsync(patientId);

            if (patientExist == null)
            {
                throw new InvalidOperationException($"El paciente con ID {patientId} no existe.");
            }

            var patientDeleted = await _patientRepository.DeletePatientAsync(patientId);

            if (!patientDeleted)
            {
                throw new InvalidOperationException("No se pudo eliminar el paciente.");
            }

            return patientDeleted;

        }

        public async Task<ICollection<PatientDetailDto>> GetAllPatient()
        {
            var patients = await _patientRepository.GetAllPatient();
            if (!patients.Any())
            {
                throw new InvalidOperationException($"No existen pacientes en la base de datos");
            }
            return _mapper.Map<ICollection<PatientDetailDto>>(patients);
        }

        public async Task<PatientDetailDto> GetPatientByIdAsync(string patientId)
        {
            var patient = await _patientRepository.GetPatientByIdAsync(patientId);
            if (patient == null)
            {
                throw new InvalidOperationException($"El paciente con ID {patientId} no existe.");
            }

            return _mapper.Map<PatientDetailDto>(patient);
        }

        public async Task<PatientDetailDto> UpdatePatientAsync(UpdatePatientDto patientDto, string patientId)
        {
            var patienExist = await _patientRepository.GetPatientByIdAsync(patientId);
            if (patienExist == null)
            {
                throw new InvalidOperationException($"El paciente con ID {patientId} no existe.");
            }

            //normalizar email

            patientDto.Email = patientDto.Email.ToLower().Trim();
            if (!string.Equals
                (patienExist.Email
                , patientDto.Email
                , StringComparison.OrdinalIgnoreCase))
            {
                var emailExist = await _patientRepository.ExistsByEmailAsync(patientDto.Email);
                if (emailExist)
                {
                    throw new InvalidOperationException($"El email {patientDto.Email} ya existe en uso por otro paciente.");
                }
            }

            //mapear sobre la entidad existente

            _mapper.Map(patientDto, patienExist);

            //guardar en la base de datos

            var patientUpdated = await _patientRepository.UpdatePatientAsync(patienExist);
            if (!patientUpdated)
            {
                throw new InvalidOperationException("No se pudo actualizar el paciente.");
            }

            //mapear a DTO de detalle

            return _mapper.Map<PatientDetailDto>(patienExist);
        }
    }
}