using System;

namespace DTO.Carrinho;

public class CarrinhoItemDTO
{
    public long UsuarioId { get; set; }
    public long ProdutoId { get; set; }
    public int Quantidade { get; set; }
}
