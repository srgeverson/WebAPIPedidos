using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using WebAPIPedidos.API.V1.ModelMapper;
using WebAPIPedidos.Core;
using WebAPIPedidos.Domain.DAO.Repository;
using WebAPIPedidos.Domain.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config
        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwaggerGen(options =>
{
    options.DocumentFilter<TagDescriptionsDocumentFilter>();
    options.OperationFilter<SwaggerDefaultValues>();
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

#region Swagger 3.0 https://github.com/microsoft/aspnet-api-versioning/tree/master/samples/aspnetcore/SwaggerSample

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
#endregion

#region DAOs
var conexao = Environment.GetEnvironmentVariable("CONNECTION_STRING_WebAPIPedidos");
builder.Services.AddSingleton<IFornecedorRepositoty, FornecedorRepositoty>();
#endregion

#region Servços
builder.Services.AddSingleton<IFornecedorService, FornecedorService>();
#endregion

#region Fachadas
//builder.Services.AddSingleton<ICompraFacade, CompraFacade>();
#endregion

#region Mapeamentos
builder.Services.AddSingleton<IFornecedorMapper, FornecedorMapper>();
#endregion

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    foreach (var description in
        app.Services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions)
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();