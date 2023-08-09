using AutoMapper;
using WebAPIPedidos.API.V1.Model.Request;
using WebAPIPedidos.API.V1.Model.Response;
using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.API.V1.ModelMapper;

public interface IProdutoMapper
{
    ProdutoEntity ToEntity(ProdutoRequest request);
    ProdutoResponse ToResponse(ProdutoEntity entity);
}
public class ProdutoMapper : Profile, IProdutoMapper
{
    public ProdutoEntity ToEntity(ProdutoRequest request)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ProdutoRequest, ProdutoEntity>();
        });
        var mapper = new Mapper(config);
        return mapper.Map<ProdutoEntity>(request);
    }

    public ProdutoResponse ToResponse(ProdutoEntity entity)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<ProdutoEntity, ProdutoResponse>());
        var mapper = new Mapper(config);
        return mapper.Map<ProdutoResponse>(entity);
    }
}
