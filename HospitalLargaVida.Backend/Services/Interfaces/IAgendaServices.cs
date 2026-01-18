using HospitalLargaVida.Backend.DAL.Dtos.AgendaDto;

namespace HospitalLargaVida.Backend.Services.Interfaces
{
    public interface IAgendaServices
    {
        Task<AgendaDetailsDto> CreateAgendaAsync(CreateAgendaDto agenda);

        Task<bool> DeleteAgendaAsync(int id);

        Task<AgendaDetailsDto> GetAgendaByIdAsync(int id);

        Task<ICollection<AgendaDetailsDto>> GetAllAgendasAsync();
    }
}