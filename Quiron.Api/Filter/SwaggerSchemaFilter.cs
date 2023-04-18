using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Quiron.Api.Filter
{
    public class SwaggerSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            foreach (var item in context.SchemaRepository.Schemas)
            {
                if ((item.Key.Contains("Edm")) || (item.Key.Contains("OData")))
                {
                    context.SchemaRepository.Schemas.Remove(item.Key);
                }
            }
        }
    }
}