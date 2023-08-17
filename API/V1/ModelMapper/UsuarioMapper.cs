using AutoMapper;
using WebAPIPedidos.API.V1.Model.Response;
using WebAPIPedidos.Domain.Model.DTO;

namespace WebAPIPedidos.API.V1.ModelMapper;

public interface IUsuarioMapper
{
    UsuarioAutenticadoResponse ToAutenticadoResponse(UsuarioAutenticadoDTO usuarioAutenticado);
}
public class UsuarioMapper : Profile, IUsuarioMapper
{
    public UsuarioAutenticadoResponse ToAutenticadoResponse(UsuarioAutenticadoDTO usuarioAutenticado)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UsuarioAutenticadoDTO, UsuarioAutenticadoResponse>());
        var mapper = new Mapper(config);
        return mapper.Map<UsuarioAutenticadoResponse>(usuarioAutenticado);
    }
}
