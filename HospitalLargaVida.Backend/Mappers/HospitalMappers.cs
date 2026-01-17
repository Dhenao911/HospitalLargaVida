using AutoMapper;
using HospitalLargaVida.Backend.DAL.Dtos.AppointmentDto;
using HospitalLargaVida.Backend.DAL.Dtos.DoctorDto;
using HospitalLargaVida.Backend.DAL.Dtos.PatientDto;
using HospitalLargaVida.Backend.DAL.Models;

namespace HospitalLargaVida.Backend.Mappers
{
    public class HospitalMappers : Profile
    {
        public HospitalMappers()
        {
            // Patient Mappings
            CreateMap<Patient, PatientDetailDto>().ReverseMap();
            CreateMap<Patient,CreatePatientDto>().ReverseMap();
            CreateMap<Patient,UpdatePatientDto>().ReverseMap();

            //Doctor Mappings

            CreateMap<Doctor, DoctorDetailsDto>().ReverseMap();
            CreateMap<Doctor, CreateDoctorDto>().ReverseMap();
            CreateMap<Doctor, UpdateDoctorDto>().ReverseMap();

            //appointment Mappings

            CreateMap<Appointment, AppointmentDetailDto>()
                .ForMember(dest => dest.NamePatient, opt => opt.MapFrom(src => src.Patient.NamePatient))
                .ForMember(dest => dest.NameDoctor, opt => opt.MapFrom(src => src.Doctor.NameDoctor));



        }
    }
}