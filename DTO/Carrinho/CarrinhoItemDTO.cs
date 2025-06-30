using System;

namespace DTO.Carrinho;

public class CarrinhoItemDTO
{
     public int ProdutoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public string? ImagemUrl { get; set; }
}
