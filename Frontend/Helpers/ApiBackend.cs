using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using DTO.Produtos;
using System.Net.Http.Json;

namespace Frontend.Helpers;

public static class ApiBackend
{
    public static IJSRuntime? JsRuntime { get; set; }

    static ApiBackend()
    {
        UrlBase = "http://localhost:5273/";
    }

    public static async Task<T?> GetAsync<T>(string complementoUrl, string? token = null)
    {
        var urlCompleta = UrlBase + complementoUrl;
        Console.WriteLine($"[ApiBackend] Iniciando GET para: {urlCompleta}");

        using var httpClient = new HttpClient();

        if (token != null)
        {
            Console.WriteLine("[ApiBackend] Token fornecido, adicionando header Authorization.");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
        else
        {
            Console.WriteLine("[ApiBackend] Nenhum token fornecido.");
        }

        try
        {
            Console.WriteLine("[ApiBackend] Enviando requisição GET...");
            var resultado = await httpClient.GetAsync(urlCompleta);

            Console.WriteLine($"[ApiBackend] Código de status da resposta: {resultado.StatusCode}");

            if (!resultado.IsSuccessStatusCode)
            {
                var conteudoErro = await resultado.Content.ReadAsStringAsync();
                Console.WriteLine($"[ApiBackend] Erro na chamada da API: {resultado.StatusCode}, Conteúdo: {conteudoErro}");
                throw new Exception($"Erro ao chamar a API: {resultado.StatusCode} - {conteudoErro}");
            }

            var result = await resultado.Content.ReadAsStringAsync();
            Console.WriteLine($"[ApiBackend] Resposta recebida: {result}");

            var objetoDesserializado = JsonSerializer.Deserialize<T>(result, Options);
            Console.WriteLine("[ApiBackend] Desserialização concluída com sucesso.");

            return objetoDesserializado;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ApiBackend] Exceção ao chamar GET: {ex.Message}");
            throw;
        }
    }


    public static async Task<T?> PostAsync<T, K>(string complementoUrl, K objeto, string? token = null)
    {
        var urlCompleta = UrlBase + complementoUrl;
        Console.WriteLine($"[ApiBackend] POST para: {urlCompleta}");
        Console.WriteLine($"[ApiBackend] Corpo enviado: {JsonSerializer.Serialize(objeto)}");

        var httpClient = new HttpClient();

        if (token != null)
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var conteudo = new StringContent(
            JsonSerializer.Serialize(objeto, Options),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        var result = await httpClient.PostAsync(urlCompleta, conteudo);

        Console.WriteLine($"[ApiBackend] Status da resposta: {result.StatusCode}");

        if (!result.IsSuccessStatusCode)
        {
            var errorContent = await result.Content.ReadAsStringAsync();
            Console.WriteLine($"[ApiBackend] Erro ao chamar a API: {result.StatusCode} - {errorContent}");
            throw new Exception($"Erro ao chamar a API: {result.StatusCode}");
        }

        var resultContent = await result.Content.ReadAsStringAsync();
        Console.WriteLine($"[ApiBackend] Conteúdo da resposta: {resultContent}");

        httpClient.Dispose();

        if (string.IsNullOrEmpty(resultContent))
            return default;

        return JsonSerializer.Deserialize<T>(resultContent, Options);
    }

    public static async Task<T?> PostAsync<T>(string complementoUrl, T objeto, string? token = null)
    {
        var urlCompleta = UrlBase + complementoUrl;
        Console.WriteLine($"[ApiBackend] POST para: {urlCompleta}");
        Console.WriteLine($"[ApiBackend] Dados enviados: {System.Text.Json.JsonSerializer.Serialize(objeto)}");

        if (JsRuntime != null)
        {
            await JsRuntime.InvokeVoidAsync("console.log", $"[ApiBackend] POST para: {urlCompleta}");
            await JsRuntime.InvokeVoidAsync("console.log", $"[ApiBackend] Dados enviados: {System.Text.Json.JsonSerializer.Serialize(objeto)}");
        }

        var httpClient = new HttpClient();

        if (token != null)
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var conteudo = new StringContent(
            JsonSerializer.Serialize(objeto, Options),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        var result = await httpClient.PostAsync(urlCompleta, conteudo);

        if (!result.IsSuccessStatusCode)
            throw new Exception($"Erro ao chamar a API: {result.StatusCode}");

        var resultContent = await result.Content.ReadAsStringAsync();

        httpClient.Dispose();

        if (string.IsNullOrEmpty(resultContent))
            return default;

        return JsonSerializer.Deserialize<T>(resultContent, Options);
    }

    public static async Task PutAsync<T>(string complementoUrl, T objeto, string? token = null)
    {
        var urlCompleta = UrlBase + complementoUrl;

        var httpClient = new HttpClient();

        if (token != null)
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var conteudo = new StringContent(
            JsonSerializer.Serialize(objeto, Options),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        var result = await httpClient.PutAsync(urlCompleta, conteudo);

        if (!result.IsSuccessStatusCode)
            throw new Exception($"Erro ao chamar a API: {result.StatusCode}");

        httpClient.Dispose();
    }

    public static async Task DeleteAsync(string complementoUrl, string? token = null)
    {
        var urlCompleta = UrlBase + complementoUrl;

        var httpClient = new HttpClient();

        if (token != null)
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var result = await httpClient.DeleteAsync(urlCompleta);

        if (!result.IsSuccessStatusCode)
            throw new Exception($"Erro ao chamar a API: {result.StatusCode}");

        httpClient.Dispose();
    }
    
    private static JsonSerializerOptions Options { get; set; } = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    private static async Task<List<ProdutoDTO>> ObterProdutosAsync(string complementoUrl)
    {
        var urlCompleta = UrlBase + complementoUrl;
        var httpClient = new HttpClient();

        var result = await httpClient.GetFromJsonAsync<List<ProdutoDTO>>(urlCompleta);

        if (result == null)
            return new List<ProdutoDTO>();

        return result;
    }

    private static string UrlBase { get; set; } = null!;
}
