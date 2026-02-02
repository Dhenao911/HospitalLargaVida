using HospitalLargaVida.Backend.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalLargaVida.Backend.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Agenda> Agendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuraciones adicionales si es necesari0

            modelBuilder.Entity<Patient>(x =>
            {
                // Definicion de la clave primaria y propiedades de la entidad Patient
                x.HasKey(p => p.PatientId);
                x.Property(p => p.PatientId).ValueGeneratedNever().IsRequired().HasMaxLength(20);
                x.Property(p => p.NamePatient).IsRequired().HasMaxLength(100);
                x.Property(p => p.Age).IsRequired();
                x.Property(p => p.Height).IsRequired().HasPrecision(10, 2);
                x.Property(p => p.Weight).IsRequired().HasPrecision(10, 2);
                x.Property(p => p.Email).IsRequired().HasMaxLength(100);
                x.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(15);

                // Definicion de indice unico en el campo Email
                x.HasIndex(p => p.Email).IsUnique();

                // Definicion de restricciones CHECK
                x.ToTable(p =>
                {
                    p.HasCheckConstraint("CK_Patient_Age", "Age>=0");
                    p.HasCheckConstraint("CK_Patient_Height", "Height>0");
                    p.HasCheckConstraint("CK_Patient_Weight", "Weight>0");
                });
            });

            modelBuilder.Entity<Doctor>(x =>
            {
                x.HasKey(d => d.DoctorId);

                x.Property(d => d.DoctorId).ValueGeneratedNever().IsRequired().HasMaxLength(100);
                x.Property(d => d.NameDoctor).IsRequired().HasMaxLength(100);
                x.Property(d => d.Specialty).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<Appointment>(x =>
            {
                x.HasKey(a => a.AppointmentId);
                x.Property(a => a.AppointmentId).ValueGeneratedOnAdd();

                x.HasOne(p => p.Patient)// Una cita pertenece a un paciente
                .WithMany(a => a.Appointments)// Un paciente puede tener muchas citas
                .HasForeignKey(a => a.PatientId)// Llave foranea en la tabla Appointment
                .OnDelete(DeleteBehavior.Restrict);// Restriccion para evitar eliminacion en cascada

                x.HasOne(a => a.Agenda)// Una cita pertenece a una agenda
                .WithOne(ag => ag.Appointment)// Una agenda tiene una cita
                .HasForeignKey<Appointment>(a => a.AgendaId)// Llave foranea en la tabla Appointment
                .OnDelete(DeleteBehavior.Restrict);// Restriccion para evitar eliminacion en cascada
            });

            modelBuilder.Entity<Agenda>(x =>
            {
                x.HasKey(a => a.IdAgenda);
                x.Property(a => a.IdAgenda).ValueGeneratedOnAdd();

                x.HasOne(d => d.Doctor)
                .WithMany(a => a.Agendas)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            }
            );
        }
    }
}