using HospitalLargaVida.Backend.DAL.Data;
using HospitalLargaVida.Backend.DAL.Models;
using HospitalLargaVida.Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalLargaVida.Backend.Repositories.Implementaciones
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateDoctorAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await SaveChanges();
        }

        public async Task DeleteDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Remove(doctor);

            await SaveChanges();
        }

        public async Task<ICollection<Doctor>> GetAllDoctorsAsync()
        {
            return await _context
                 .Doctors
                .Include(ag => ag.Agendas)
                 .ToListAsync();
        }

        public async Task<Doctor> GetDoctorByIdAsync(string doctorId)
        {
            return await _context
                .Doctors
                .Include(ag => ag.Agendas)
                .FirstOrDefaultAsync(d => d.DoctorId == doctorId);
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await SaveChanges();
        }

        private async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}