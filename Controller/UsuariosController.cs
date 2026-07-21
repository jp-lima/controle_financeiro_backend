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
  public async Task<ActionResult<IEnumerable<Usuario>>> getUsuarios()
  {
    var usuarios = await _appDbContext.Usuarios.ToArrayAsync(); 
    return Ok(usuarios);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Usuario>> getUsuarioById(string id)
  {
    Usuario? usuario = await _appDbContext.Usuarios.FindAsync(id);
    if(usuario is null) return NotFound("Usuario nao encontrado para esse id"); 
    return usuario;
  }
}


