using Model;
using Dapper;
using Microsoft.Data.Sqlite;
using SnowflakeGenerator;
using System.Reflection;

namespace Infra;

public abstract class BaseDAO<T> : IBaseDAO<T> where T : IModel
{
    protected abstract string NomeTabela { get; }

    public virtual async Task InserirAsync(T obj)
    {
        if (obj.Id == 0)
            obj.Id = GetNovoId();

        string nomesCampos = "";
        string parametrosCampos = "";

        foreach (var nomeProp in GetPropriedades(obj))
        {
            if (!nomeProp.ToLower().Equals("id"))
            {
                nomesCampos += $", {nomeProp.ToLower()}";
                parametrosCampos += $", @{nomeProp}";
            }
        }

        string sql = $"INSERT INTO {NomeTabela}" +
            $" (id{nomesCampos})" +
            " values " +
            $" (@Id{parametrosCampos})";

        Console.WriteLine($"SQL gerado: {sql}");
        Console.WriteLine($"Objeto: {System.Text.Json.JsonSerializer.Serialize(obj)}");
        await ExecutarAsync(sql, obj);
    }

    public virtual async Task AlterarAsync(T obj)
    {
        var virgula = "";
        var campos = "";

        foreach (var nomeProp in GetPropriedades(obj))
        {
            campos += $"{virgula}{nomeProp.ToLower()} = @{nomeProp}";
            virgula = ",";
        }

        string sql = $"UPDATE {NomeTabela}" +
            $" SET {campos}" +
            " WHERE " +
            " id = @Id";

        await ExecutarAsync(sql, obj);
    }

    public virtual async Task ExcluirAsync(long id)
    {
        string sql = $"DELETE {NomeTabela}" +
            " WHERE " +
            " id = @Id";

        await ExecutarAsync(sql, new { Id = id });
    }

    public virtual async Task<T?> RetornarPorIdAsync(long id)
    {
        var campos = "";

        foreach (var nomeProp in GetPropriedades(typeof(T)))
            campos += $", {nomeProp.ToLower()} as {nomeProp}";

        string sql = $"SELECT id as Id{campos}" +
            $" FROM {NomeTabela}" +
            " WHERE id = @id";

        return await SelecionarUnicoAsync(sql, new { id });
    }

    protected async Task ExecutarAsync(string sql, object obj)
    {
        using var conexao = new SqliteConnection(StringConexao);

        await conexao.OpenAsync();

        await conexao.ExecuteAsync(sql, obj);

        await conexao.CloseAsync();
    }

    protected async Task<IEnumerable<T>> SelecionarAsync(string sql, object? obj = null)
    {
        using var conexao = new SqliteConnection(StringConexao);

        await conexao.OpenAsync();

        if (obj == null)
            return await conexao.QueryAsync<T>(sql);

        return await conexao.QueryAsync<T>(sql, obj);
    }

    protected async Task<T?> SelecionarUnicoAsync(string sql, object? obj = null)
    {
        T? result;
        using var conexao = new SqliteConnection(StringConexao);

        await conexao.OpenAsync();

        if (obj == null)
            result = await conexao.QuerySingleOrDefaultAsync<T>(sql);
        else
            result = await conexao.QuerySingleOrDefaultAsync<T>(sql, obj);

        await conexao.CloseAsync();
        return result;
    }

    protected async Task<K?> SelecionarUnicoAsync<K>(string sql, object? obj = null)
    {
        K? result;
        using var conexao = new SqliteConnection(StringConexao);

        await conexao.OpenAsync();

        if (obj == null)
            result = await conexao.QuerySingleOrDefaultAsync<K>(sql);

        result = await conexao.QuerySingleOrDefaultAsync<K>(sql, obj);

        await conexao.CloseAsync();
        return result;
    }

    protected IEnumerable<string> GetPropriedades(T obj)
    {
        return GetPropriedades(obj.GetType());
    }

    protected IEnumerable<string> GetPropriedades(Type tipo)
    {
        //LINQ - Language Integrated Query
        return tipo.GetProperties().Where(x => !x.Name.Equals("Id") && x.GetCustomAttribute(typeof(IgnoreEsteCampoAttribute)) == null).Select(x => x.Name);
    }

    //https://www.nuget.org/packages/SnowflakeGenerator
    public long GetNovoId()
    {
        return snowflake.NextID();
    }

    private static Settings settings = new Settings
    {
        MachineID = 1, //O ID da m�quina deve ser alterado para ser �nico para cada servidor ou inst�ncia
        CustomEpoch = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero)
    };

    private static Snowflake snowflake = new(settings);

    //Fazendo com absolute path
    // public static string StringConexao = @"Data Source=C:\Users\mathe\OneDrive\Área de Trabalho\ECommerce\Infra\_db\dados.db";


    //Fazendo com path dinâmico
    public static string StringConexao
    {
        get
        {
            var solutionDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\.."));
            var dbPath = Path.Combine(solutionDir, "Infra", "_db", "dados.db");
            return $"Data Source={dbPath}";
        }
    }

}