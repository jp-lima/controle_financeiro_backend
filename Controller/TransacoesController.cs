namespace ControleFinanceiro.Controller;
using Microsoft.EntityFrameworkCore; 
using Microsoft.AspNetCore.Mvc;  
using ControleFinanceiro.Data; 
using ControleFinanceiro.Models;
using ControleFinanceiro.DTO;
using ControleFinanceiro.Enums; 

[ApiController]
[Route("api/[controller]")]
public class TransacaoController : ControllerBase
{
  private readonly AppDbContext _appDbContext;  
  public TransacaoController(AppDbContext appDbContext)
  {
    _appDbContext = appDbContext;
  }

  [HttpPost]
  public async Task<IActionResult> addTransicion(CreateTransacaoDTO dto)
  {
    Usuario? usuario = await _appDbContext.Usuarios.FindAsync(dto.IdUsuario);
    if(usuario is null) return NotFound("Usuario nao encontrado para esse id"); 
    if (usuario.Idade < 18 && dto.Tipo == TipoTransacao.Receita)
    {
        return BadRequest("Usuários menores de 18 anos só podem cadastrar despesas.");
    }

   var transacao = new Transacao{
        Id = Guid.NewGuid().ToString(),
        Tipo = dto.Tipo,
        IdUsuario = dto.IdUsuario, 
        Descricao = dto.Descricao,
        Valor = dto.Valor
       }; 

    var resposta = await _appDbContext.Transacoes.AddAsync(transacao); 
    await _appDbContext.SaveChangesAsync();
    return Ok(transacao); 
  }
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Transacao>>> getTransacoes()
  {
    var transacoes = await _appDbContext.Transacoes.ToArrayAsync(); 
    return Ok(transacoes); 
  }
}

