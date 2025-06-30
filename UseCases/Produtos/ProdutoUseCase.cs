using DTO.Produtos;
using Infra.Produtos;
using Mappers;
using Model.Produtos;
using System;
using System.Threading.Tasks;

namespace UseCases.Produtos
{
    public class ProdutoUseCase : BaseUseCase, IProdutoUseCase
    {
        private readonly IProdutoDAO produtoDAO;
        private readonly IMapper<Produto, ProdutoDTO> produtoMapper;

        public ProdutoUseCase(IProdutoDAO produtoDAO, IMapper<Produto, ProdutoDTO> produtoMapper)
        {
            this.produtoDAO = produtoDAO;
            this.produtoMapper = produtoMapper;
        }

        public async Task<ResultadoUnico<ProdutoDTO>> InserirProduto(RegistrarProdutoDTO produtoDTO)
        {
            try
            {
                Console.WriteLine($"[RegistrarProduto Usecase] Dados recebidos: {System.Text.Json.JsonSerializer.Serialize(produtoDTO)}");

                var produto = new Produto
                {
                    Nome = produtoDTO.Nome,
                    Descricao = produtoDTO.Descricao,
                    Preco = produtoDTO.Preco,
                    Estoque = produtoDTO.Estoque
                    // ImagemUrl = produtoDTO.ImagemUrl,  
                    // DataCadastro = produtoDTO.DataCadastro  
                };

                Console.WriteLine($"[RegistrarProduto Usecase] Produto a ser inserido: {System.Text.Json.JsonSerializer.Serialize(produto)}");

                // Chamada assíncrona para inserir o produto
                await produtoDAO.InserirAsync(produto);

                return SucessoObjeto(produtoMapper.GetDto(produto));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RegistrarProduto] Erro: {ex.Message}");

                return FalhaObjeto<ProdutoDTO>([new("Erro na tentativa de obter usuário por e-mail.", MensagemRetorno.EOrigem.Erro)]);
            }
        }
        public async Task<List<ProdutoDTO>> ObterProdutosAsync(string complementoUrl)
        {
            try
            {
                var produtos = await produtoDAO.ObterProdutosAsync(complementoUrl);
                var produtosDTO = produtos.Select(p => produtoMapper.GetDto(p)).ToList();

                return produtosDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter produtos: {ex.Message}");
                return new List<ProdutoDTO>();
            }

        }
    }
}
