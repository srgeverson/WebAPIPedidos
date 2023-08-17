using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
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

#region Swagger 3.0 https://github.com/microsoft/aspnet-api-versioning/tree/master/samples/aspnetcore/SwaggerSample

builder.Services.AddSwaggerGen(options =>
{
    options.DocumentFilter<TagDescriptionsDocumentFilter>();
    options.OperationFilter<SwaggerDefaultValues>();
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
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

#region Autorização
#pragma warning disable ASP5001 // Type or member is obsolete
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
}).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
#pragma warning restore ASP5001 // Type or member is obsolete

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(WebAPIPedido.SECRET_KEY),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    foreach (var description in
        app.Services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions)
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", string.Format("{0} API de Pedidos",description.GroupName.ToUpperInvariant()));
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseCors();

app.MapControllers();

app.Run();