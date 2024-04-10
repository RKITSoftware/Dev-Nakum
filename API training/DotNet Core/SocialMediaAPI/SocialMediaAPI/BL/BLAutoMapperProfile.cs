using AutoMapper;
using SocialMediaAPI.Model.Dtos;
using SocialMediaAPI.Model.POCO;

namespace SocialMediaAPI.BL
{
    /// <summary>
    /// Configures mappings between different data transfer objects (DTOs) and their corresponding domain model entities.
    /// </summary>
    public class BLAutoMapperProfile : Profile
    {
        /// <summary>
        /// Constructor that configures AutoMapper mappings.
        /// </summary>
        public BLAutoMapperProfile()
        {
            // Mapping between Use01 entity and DtoUse01 DTO
            CreateMap<Use01, DtoUse01>()
                .ForMember(dest => dest.E01101, opt => opt.MapFrom(src => src.E01F02)) 
                .ForMember(dest => dest.E01102, opt => opt.MapFrom(src => src.E01F03)) 
                .ForMember(dest => dest.E01103, opt => opt.MapFrom(src => src.E01F04)) 
                .ForMember(dest => dest.E01105, opt => opt.MapFrom(src => src.E01F06)) 
                .ReverseMap();                                                         

            // Mapping between Pos01 entity and DtoPos01 DTO
            CreateMap<Pos01, DtoPos01>()
                .ForMember(dest => dest.S01102, opt => opt.MapFrom(src => src.S01F04))
                .ReverseMap();

            // Mapping between Com01 entity and DtoCom01 DTO
            CreateMap<Com01, DtoCom01>()
                .ForMember(dest => dest.M01101, opt => opt.MapFrom(src => src.M01F02))
                .ForMember(dest => dest.M01102, opt => opt.MapFrom(src => src.M01F04))
                .ReverseMap();

            // Mapping between Fol01 entity and DtoFol01 DTO
            CreateMap<Fol01, DtoFol01>()
                .ForMember(dest => dest.L01101, opt => opt.MapFrom(src => src.L01F03))
                .ReverseMap();
        }
    }
}
