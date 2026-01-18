using HospitalLargaVida.Backend.DAL.Models;

namespace HospitalLargaVida.Backend.Repositories.Interfaces
{
    public interface IAgendaRepository
    {
        Task<bool> CreateAgendaAsync(Agenda agenda);
        Task<bool> DeleteAgendaAsync(int id);
        Task<Agenda> GetAgendaByIdAsync(int id);
        Task<ICollection<Agenda>> GetAllAgendasAsync();

        Task<bool> GetAgendasByDoctorIdAsync(string doctorId,int hour,int minute);
        Task<bool> UpdateAgendaAsync(Agenda agenda);
    }
}
