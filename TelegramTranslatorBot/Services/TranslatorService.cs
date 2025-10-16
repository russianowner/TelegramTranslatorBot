using RestSharp;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace TelegramTranslatorBot.Services;

public class TranslatorService
{
    private readonly IConfiguration _config;

    public TranslatorService(IConfiguration config)
    {
        _config = config;
    }
    public async Task<string> TranslateAsync(string text, string targetLang, string sourceLang = "auto")
    {
        string? baseUrl = _config["TranslatorApi:BaseUrl"];
        if (string.IsNullOrWhiteSpace(baseUrl))
            throw new InvalidOperationException("юрл нету в аппсетинге");
        try
        {
            var libreResult = await TranslateLibreAsync(text, targetLang, sourceLang, baseUrl);
            if (!string.IsNullOrWhiteSpace(libreResult))
                return libreResult;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
        try
        {
            var memoryResult = await TranslateMyMemoryAsync(text, targetLang, sourceLang);
            if (!string.IsNullOrWhiteSpace(memoryResult))
                return memoryResult;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
        return "ни один сервис не ответил.";
    }
    private async Task<string?> TranslateLibreAsync(string text, string targetLang, string sourceLang, string baseUrl)
    {
        var client = new RestClient(baseUrl);
        var request = new RestRequest();
        request.AddHeader("Content-Type", "application/json");
        request.AddStringBody(
            $"{{\"q\": \"{text}\", \"source\": \"{sourceLang}\", \"target\": \"{targetLang}\"}}",
            DataFormat.Json);
        var response = await client.ExecutePostAsync(request);
        if (!response.IsSuccessful)
        {
            return null;
        }
        if (string.IsNullOrWhiteSpace(response.Content) || !response.Content.TrimStart().StartsWith("{"))
            return null;
        var json = JObject.Parse(response.Content!);
        return json["translatedText"]?.ToString();
    }
    private async Task<string?> TranslateMyMemoryAsync(string text, string targetLang, string sourceLang)
    {
        var client = new RestClient("https://api.mymemory.translated.net/get");
        var request = new RestRequest();
        request.AddParameter("q", text);
        request.AddParameter("langpair", $"{sourceLang}|{targetLang}");
        var response = await client.ExecuteGetAsync(request);
        if (!response.IsSuccessful) return null;
        var json = JObject.Parse(response.Content!);
        return json["responseData"]?["translatedText"]?.ToString();
    }
}
