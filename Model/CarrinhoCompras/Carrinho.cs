using Model.ControleAcessos;
using Model.Produtos;

namespace Model.CarrinhoCompras
{
    public class Carrinho : BaseModel
    {
        public List<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();

        public long usuarioId { get; set; }

        public double valorFinal
        {
            get
            {
                return Itens.Sum(item => item.Produto.Preco * item.Quantidade);
            }
        }
        public void addItem(Produto produto, int quantidade)
        {
            var item = Itens.FirstOrDefault(item => item.Produto.Id == produto.Id);

            if (item != null)
                item.Quantidade += quantidade;

            else
                Itens.Add(new ItemCarrinho { Produto = produto, Quantidade = quantidade });
        }
        public void RemoverItem(int produtoId)
        {
            var item = Itens.FirstOrDefault(i => i.Produto.Id == produtoId);
            if (item != null)
            {
                Itens.Remove(item);
            }
        }
        public void LimparCarrinho()
        {
            Itens.Clear();
        }

    }
}
