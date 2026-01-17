using HospitalLargaVida.Backend.DAL.Data;
using HospitalLargaVida.Backend.DAL.Models;
using HospitalLargaVida.Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalLargaVida.Backend.Repositories.Implementaciones
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAppointmentAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            return await SaveChanges();
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return false;
            }

            _context.Appointments.Remove(appointment);
            return await SaveChanges();
        }

        public async Task<ICollection<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context
                 .Appointments
                 .Include(a => a.Patient)
                 .Include(a => a.Doctor)
                 .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _context
                .Appointments
                 .Include(a => a.Patient)
                 .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
        }

        public async Task<ICollection<Appointment>> GetDoctorAppointmentByIdAsunc(string doctorId)
        {
            return await _context
                .Appointments
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<bool> UpdateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            return await SaveChanges();
        }

        private async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}