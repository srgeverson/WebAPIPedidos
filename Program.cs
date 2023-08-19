using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using WebAPIPedidos.API.V1.ModelMapper;
using WebAPIPedidos.Core;
using WebAPIPedidos.Domain.DAO.Repository;
using WebAPIPedidos.Domain.Facade;
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

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#region Autorização
var certs = new X509Certificate2Collection();

var certName = Environment.GetEnvironmentVariable("CERTIFICATE_NAME");
if (string.IsNullOrEmpty(certName))
    certName = "localhost.pfx";
var certPassword = Environment.GetEnvironmentVariable("CERTIFICATE_PASSWORD");
if (string.IsNullOrEmpty(certPassword))
    certPassword = "@G12345678";
if (builder.Environment.IsDevelopment())
{
    var fileName = Path.Combine(AppContext.BaseDirectory, certName);
    if (!File.Exists(fileName))
        throw new FileNotFoundException("Signing Certificate is missing!");
    certs.Add(new X509Certificate2(fileName, certPassword));
}
else
{
    var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
    store.Open(OpenFlags.ReadOnly);
    certs = store.Certificates.Find(X509FindType.FindByThumbprint, certName, false);
}

builder.Services
    .AddIdentityServer()
    .AddInMemoryClients(new List<Client>
    {
        new()
        {
            ClientId = "console",
            ClientSecrets = new List<Secret> {new("secret".Sha256())},
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = new List<string> {"api"},
        }
    })
    .AddInMemoryApiScopes(new List<ApiScope>
    {
        new("api")
    })
    .AddInMemoryApiResources(new List<ApiResource>
    {
        new("api")
        {
            Scopes = new List<string> {"api"}
        }
    })
    .AddSigningCredential(certs.First());
#endregion

#region Swagger 3.0 https://github.com/microsoft/aspnet-api-versioning/tree/master/samples/aspnetcore/SwaggerSample

builder.Services.AddSwaggerGen(options =>
{
    options.DocumentFilter<TagDescriptionsDocumentFilter>();
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows()
        {
            ClientCredentials = new OpenApiOAuthFlow()
            {
                TokenUrl = new Uri("https://localhost:44370/connect/token"),
                Scopes = new Dictionary<string, string> { { "api", "API" } }
            }
        }
    });
    options.OperationFilter<SwaggerDefaultValues>();
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var authorize = Environment.GetEnvironmentVariable("URL_AUTHORIZE");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:44370";
        options.Audience = "api";
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", builder =>
    {
        builder.RequireAuthenticatedUser();
        builder.RequireClaim("scope", "api");
    });
});

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

builder.Services.AddSingleton<IConfiguration>(_ => builder.Configuration);

#region DAOs
var conexao = Environment.GetEnvironmentVariable("URL_DB_WebAPIPedidos");
builder.Services.AddDbContextPool<ContextRepository>(options =>
{
    options.UseSqlServer(conexao, providerOptions => { providerOptions.EnableRetryOnFailure(); });
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddTransient<IFornecedorRepositoty, FornecedorRepositoty>();
builder.Services.AddTransient<IPedidoRepositoty, PedidoRepositoty>();
builder.Services.AddTransient<IProdutoRepositoty, ProdutoRepositoty>();
builder.Services.AddTransient<IUsuarioRepositoty, UsuarioRepositoty>();
#endregion

#region Servços
builder.Services.AddScoped<IFornecedorService, FornecedorService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
#endregion

#region Fachadas
builder.Services.AddScoped<IAutenticacaoFacade, AutenticacaoFacade>();
builder.Services.AddScoped<ICompraFacade, CompraFacade>();
#endregion

#region Mapeamentos
builder.Services.AddScoped<IFornecedorMapper, FornecedorMapper>();
builder.Services.AddScoped<IProdutoMapper, ProdutoMapper>();
builder.Services.AddScoped<IPedidoMapper, PedidoMapper>();
builder.Services.AddScoped<IUsuarioMapper, UsuarioMapper>();
#endregion

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    foreach (var description in app.Services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions)
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", string.Format("{0} API de Pedidos", description.GroupName.ToUpperInvariant()));
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.UseIdentityServer();

app.Run();