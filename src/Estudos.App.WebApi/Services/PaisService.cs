using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Estudos.App.WebApi.Extensions;
using Estudos.App.WebApi.ViewModels;
using Microsoft.Extensions.Options;

namespace Estudos.App.WebApi.Services
{
    public class PaisService : Service, IPaisService
    {
        private readonly HttpClient _httpClient;

        public PaisService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(appSettings.Value.ApiPaisesUrl);
        }

        public async Task<IEnumerable<PaisViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync("/rest/v2/all");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<PaisViewModel>>(response);
        }
    }
}