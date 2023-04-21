using AutoMapper;
using Quiron.Domain.Dto;
using Quiron.Domain.Dto.Exibicao;
using Quiron.Domain.Entities;

namespace Quiron.Service.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Animal, AnimalDto>()
                .ReverseMap();

            CreateMap<Estado, EstadoDto>()
                .ReverseMap();

            CreateMap<Cidade, CidadeDto>()
                .ReverseMap();

            CreateMap<Usuario, UsuarioDto>()
                .ReverseMap();

            CreateMap<Estado, EstadoCidadeDto>()
                .ReverseMap();

            CreateMap<Cidade, CidadeEstadoDto>()
                .ReverseMap();
        }
    }
}