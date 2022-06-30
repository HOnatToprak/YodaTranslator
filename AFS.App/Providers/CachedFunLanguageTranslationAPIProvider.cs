using AFS.Logic;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AFS.Test")]
namespace AFS.App.Providers;

class CachedFunLanguageTranslationAPIProvider : IFunLanguageTranslationProvider
{
    private readonly FunLanguageTranslationAPIProvider _funLanguageTranslationAPIProvider;
    private readonly IMemoryCache _memoryCache;
    public CachedFunLanguageTranslationAPIProvider(FunLanguageTranslationAPIProvider funLanguageTranslationAPIProvider, IMemoryCache memoryCache)
    {
        _funLanguageTranslationAPIProvider = funLanguageTranslationAPIProvider;
        _memoryCache = memoryCache;
    }
    public async Task<string> Translate(FunLanguage language, string text)
    {
        // Maybe hashing the key is better?
        string cacheKey = String.Format("Translation_{0}_{1}", Enum.GetName(language), text);
        return await _memoryCache.GetOrCreateAsync(cacheKey, async cacheEntry =>
        {
            cacheEntry.SetSlidingExpiration(TimeSpan.FromHours(1));
            return await _funLanguageTranslationAPIProvider.Translate(language, text);
        });
    }
}




