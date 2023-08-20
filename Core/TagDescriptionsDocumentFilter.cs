using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPIPedidos.Core;

public class TagDescriptionsDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var todasTags = new List<OpenApiTag> {
                new OpenApiTag { Name = "Fornecedor", Description = "Controlador responsável pelas operações relativas ao fornecedor." },
                new OpenApiTag { Name = "Host", Description = "Controlador responsável pelas configurações da aplicação." },
                new OpenApiTag { Name = "Pedido", Description = "Controlador responsável pelas operações relativas ao pedido." },
                new OpenApiTag { Name = "Produto", Description = "Controlador responsável pelas operações relativas ao produto." },
                new OpenApiTag { Name = "Usuario", Description = "Controlador responsável pelas operações relativas ao usuário." },
            };
        var tagsRespectivaVersao = new List<OpenApiTag>();
        todasTags.ForEach(tag =>
        {
            if (swaggerDoc.Paths.Where(p => p.Key.StartsWith(string.Concat("/", context.DocumentName, "/", tag.Name))).Any())
                tagsRespectivaVersao.Add(tag);
        });
        swaggerDoc.Tags = tagsRespectivaVersao;
    }
}