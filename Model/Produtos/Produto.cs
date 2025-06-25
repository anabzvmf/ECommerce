using Model.ControleAcessos;
using System.Text.Json.Serialization;

namespace Model.Produtos;

public class Produto : BaseModel
{

    public string nome { get; set; } = string.Empty;
    public string descricao { get; set; } = string.Empty;

    private long idProduto;
}
