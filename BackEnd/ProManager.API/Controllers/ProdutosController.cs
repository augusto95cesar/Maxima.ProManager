using Microsoft.AspNetCore.Mvc;
using ProManager.Application.DTOs.Input;
using ProManager.Application.EventHandlers;
using ProManager.Application.Interfaces;
using ProManager.Application.Mappers;

namespace ProManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _produtoService.ObterTodosAsync().Map());

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProdutoPostDTO dtoInputProduto)
        {
            var validationResult = ProdutoRequestHandler.ValidateProduto(dtoInputProduto);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            await _produtoService.AdicionarAsync(dtoInputProduto.Map());

            // caso eu queira retornar alguma info diferente, montaria um dto de output
            var codigoResuto = dtoInputProduto.Codigo;
            return CreatedAtAction(null, new { codigo = codigoResuto }, dtoInputProduto);
        } 
    }

}
