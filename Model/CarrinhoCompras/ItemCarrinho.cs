using Model.Produtos;

namespace Model.CarrinhoCompras;
public class ItemCarrinho : BaseModel
{
    public Produto Produto { get; set; }
    public int Quantidade { get; set; }
}