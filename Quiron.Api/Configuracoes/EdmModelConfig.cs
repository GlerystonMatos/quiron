using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Quiron.Domain.Dto;

namespace Quiron.Api.Configuracoes
{
    public class EdmModelConfig
    {
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder odataBuilder = new ODataConventionModelBuilder();

            odataBuilder.EntitySet<AnimalDto>("Animal");
            odataBuilder.EntitySet<EstadoDto>("Estado");
            odataBuilder.EntitySet<CidadeDto>("Cidade");
            odataBuilder.EntitySet<UsuarioDto>("Usuario");

            return odataBuilder.GetEdmModel();
        }
    }
}