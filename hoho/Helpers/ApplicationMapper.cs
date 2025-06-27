using AutoMapper;
using hoho.Data   ;
using hoho.DTO;
using hoho.DTOs.OITM;
using hoho.DTOs.OITW;
using hoho.DTOs.OUGP;
using hoho.DTOs.OUOM;
using hoho.DTOs.UGP1;

using hoho.Models.OUGP;
using hoho.Models.OUOM;
using hoho.Models.UGP1;
namespace hoho.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<OITM, OITMDTO>()
               
                .ReverseMap()
                    .ForMember(dest => dest.WarehouseStocks, opt => opt.Ignore()) ;


            CreateMap<OITW, OITWDTO>()
                .ReverseMap();


            CreateMap<OUGP, OUGPModel>();
            CreateMap<OUGP, OUGPDTO>().ForMember(dest => dest.UGP1, opt => opt.MapFrom(src => src.UnitConversions));
            ;
            CreateMap<OUGP, OUGPUpdateDto>();
            CreateMap<OUGPCreateDto, OUGP>();
            CreateMap<OUGPUpdateDto, UGP1>();

            CreateMap<OUOMModel, OUOM>();
            CreateMap<OUOM, OUOMDTO>();

           
            CreateMap<UGP1, UGP1DTO>().ReverseMap();
            CreateMap<UGP1Model, UGP1>();

        }
    }
}
