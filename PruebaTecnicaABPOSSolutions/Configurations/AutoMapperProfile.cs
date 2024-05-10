using AutoMapper;
using PruebaTecnicaABPOSSolutions.Inputs;
using PruebaTecnicaABPOSSolutions.Models;
using PruebaTecnicaABPOSSolutions.ViewModels;

namespace PruebaTecnicaABPOSSolutions.Configurations
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NegocioInput, Negocio>()
                .ForMember(i=>i.FechaCreacion,opt=>opt.Ignore())
                .ReverseMap();

            CreateMap<Negocio, NegocioViewModel>()
                .ForMember(vw => vw.User, opt => opt.MapFrom(src => src.User.Nombres))
                .ForMember(vm=> vm.FechaCreacion, opt=>opt.MapFrom(src=> src.FechaCreacion.ToString("dd/MM/yyyy HH:mm:ss")));

            CreateMap<MenuInput, Menu>();
            CreateMap<UserInput, User>()
             .ForMember(i => i.Id, opt => opt.Ignore())
             .ReverseMap();
        }
    }
}
