using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Estudos.App.WebApi.Services;
using Estudos.App.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Estudos.App.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaisesController : Controller
    {
        private readonly IPaisService _paisService;
        private readonly IMemoryCache _memoryCache;
        private const string ObterTodosKey = "PaisesController.ObterTodos";

        public PaisesController(IPaisService paisService, IMemoryCache memoryCache)
        {
            _paisService = paisService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaisViewModel>>> ObterTodos()
        {
            if (_memoryCache.TryGetValue(ObterTodosKey, out IEnumerable<PaisViewModel> paises))
            {
                return Ok(paises);
            }

            paises = await _paisService.ObterTodos();
            SetCache(ObterTodosKey, paises);
            
            return Ok(paises);
        }

        #region Privates

        private void SetCache(string key, object value)
        {
            var memoryCacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                SlidingExpiration = TimeSpan.FromSeconds(1800)
            };
            _memoryCache.Set(key, value, memoryCacheEntryOptions);
        }


        #endregion
    }
}