using HospitalLargaVida.Backend.DAL.Data;
using HospitalLargaVida.Backend.DAL.Models;
using HospitalLargaVida.Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalLargaVida.Backend.Repositories.Implementaciones
{
    public class AgendaRepository : IAgendaRepository
    {
        private readonly ApplicationDbContext _context;

        public AgendaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAgendaAsync(Agenda agenda)
        {
            _context.Agendas.Add(agenda);
            return await SaveChanges();
        }

        public async Task<bool> DeleteAgendaAsync(int id)
        {
            var agenda = await GetAgendaByIdAsync(id);
            if (agenda == null)
            {
                return false;
            }

            _context.Agendas.Remove(agenda);
            return await SaveChanges();
        }

        public async Task<Agenda> GetAgendaByIdAsync(int id)
        {
            return await _context
                  .Agendas
                  .AsNoTracking()
                  .Include(a => a.Doctor)
                  .FirstOrDefaultAsync(a => a.IdAgenda == id);
        }

        public async Task<bool> GetAgendasByDoctorIdAsync(string doctorId, int hour, int minute)
        {
            return await _context.Agendas
                .AnyAsync(a => a.DoctorId == doctorId
                && a.AppointmentDate.Hour == hour
                && a.AppointmentDate.Minute == minute);
        }

        public async Task<ICollection<Agenda>> GetAllAgendasAsync()
        {
            return _context
                 .Agendas
                 .AsNoTracking()
                 .Include(a => a.Doctor)
                 .ToList();
        }

        public async Task<bool> UpdateAgendaAsync(Agenda agenda)
        {
            _context.Agendas.Update(agenda);
            return await SaveChanges();
        }

        private async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}