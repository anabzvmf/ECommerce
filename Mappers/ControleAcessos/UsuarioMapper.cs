using DTO.ControleAcessos;
using Model.ControleAcessos;
using Mappers;

namespace Mappers.ControleAcessos;

public class UsuarioMapper : IMapper<Usuario, UsuarioDTO>
{
    public UsuarioDTO GetDto(Usuario model)
    {
        return new UsuarioDTO
        (
            model.Id,
            model.Nome,
            model.Email,
            model.Slug,
            model.Apresentacao
        );
    }

    public void PreencherModel(Usuario model, UsuarioDTO dto)
    {
        model.Id = dto.Id;
        model.Email = dto.Email;
        model.Nome = dto.Nome;
        model.Slug = dto.Slug;
        model.Apresentacao = dto.Apresentacao;
    }
}