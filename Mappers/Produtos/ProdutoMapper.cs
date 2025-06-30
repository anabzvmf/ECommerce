using DTO.Produtos;
using Model.Produtos;
using Mappers;

namespace Mappers.Produtos
{
    public class ProdutoMapper : IMapper<Produto, ProdutoDTO>
    {
        public ProdutoDTO GetDto(Produto model)
        {
            return new ProdutoDTO
            (
                model.Id,
                model.Nome,
                model.Descricao,
                model.Preco,
                model.Estoque,
                model.ImagemUrl, 
                model.DataCadastro
            );
        }

        public void PreencherModel(Produto model, ProdutoDTO dto)
        {
            model.Id = dto.Id;
            model.Nome = dto.Nome;
            model.Descricao = dto.Descricao;
            model.Preco = dto.Preco;
            model.Estoque = dto.Estoque;
            model.ImagemUrl = dto.ImagemUrl; 
            model.DataCadastro = dto.DataCadastro; 
        }
    }
}
