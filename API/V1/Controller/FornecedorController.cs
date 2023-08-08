﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WebAPIPedidos.API.V1.Exception;
using WebAPIPedidos.API.V1.Model.Request;
using WebAPIPedidos.API.V1.Model.Response;
using WebAPIPedidos.API.V1.ModelMapper;
using WebAPIPedidos.Domain.Service;

namespace WebAPIPedidos.API.V1.Controller;

[ApiController]
[ApiVersion("1.0", Deprecated = false)]
[Route("/v{version:apiVersion}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class FornecedorController : ControllerBase
{
    private IFornecedorService _fornecedorService;
    private IFornecedorMapper _fornecedorMapper;

    public FornecedorController(IFornecedorService fornecedorService, IFornecedorMapper fornecedorMapper)
    {
        _fornecedorService = fornecedorService;
        _fornecedorMapper = fornecedorMapper;
    }

    /// <summary>
    /// Apagar fornecedor por ID.
    /// </summary>
    /// <response code="200">Fornecedor encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Fornecedor não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpDelete("apagar"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(PadraoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ApagarPorId([FromQuery] int? id)
    {
        try
        {
            if (id.HasValue)
            {
                var fornecedorExistente = await _fornecedorService.BuscarPorId((int)id);
                if (fornecedorExistente == null)
                    throw new ProblemaException(404, String.Format("Fornecedor com ID = {0} não foi encontrado!", id));
                else
                {
                    await _fornecedorService.Apagar(fornecedorExistente);

                    return Ok(new PadraoResponse() { Mensagens = new List<string>() { "Fornecedor apagado com sucesso!" } });
                }
            }
            else
                throw new ProblemaException(400, String.Format("ID = {0} inválido!", id));
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao apagar fornecedor", Descricao = pex.Message });
        }
    }

    /// <summary>
    /// Atualizar fornecedor.
    /// </summary>
    /// <response code="200">Fornecedor encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Fornecedor não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpPut("atualizar"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(FornecedorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarFornecedor([FromQuery] int? id, [FromBody] FornecedorRequest request)
    {
        try
        {
            if (id.HasValue)
            {
                var fornecedor = await _fornecedorService.BuscarPorId((int)id);
                if (fornecedor == null)
                    throw new ProblemaException(404, String.Format("Fornecedor com ID = {0} não foi encontrado!", id));
                else
                {
                    var fornecedorNovo = _fornecedorMapper.ToEntity(request);
                    var fornecedorAtualizado = await _fornecedorService.Atualizar(fornecedor);
                    return Ok(fornecedor);
                }
            }
            else
                throw new ProblemaException(400, String.Format("ID = {0} inválido!", id));
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao atualizar fornecedor", Descricao = pex.Message });
        }
    }

    /// <summary>
    /// Lista todos fornecedores cadastrados.
    /// </summary>
    /// <response code="200">Todos fornecedores encontrados.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpGet("todos"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(IList<FornecedorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Fornecedores()
    {
        try
        {
            var lista = await _fornecedorService.ListarTodos();
            return Ok(lista);
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao consultar todos fornecedor", Descricao = pex.Message });
        }
    }

    /// <summary>
    /// Buscar fornecedor por ID.
    /// </summary>
    /// <response code="200">Fornecedor encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Fornecedor não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpGet("por-id"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(FornecedorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> FornecedorPorId([FromQuery] int? id)
    {
        try
        {
            if (id.HasValue)
            {
                var fornecedor = await _fornecedorService.BuscarPorId((int)id);
                if (fornecedor == null)
                    throw new ProblemaException(404, String.Format("Fornecedor com ID = {0} não foi encontrado!", id));
                else
                    return Ok(fornecedor);
            }
            else
                throw new ProblemaException(400, String.Format("ID = {0} inválido!", id));
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao consultar fornecedor por id", Descricao = pex.Message });
        }
    }

    /// <summary>
    /// Cadastrar fornecedor.
    /// </summary>
    /// <response code="201">Fornecedor encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="409">Fornecedor duplicado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpPost("cadastrar"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(FornecedorResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SalvarFornecedor([FromBody] FornecedorRequest request)
    {
        try
        {
            var fornecedorNovo = _fornecedorMapper.ToEntity(request);
            var fornecedorCadastrado = await _fornecedorService.Salvar(fornecedorNovo);
            return Ok(fornecedorCadastrado);
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao cadastrar fornecedor", Descricao = pex.Message });
        }
    }
}
