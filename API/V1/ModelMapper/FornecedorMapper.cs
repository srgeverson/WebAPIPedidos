using AutoMapper;
using WebAPIPedidos.API.V1.Model.Request;
using WebAPIPedidos.API.V1.Model.Response;
using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.API.V1.ModelMapper;

public interface IFornecedorMapper
{
    FornecedorEntity ToEntity(FornecedorRequest request);
    FornecedorResponse ToResponse(FornecedorEntity entity);
}
public class FornecedorMapper : Profile, IFornecedorMapper
{
    public FornecedorEntity ToEntity(FornecedorRequest request)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<FornecedorRequest, FornecedorEntity>();
        });
        var mapper = new Mapper(config);
        return mapper.Map<FornecedorEntity>(request);
    }

    public FornecedorResponse ToResponse(FornecedorEntity entity)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<FornecedorEntity, FornecedorResponse>());
        var mapper = new Mapper(config);
        return mapper.Map<FornecedorResponse>(entity);
    }
}
