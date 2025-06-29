using Model.ControleAcessos;

namespace Infra.ControlesAcessos;

public class UsuarioDAO : BaseDAO<Usuario>, IUsuarioDAO
{
    protected override string NomeTabela => "usuario";

    public async Task<Usuario?> RetornarPorEmailAsync(string email)
    {
        var sql = "SELECT * FROM usuario WHERE email=@Email";

        return await SelecionarUnicoAsync(sql, new { Email = email });
    }

    public async Task<IEnumerable<Usuario>> RetornarPrincipaisAutoresAsync()
    {
        //Aqui deveria ser feita alguma checagem para verificar a popularidade do autor
        //Para fins de exemplo, estou retornando os 20 primeiros autores ordenados por nome
        var sql = "SELECT * FROM usuario ORDER BY nome LIMIT 20";

        return await SelecionarAsync(sql);
    }
}