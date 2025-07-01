using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO.Carrinho;

public class CarrinhoDTO
{
    public List<CarrinhoItemDTO> Itens { get; set; } = new();
    // public decimal Total => Itens.Sum(i => i.Preco * i.Quantidade);
}