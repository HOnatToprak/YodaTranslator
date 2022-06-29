using AFS.App.Models;
using AFS.App.Providers;
using AFS.DatabaseModel;
using AFS.Logic;
using Microsoft.AspNetCore.Mvc;

namespace AFS.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationController : ControllerBase
    {
        private readonly IFunLanguageTranslationProvider _funLanguageTranslationProvider;
        private readonly AFSDatabaseContext _dbContext;
        private readonly ILogger<TranslationController> _logger;

        public TranslationController(
            IFunLanguageTranslationProvider funLanguageTranslationProvider, 
            AFSDatabaseContext dbContext,
            ILogger<TranslationController> logger)
        {
            _funLanguageTranslationProvider = funLanguageTranslationProvider;
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<TranslationResponse>> Get([FromQuery]string text)
        {
            _logger.LogDebug("{0} request with parameter {1} called.", nameof(Get), text);
            FunLanguageTranslator translator = new(_funLanguageTranslationProvider, _dbContext);
            try
            {
                string translation = await translator.Translate(FunLanguage.YODA, text);
                return Ok(new TranslationResponse()
                {
                    Text = text,
                    Translation = translation
                });
            }
            catch(HttpRequestException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    _logger.LogInformation(e, "Exceeded FunTranslations api call limit(5 req/h).");
                    return StatusCode(StatusCodes.Status429TooManyRequests, "Exceeded FunTranslations api call limit(5 req/h). Try again after an hour.");
                }
                
                throw;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Unexpected exception on method {0} with parameter {1}", nameof(Get), text);
                throw;
            }
        }
    }
}
