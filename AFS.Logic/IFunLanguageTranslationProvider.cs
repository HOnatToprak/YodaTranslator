namespace AFS.Logic;

public interface IFunLanguageTranslationProvider
{
    public Task<string> Translate(FunLanguage language, String text);
}
