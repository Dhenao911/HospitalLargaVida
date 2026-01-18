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
        private readonly IAgendaRepository _agendaRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IPatientRepository patientRepository, IAgendaRepository agendaRepository,
            IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _agendaRepository = agendaRepository;
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

            var agenda = await _agendaRepository.GetAgendaByIdAsync(appointmentDto.AgendaId);
            if (agenda == null)
            {
                throw new InvalidOperationException($"La agenda con ID {appointmentDto.AgendaId} no existe.");
            }

            if (agenda.statusAgenda != StatusAgenda.Disponible)
            {
                throw new InvalidOperationException($"La agenda con ID {appointmentDto.AgendaId} no está disponible para citas.");
            }

            var appointment = new Appointment
            {
                PatientId = appointmentDto.PatientId,
                AgendaId = appointmentDto.AgendaId,
                AppointmentDate = agenda.AppointmentDate,
            };

            var createdAppointment = await _appointmentRepository.CreateAppointmentAsync(appointment);
            if (!createdAppointment)
            {
                throw new InvalidOperationException("No se pudo crear la cita.");
            }

            agenda.statusAgenda = StatusAgenda.Ocupado;
            await _agendaRepository.UpdateAgendaAsync(agenda);

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