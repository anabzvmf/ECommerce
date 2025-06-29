namespace DTO.ControleAcessos;

public record struct UsuarioDTO(
    long Id,
    string Nome,
    string Email
)
{
}