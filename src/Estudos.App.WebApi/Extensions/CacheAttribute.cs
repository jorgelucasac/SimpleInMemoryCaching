using System;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Estudos.App.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Estudos.App.WebApi.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _tempoDuracaoCache;
        private readonly IResponseCacheService _responseCacheService;

        public CacheAttribute(int tempoDuracaoCache, IResponseCacheService memoryCache)
        {
            _tempoDuracaoCache = tempoDuracaoCache;
            _responseCacheService = memoryCache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var chaveCache = GerarChaveCache(context.HttpContext.Request);
            var cache = _responseCacheService.ObterCache(chaveCache);

            if (cache != null)
            {
                var result = GerarContentResult(cache);
                context.Result = result;
                return;
            }

            var contextoExecutado = await next();

            if (contextoExecutado.Result is OkObjectResult okObjectResult)
            {
                _responseCacheService.SalvarCache(chaveCache, okObjectResult.Value, _tempoDuracaoCache);
            }
        }


        #region privates
        
        private string GerarChaveCache(HttpRequest request)
        {
            var chaveBuider = new StringBuilder();

            chaveBuider.Append(request.Path.ToString());

            foreach (var (key, value) in request.Query.OrderBy(p => p.Key))
            {
                chaveBuider.Append($"{key}-{value}");
            }

            return chaveBuider.ToString();
        }

        private ContentResult GerarContentResult(object dado)
        {
            return new ContentResult
            {
                Content = JsonSerializer.Serialize(dado),
                ContentType = MediaTypeNames.Application.Json,
                StatusCode = StatusCodes.Status200OK
            };
        }

        #endregion

    }
}