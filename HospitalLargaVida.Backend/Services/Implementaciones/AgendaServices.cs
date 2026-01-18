using AutoMapper;
using HospitalLargaVida.Backend.DAL.Dtos.AgendaDto;
using HospitalLargaVida.Backend.DAL.Enums;
using HospitalLargaVida.Backend.DAL.Models;
using HospitalLargaVida.Backend.Repositories.Interfaces;
using HospitalLargaVida.Backend.Services.Interfaces;

namespace HospitalLargaVida.Backend.Services.Implementaciones
{
    public class AgendaServices : IAgendaServices
    {
        private readonly IAgendaRepository _agendaRepository;
        private readonly IDoctorRepository _doctorRepository;

        private readonly IMapper _mapper;

        public AgendaServices(IAgendaRepository agendaRepository, IDoctorRepository doctorRepository, IMapper mapper)
        {
            _agendaRepository = agendaRepository;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<AgendaDetailsDto> CreateAgendaAsync(CreateAgendaDto agenda)
        {
            var existDoctor = await _doctorRepository.GetDoctorByIdAsync(agenda.DoctorId);
            if (existDoctor == null)
            {
                throw new InvalidOperationException($"El doctor con ID {agenda.DoctorId} no existe.");
            }

            //evitar que un doctor tenga mas de una agenda en la misma fecha y hora
            var appointmentExist = await _agendaRepository
                .GetAgendasByDoctorIdAsync(agenda.DoctorId, agenda.AppointmentDate.Hour, agenda.AppointmentDate.Minute);

            if (appointmentExist)
            {
                throw new InvalidOperationException($"El doctor con ID {agenda.DoctorId} ya tiene una agenda en esa fecha y hora.");
            }

            // mapear de DTO a entidad para crear una nueva agenda
            var createdAgenda = _mapper.Map<Agenda>(agenda);
            createdAgenda.statusAgenda = StatusAgenda.Disponible;

            // guardar en la base de datos
            var agendaNew = await _agendaRepository.CreateAgendaAsync(createdAgenda);

            if (!agendaNew)
            {
                throw new InvalidOperationException("No se pudo crear la agenda.");
            }

            return _mapper.Map<AgendaDetailsDto>(createdAgenda);
        }

        public async Task<bool> DeleteAgendaAsync(int id)
        {
            var existingAgenda =await  _agendaRepository.GetAgendaByIdAsync(id);
            if (existingAgenda == null)
            {
                throw new InvalidOperationException($"La agenda con ID {id} no existe.");
            }

            var agendaDeleted = await _agendaRepository.DeleteAgendaAsync(id);

            if (!agendaDeleted)
            {
                throw new InvalidOperationException("No se pudo eliminar la agenda.");
            }

            return agendaDeleted;
        }

        public async Task<AgendaDetailsDto> GetAgendaByIdAsync(int id)
        {
            var agenda = await _agendaRepository.GetAgendaByIdAsync(id);
            if (agenda == null)
            {
                throw new InvalidOperationException($"La agenda con ID {id} no existe.");
            }

            return _mapper.Map<AgendaDetailsDto>(agenda);
        }

        public async Task<ICollection<AgendaDetailsDto>> GetAllAgendasAsync()
        {
            var agendas = await _agendaRepository.GetAllAgendasAsync();
            if (!agendas.Any())
            {
                throw new InvalidOperationException("No existen agendas registradas.");
            }

            return _mapper.Map<ICollection<AgendaDetailsDto>>(agendas);
        }
    }
}