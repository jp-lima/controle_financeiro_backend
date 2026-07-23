namespace ControleFinanceiro.DTO; 
using ControleFinanceiro.Models; 

public class CreateUsuarioDto
{
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
}


public class GetUsuarioDto
{
    public string Id { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public List<GetTransacaoDto> Transacoes {get; set;}
}

