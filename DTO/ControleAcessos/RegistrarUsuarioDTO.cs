namespace DTO.ControleAcessos;

public record struct RegistrarUsuarioDTO(
    string Nome,
    string Email,
    string Senha
)
{
}
