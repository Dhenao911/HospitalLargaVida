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

        public async Task<bool> CreateDoctorAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            return await SaveChanges();
        }

        public async Task<bool> DeleteDoctorAsync(string doctorId)
        {
            var doctor = await GetDoctorByIdAsync(doctorId);
            if (doctor == null)
            {
                return false;
            }

            _context.Doctors.Remove(doctor);

            return await SaveChanges();
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

        public async Task<bool> UpdateDoctorAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            return await SaveChanges();
        }

        private async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}