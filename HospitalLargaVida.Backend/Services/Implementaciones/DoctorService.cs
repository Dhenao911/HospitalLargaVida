using AutoMapper;
using HospitalLargaVida.Backend.DAL.Dtos.DoctorDto;
using HospitalLargaVida.Backend.DAL.Models;
using HospitalLargaVida.Backend.Exceptions;
using HospitalLargaVida.Backend.Repositories.Interfaces;
using HospitalLargaVida.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HospitalLargaVida.Backend.Services.Implementaciones
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public DoctorService(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<DoctorDetailsDto> CreateDoctorAsync(CreateDoctorDto doctorDto)
        {
            var doctorExist = await _doctorRepository.GetDoctorByIdAsync(doctorDto.DoctorId);

            if (doctorExist != null)
            {
                throw new ConflictException($"El doctor con ID {doctorDto.DoctorId} ya existe.");
            }

            // mapear de DTO a entidad para crear el doctor

            var doctorNew = _mapper.Map<Doctor>(doctorDto);

            // guardar en la base de datos
            await _doctorRepository.CreateDoctorAsync(doctorNew);

            return _mapper.Map<DoctorDetailsDto>(doctorNew);
        }

        public async Task DeleteDoctorAsync(string doctorId)
        {
            var doctorExist = await _doctorRepository.GetDoctorByIdAsync(doctorId);

            if (doctorExist == null)
            {
                throw new NotFoundException($"El doctor con ID {doctorId} no existe.");
            }

            await _doctorRepository.DeleteDoctorAsync(doctorExist);
        }

        public async Task<ICollection<DoctorDetailsDto>> GetAllDoctorsAsync()
        {
            var doctors = await _doctorRepository.GetAllDoctorsAsync();

            return _mapper.Map<ICollection<DoctorDetailsDto>>(doctors);
        }

        public async Task<DoctorDetailsDto> GetDoctorByIdAsync(string doctorId)
        {
            var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorId);

            if (doctor == null)
            {
                throw new NotFoundException($"El doctor con ID {doctorId} no existe.");
            }

            return _mapper.Map<DoctorDetailsDto>(doctor);
        }

        public async Task<DoctorDetailsDto> UpdateDoctorAsync(UpdateDoctorDto doctorDto, string doctorId)
        {
            var doctorExist = await _doctorRepository.GetDoctorByIdAsync(doctorId);
            if (doctorExist == null)
            {
                throw new NotFoundException($"El doctor con ID {doctorId} no existe.");
            }

            //mappear sobre la entidad existente para actualizar el doctor

            _mapper.Map(doctorDto, doctorExist);

            //guardar en la base de datos
            await _doctorRepository.UpdateDoctorAsync(doctorExist);

            return _mapper.Map<DoctorDetailsDto>(doctorExist);
        }
    }
}