using AutoMapper;
using HospitalLargaVida.Backend.DAL.Dtos.AppointmentDto;
using HospitalLargaVida.Backend.DAL.Enums;
using HospitalLargaVida.Backend.DAL.Models;
using HospitalLargaVida.Backend.Repositories.Interfaces;
using HospitalLargaVida.Backend.Services.Interfaces;

namespace HospitalLargaVida.Backend.Services.Implementaciones
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IPatientRepository patientRepository, IDoctorRepository doctorRepository,
            IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<AppointmentDetailDto> CreateAppointmentAsync(CreateAppointmentDto appointmentDto)
        {
            var patient = await _patientRepository.GetPatientByIdAsync(appointmentDto.PatientId);
            if (patient == null)
            {
                throw new InvalidOperationException($"El paciente con ID {appointmentDto.PatientId} no existe.");
            }

            var doctor = await _doctorRepository.GetDoctorByIdAsync(appointmentDto.DoctorId);

            if (doctor == null)
            {
                throw new InvalidOperationException($"El doctor con ID {appointmentDto.DoctorId} no existe.");
            }

            if (appointmentDto.AppointmentDate < DateTime.Now)
            {
                throw new InvalidOperationException("La fecha de la cita no puede ser en el pasado.");
            }

            var doctorAppointments = await _appointmentRepository.GetDoctorAppointmentByIdAsunc(appointmentDto.DoctorId);

            foreach (var a in doctorAppointments)
            {
                double diferencia = Math.Abs((a.AppointmentDate - appointmentDto.AppointmentDate).TotalMinutes);

                if (diferencia < 30)
                {
                    throw new InvalidOperationException("El doctor ya tiene una cita programada en ese horario.");
                }
            }

            var appointment = new Appointment
            {
                PatientId = appointmentDto.PatientId,
                DoctorId = appointmentDto.DoctorId,
                AppointmentDate = appointmentDto.AppointmentDate,
                Status = AppointmentStatus.Program
            };

            var createdAppointment = await _appointmentRepository.CreateAppointmentAsync(appointment);
            if (!createdAppointment)
            {
                throw new InvalidOperationException("No se pudo crear la cita.");
            }

            return _mapper.Map<AppointmentDetailDto>(appointment);
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                throw new InvalidOperationException($"La cita con ID {id} no existe.");
            }

            var deleteAppointment = await _appointmentRepository.DeleteAppointmentAsync(id);
            if (!deleteAppointment)
            {
                throw new InvalidOperationException("No se pudo eliminar la cita.");
            }

            return deleteAppointment;
        }

        public async Task<ICollection<AppointmentDetailDto>> GetAllAppointmentsAsync()
        {
            var appointments = await _appointmentRepository.GetAllAppointmentsAsync();

            return _mapper.Map<ICollection<AppointmentDetailDto>>(appointments);
        }

        public async Task<AppointmentDetailDto> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);

            if (appointment == null)
            {
                throw new InvalidOperationException($"La cita con ID {id} no existe.");
            }

            return _mapper.Map<AppointmentDetailDto>(appointment);
        }
    }
}