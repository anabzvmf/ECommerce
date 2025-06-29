namespace DTO.Imagem;

public record struct ImagemDTO(
    string Nome,
    long Tamanho,
    DateTimeOffset UltimaModificacao
)
{}