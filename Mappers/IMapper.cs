using Model;
using DTO;
using Model.ControleAcessos;
using DTO.ControleAcessos;

namespace Mappers;

public interface IMapper<TModel, TDto>
{
    TDto GetDto(TModel model);
    void PreencherModel(TModel model, TDto dto);
}
