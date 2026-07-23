namespace ControleFinanceiro.Controller;
using Microsoft.EntityFrameworkCore; 
using Microsoft.AspNetCore.Mvc; 
using ControleFinanceiro.Data; 
using ControleFinanceiro.Models;
using ControleFinanceiro.DTO; 

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
  private readonly AppDbContext _appDbContext; 

  public UsuariosController(AppDbContext appDbContext)
  {
    _appDbContext = appDbContext; 
  }

  [HttpPost]
  public async Task<IActionResult> addUsuario(CreateUsuarioDto dto)
  {
    var usuario = new Usuario
    {
        Id = Guid.NewGuid().ToString(),
        Nome = dto.Nome,
        Idade = dto.Idade
    };

    _appDbContext.Usuarios.Add(usuario);
    await _appDbContext.SaveChangesAsync(); 
    return Ok(usuario); 
  }
  [HttpGet]
  public async Task<ActionResult<IEnumerable<GetUsuarioDto>>> getUsuarios()
  {
    List<GetUsuarioDto> ListaUsuarios = []; 
    int contador = 0; 
    var usuarios = await _appDbContext.Usuarios.ToArrayAsync(); 
    foreach(var usuario in usuarios)
    {
      var transictions = await _appDbContext.Transacoes.Where(t => t.IdUsuario == usuario.Id).Select(t => new GetTransacaoDto{
          Id = t.Id,
          Descricao = t.Descricao,
          Tipo = t.Tipo,
          Valor = t.Valor
          }
          ).ToListAsync(); 

      GetUsuarioDto usuarioDto = new GetUsuarioDto{
           Id = usuario.Id,
           Nome = usuario.Nome,  
           Idade = usuario.Idade,
           Transacoes = transictions
      };  
      
      ListaUsuarios.Add(usuarioDto); 
      contador++; 
    }
    return Ok(ListaUsuarios);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Usuario>> getUsuarioById(string id)
  {
    Usuario? usuario = await _appDbContext.Usuarios.FindAsync(id);
    if(usuario is null) return NotFound("Usuario nao encontrado para esse id"); 
    return usuario;
  }
  [HttpDelete("{id}")]
  public async Task<IActionResult> deleteUsuarioById(string id)
  {
    Usuario? usuario = await _appDbContext.Usuarios.FindAsync(id);
    if(usuario is null) return NotFound("Usuario nao encontrado para esse id"); 
    _appDbContext.Usuarios.Remove(usuario);   

    var transacoes = await _appDbContext.Transacoes
    .Where(t => t.IdUsuario == id)
    .ToListAsync();

    _appDbContext.Transacoes.RemoveRange(transacoes);

    await _appDbContext.SaveChangesAsync(); 
    return NoContent(); 
  }
}


