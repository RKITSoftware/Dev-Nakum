using AutoMapper;
using SocialMediaAPI.Model.Dtos;
using SocialMediaAPI.Model.POCO;

namespace SocialMediaAPI.BL
{
    public class BLAutoMapperProfile : Profile
    {
        public BLAutoMapperProfile()
        {
            CreateMap<Use01, DtoUse01>()
              .ForMember(dest => dest.E01101, opt => opt.MapFrom(src => src.E01F02))
              .ForMember(dest => dest.E01102, opt => opt.MapFrom(src => src.E01F03))
              .ForMember(dest => dest.E01103, opt => opt.MapFrom(src => src.E01F04))
              .ForMember(dest => dest.E01105, opt => opt.MapFrom(src => src.E01F06))
              .ReverseMap(); // Optional, allows mapping DtoUse01 back to Use01


            CreateMap<Pos01, DtoPos01>()
                .ForMember(dest => dest.S01102, opt => opt.MapFrom(src => src.S01F04))
                .ReverseMap();

            CreateMap<Com01, DtoCom01>()
                .ForMember(dest => dest.M01101, opt => opt.MapFrom(src => src.M01F02))
                .ForMember(dest => dest.M01102, opt => opt.MapFrom(src => src.M01F04))
                .ReverseMap();


            CreateMap<Fol01, DtoFol01>()
                .ForMember(dest => dest.L01101, opt => opt.MapFrom(src => src.L01F03))
               
                .ReverseMap();
        }   
    }
}
