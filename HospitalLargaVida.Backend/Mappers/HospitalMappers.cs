using AutoMapper;
using HospitalLargaVida.Backend.DAL.Dtos.AppointmentDto;
using HospitalLargaVida.Backend.DAL.Dtos.DoctorDto;
using HospitalLargaVida.Backend.DAL.Dtos.PatientDto;

using HospitalLargaVida.Backend.DAL.Dtos.PatientDto;

using HospitalLargaVida.Backend.DAL.Dtos.AgendaDto;
using HospitalLargaVida.Backend.DAL.Models;

namespace HospitalLargaVida.Backend.Mappers
{
    public class HospitalMappers : Profile
    {
        public HospitalMappers()
        {
            // Patient Mappings
            CreateMap<Patient, PatientDetailDto>().ReverseMap();
            CreateMap<Patient, CreatePatientDto>().ReverseMap();
            CreateMap<Patient, UpdatePatientDto>().ReverseMap();

            //Agenda Mappings

            CreateMap<Agenda, AgendaDetailsDto>().
                ForMember(dest => dest.NameDoctor, opt => opt.MapFrom(src => src.Doctor.NameDoctor))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Doctor.Specialty));

            CreateMap<Agenda, CreateAgendaDto>().ReverseMap();

            //Doctor Mappings

            CreateMap<Doctor, DoctorDetailsDto>().ReverseMap();
            CreateMap<Doctor, CreateDoctorDto>().ReverseMap();
            CreateMap<Doctor, UpdateDoctorDto>().ReverseMap();

            //appointment Mappings

            CreateMap<Appointment, AppointmentDetailDto>()
            .ForMember(dest => dest.NamePatient, opt => opt.MapFrom(src => src.Patient.NamePatient))
            .ForMember(dest => dest.NameDoctor, opt => opt.MapFrom(src => src.Agenda.Doctor.NameDoctor))
            .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.Agenda.AppointmentDate))
            .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Agenda.Doctor.Specialty));
        }
    }
}