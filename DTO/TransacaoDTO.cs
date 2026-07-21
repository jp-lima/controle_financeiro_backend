using ControleFinanceiro.DTO;
using ControleFinanceiro.Enums; 

public class CreateTransacaoDTO
{
  public string IdUsuario {get; set;}
  public TipoTransacao Tipo      {get; set;}
  public string Descricao {get; set;}
  public float         Valor {get; set;}
}
