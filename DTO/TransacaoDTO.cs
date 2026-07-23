using ControleFinanceiro.DTO;
using ControleFinanceiro.Enums; 

public class CreateTransacaoDTO
{
  public string IdUsuario {get; set;}
  public TipoTransacao Tipo      {get; set;}
  public string Descricao {get; set;}
  public float         Valor {get; set;}
}

public class GetTransacaoDto
{
    public string Id { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public TipoTransacao Tipo { get; set; }
    public float Valor { get; set; }
}
