using HospitalLargaVida.Backend.DAL.Data;
using HospitalLargaVida.Backend.DAL.Models;
using HospitalLargaVida.Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalLargaVida.Backend.Repositories.Implementaciones
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext Context)
        {
            _context = Context;
        }

        public async Task<bool> CreatePatientAsync(Patient patient)
        {
            await _context
                .Patients
                .AddAsync(patient);

            return await SaveChanges();
        }

        public async Task<bool> DeletePatientAsync(string patientId)
        {
            var patient = await GetPatientByIdAsync(patientId);

            if (patient == null)
            {
                return false;
            }

            _context.Patients.Remove(patient);

            return await SaveChanges();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context
                .Patients
                .AnyAsync(p => p.Email == email);
        }

        public async Task<ICollection<Patient>> GetAllPatient()
        {
            return await _context
                .Patients
                .ToListAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(string patientId)
        {
            return await _context
                .Patients
                .Include(p => p.Appointments)
                .FirstOrDefaultAsync(p => p.PatientId == patientId);
        }

        public async Task<bool> UpdatePatientAsync(Patient patient)
        {
            _context
                .Patients
                .Update(patient);
            return await SaveChanges();
        }

        private async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}