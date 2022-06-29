namespace AFS.Test.Integration;
using AFS.App.Providers;

public class FunLanguageTranslationAPIProviderTests
{
    [Fact]
    public async Task Translate_Text_NotThrows()
    {
        FunLanguageTranslationAPIProvider provider = new();
        await provider.Translate(Logic.FunLanguage.YODA, "Hello, how are you?");
    }
}