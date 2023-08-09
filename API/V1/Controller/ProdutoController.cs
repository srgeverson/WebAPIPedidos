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
public class ProdutoController : ControllerBase
{
    private IProdutoService _produtoService;
    private IProdutoMapper _produtoMapper;

    public ProdutoController(IProdutoService produtoService, IProdutoMapper produtoMapper)
    {
        _produtoService = produtoService;
        _produtoMapper = produtoMapper;
    }

    /// <summary>
    /// Apagar produto por código.
    /// </summary>
    /// <response code="200">Produto encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Produto não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpDelete("apagar"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(PadraoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ApagarPorId([FromQuery] int? codigo)
    {
        try
        {
            if (codigo.HasValue)
            {
                var produtoExistente = await _produtoService.BuscarPorId((int)codigo);
                if (produtoExistente == null)
                    throw new ProblemaException(404, String.Format("Produto com Código = {0} não foi encontrado!", codigo));
                else
                {
                    await _produtoService.Apagar(produtoExistente);

                    return Ok(new PadraoResponse() { Mensagens = new List<string>() { "Produto apagado com sucesso!" } });
                }
            }
            else
                throw new ProblemaException(400, String.Format("Código = {0} inválido!", codigo));
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao apagar Produto", Descricao = pex.Message });
        }
    }

    /// <summary>
    /// Atualizar Produto.
    /// </summary>
    /// <response code="200">Produto encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Produto não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpPut("atualizar"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AtualizarProduto([FromQuery] int? codigo, [FromBody] ProdutoRequest request)
    {
        try
        {
            if (codigo.HasValue)
            {
                var produto = await _produtoService.BuscarPorId((int)codigo);
                if (produto == null)
                    throw new ProblemaException(404, String.Format("Produto com Código = {0} não foi encontrado!", codigo));
                else
                {
                    var produtoNovo = _produtoMapper.ToEntity(request);
                    produtoNovo.Codigo=produto.Codigo;
                    var produtoAtualizado = await _produtoService.Atualizar(produtoNovo);
                    return Ok(produtoAtualizado);
                }
            }
            else
                throw new ProblemaException(400, String.Format("Código = {0} inválido!", codigo));
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao atualizar Produto", Descricao = pex.Message });
        }
    }

    /// <summary>
    /// Lista todos produtoes cadastrados.
    /// </summary>
    /// <response code="200">Todos produtoes encontrados.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpGet("todos"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(IList<ProdutoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Produtoes()
    {
        try
        {
            var lista = await _produtoService.ListarTodos();
            return Ok(lista);
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao consultar todos Produto", Descricao = pex.Message });
        }
    }

    /// <summary>
    /// Buscar Produto por código.
    /// </summary>
    /// <response code="200">Produto encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="404">Produto não encontrado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpGet("por-codigo"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ProdutoPorId([FromQuery] int? codigo)
    {
        try
        {
            if (codigo.HasValue)
            {
                var Produto = await _produtoService.BuscarPorId((int)codigo);
                if (Produto == null)
                    throw new ProblemaException(404, String.Format("Produto com Código = {0} não foi encontrado!", codigo));
                else
                    return Ok(Produto);
            }
            else
                throw new ProblemaException(400, String.Format("Código = {0} inválido!", codigo));
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao consultar Produto por codigo", Descricao = pex.Message });
        }
    }

    /// <summary>
    /// Cadastrar Produto.
    /// </summary>
    /// <response code="201">Produto encontrado.</response>
    /// <response code="400">Dados informados incorretamenten.</response>
    /// <response code="409">Produto duplicado.</response>
    /// <response code="500">Erro interno de sistema.</response>
    [HttpPost("cadastrar"), MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(ProdutoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemaResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SalvarProduto([FromBody] ProdutoRequest request)
    {
        try
        {
            var produtoNovo = _produtoMapper.ToEntity(request);
            var produtoCadastrado = await _produtoService.Salvar(produtoNovo);
            return Ok(produtoCadastrado);
        }
        catch (ProblemaException pex)
        {
            return StatusCode(pex.Id, new ProblemaResponse() { Codigo = pex.Id, Mensagem = "Falha ao cadastrar Produto", Descricao = pex.Message });
        }
    }
}
