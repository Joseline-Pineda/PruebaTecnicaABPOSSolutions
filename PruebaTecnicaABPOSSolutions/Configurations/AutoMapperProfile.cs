using AutoMapper;
using PruebaTecnicaABPOSSolutions.Inputs;
using PruebaTecnicaABPOSSolutions.Models;

namespace PruebaTecnicaABPOSSolutions.Configurations
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NegocioInput,Negocio> ()
                .ReverseMap();

        }
    }
}
