using AutoMapper;
using WebAPIPedidos.API.V1.Model.Request;
using WebAPIPedidos.API.V1.Model.Response;
using WebAPIPedidos.Domain.Model.DTO;
using WebAPIPedidos.Domain.Model.Entity;

namespace WebAPIPedidos.API.V1.ModelMapper;

public interface IUsuarioMapper
{
    UsuarioAutenticadoResponse ToAutenticadoResponse(UsuarioAutenticadoDTO usuarioAutenticado);
    UsuarioEntity ToEntity(FornecedorRequest request);
}
public class UsuarioMapper : Profile, IUsuarioMapper
{
    public UsuarioAutenticadoResponse ToAutenticadoResponse(UsuarioAutenticadoDTO usuarioAutenticado)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UsuarioAutenticadoDTO, UsuarioAutenticadoResponse>());
        var mapper = new Mapper(config);
        return mapper.Map<UsuarioAutenticadoResponse>(usuarioAutenticado);
    }

    public UsuarioEntity ToEntity(FornecedorRequest request)
    {
        throw new NotImplementedException();
    }
}
