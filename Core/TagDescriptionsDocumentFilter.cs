using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace WebAPIPedidos.Core;

public class TagDescriptionsDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var todasTags = new List<OpenApiTag> {
                new OpenApiTag { Name = "Fornecedor", Description = "Controlador responsável pelas operações relaticas ao fornecedor." },
            };
        var tagsRespectivaVersao = new List<OpenApiTag>();
        todasTags.ForEach(tag =>
        {
            if (swaggerDoc.Paths.Where(p => p.Key.StartsWith(string.Concat("/", context.DocumentName, "/", tag.Name, "s"))).Any())
                tagsRespectivaVersao.Add(tag);
        });
        swaggerDoc.Tags = tagsRespectivaVersao;

        //var todasAsClasses = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("WebAPIPedidos, Version")).First().DefinedTypes.Select(t => t.Name);
        //IDictionary<string, OpenApiSchema> _remove = swaggerDoc.Components.Schemas;
        //foreach (KeyValuePair<string, OpenApiSchema> _item in _remove)
        //{
        //    if (!todasAsClasses.Contains(_item.Key) || _item.Key.Contains("ProblemaException"))
        //        swaggerDoc.Components.Schemas.Remove(_item.Key);
        //}
    }
}