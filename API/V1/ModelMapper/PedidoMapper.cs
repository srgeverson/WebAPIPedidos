using AutoMapper;
using Azure.Core;
using System.Linq;
using WebAPIPedidos.API.V1.Model.Request;
using WebAPIPedidos.API.V1.Model.Response;
using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.API.V1.ModelMapper;

public interface IPedidoMapper
{
    PedidoEntity ToEntity(PedidoRequest request);
    IList<PedidoEntity> ToListEntity(PedidoLoteRequest request);
    PedidoId ToListIdEntity(PedidoIdRequest id);
    IList<PedidoResponse> ToListResponse(IList<PedidoEntity> pedidoCadastrado);
    PedidoResponse ToResponse(PedidoEntity entity);
}
public class PedidoMapper : Profile, IPedidoMapper
{
    public PedidoEntity ToEntity(PedidoRequest request)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<PedidoRequest, PedidoEntity>();
        });
        var mapper = new Mapper(config);
        return mapper.Map<PedidoEntity>(request);
    }

    public IList<PedidoEntity> ToListEntity(PedidoLoteRequest request)
    {
        var itens = new List<PedidoEntity>();
        request.PedidoRequests.ToList().ForEach(i => itens.Add(ToEntity(i)));
        return itens;
    }

    public PedidoId ToListIdEntity(PedidoIdRequest id)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<PedidoIdRequest, PedidoId>();
        });
        var mapper = new Mapper(config);
        return mapper.Map<PedidoId>(id);
    }

    public IList<PedidoResponse> ToListResponse(IList<PedidoEntity> pedidos)
    {
        var itens = new List<PedidoResponse>();
        pedidos.ToList().ForEach(i => itens.Add(ToResponse(i)));
        return itens;
    }

    public PedidoResponse ToResponse(PedidoEntity entity)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<PedidoEntity, PedidoResponse>());
        var mapper = new Mapper(config);
        return mapper.Map<PedidoResponse>(entity);
    }
}
