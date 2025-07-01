using Model.ControleAcessos;
using System.Text.Json.Serialization;

namespace Model.Produtos;

public class Produto : BaseModel
{

    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Preco { get; set; }
    public int Estoque;
    public string ImagemUrl { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }
    private long Id;
}
