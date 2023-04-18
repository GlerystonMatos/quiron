using AutoMapper;
using Quiron.Domain.Dto;
using Quiron.Domain.Entities;

namespace Quiron.Service.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Animal, AnimalDto>()
                .ReverseMap();

            CreateMap<Usuario, UsuarioDto>()
                .ReverseMap();
        }
    }
}