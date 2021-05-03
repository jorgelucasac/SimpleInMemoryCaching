using System.Collections.Generic;
using System.Threading.Tasks;
using Estudos.App.WebApi.Services;
using Estudos.App.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estudos.App.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaisesController : Controller
    {
        private readonly IPaisService _paisService;

        public PaisesController(IPaisService paisService)
        {
            _paisService = paisService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaisViewModel>>> ObterTodos()
        {
            var paises = await _paisService.ObterTodos();
            return Ok(paises);
        }
    }
}