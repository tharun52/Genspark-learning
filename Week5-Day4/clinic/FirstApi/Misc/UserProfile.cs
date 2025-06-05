using AutoMapper;
using FirstApi.Models;
using FirstApi.Models.DTOs;


namespace FirstApi.Misc
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<DoctorAddRequestDto, User>()
            .ForMember(dest => dest.Username, act => act.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<User, DoctorAddRequestDto>()
            .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Username));

            CreateMap<PatientAddRequestDto, User>()
            .ForMember(dest => dest.Username, act => act.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<User, PatientAddRequestDto>()
            .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Username));
        }
    }
}