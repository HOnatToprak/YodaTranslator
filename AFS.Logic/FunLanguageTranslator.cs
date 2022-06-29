using AFS.DatabaseModel;

namespace AFS.Logic;

public class FunLanguageTranslator
{
    private readonly IFunLanguageTranslationProvider _funLanguageTranslationProvider;
    private readonly AFSDatabaseContext _afsDatabaseContext;

    public FunLanguageTranslator(IFunLanguageTranslationProvider funLanguageTranslationStore, AFSDatabaseContext afsDatabaseContext)
    {
        _funLanguageTranslationProvider = funLanguageTranslationStore;
        _afsDatabaseContext = afsDatabaseContext;
    }

    public async Task<string> Translate(FunLanguage funLanguage, string text)

    {
        if(!Enum.IsDefined(funLanguage))
        {
            throw new ArgumentException("Enum is not valid", nameof(funLanguage));
        }

        string translation = await _funLanguageTranslationProvider.Translate(funLanguage, text);
        await StoreTranslation(funLanguage, text, translation);
        return translation;
    }

    private async Task StoreTranslation(FunLanguage funLanguage, string text, string translation)
    {
        _afsDatabaseContext.Translations.Add(new Translation
        {
            FunLanguage = Enum.GetName(funLanguage)!,
            InputText = text,
            TranslatedText = translation
        });
        await _afsDatabaseContext.SaveChangesAsync();
    }
}