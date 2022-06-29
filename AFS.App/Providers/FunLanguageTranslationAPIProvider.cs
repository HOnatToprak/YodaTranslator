using AFS.Logic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AFS.Test")]
namespace AFS.App.Providers;

class FunLanguageTranslationAPIProvider : IFunLanguageTranslationProvider
{
    private readonly HttpClient _httpClient = new();
    public FunLanguageTranslationAPIProvider()
    {
        _httpClient.BaseAddress = new Uri("https://api.funtranslations.com/translate/");
    }
    public async Task<string> Translate(FunLanguage language, string text)
    {
        Dictionary<string, string> requestParams = new Dictionary<string, string>
        {
            {"text", text}
        };
        var response = await _httpClient.PostAsync(GetEndpoint(language), new FormUrlEncodedContent(requestParams));
        response.EnsureSuccessStatusCode();
        ResponseModel model = await response.Content.ReadFromJsonAsync<ResponseModel>();
        return model.Contents.Translated;
    }

    private string GetEndpoint(FunLanguage language) => language switch
    {
        FunLanguage.YODA => "yoda",
        _ => throw new ArgumentOutOfRangeException(nameof(language))
    };

    protected class ResponseModel
    {
        public Contents Contents { get; set; }
    }

    protected class Contents
    {
        public String Translated { get; set; }
    }
}




