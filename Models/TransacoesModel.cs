namespace ControleFinanceiro.Models; 
using ControleFinanceiro.Enums; 

public class Transacao
{
  public string        Id        {get; set;}
  public string        Descricao {get; set;}
  public TipoTransacao Tipo      {get; set;}
  public string        IdUsuario {get; set;}
  public float         Valor {get; set;}

}
