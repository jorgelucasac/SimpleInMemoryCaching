using System;

namespace Estudos.App.WebApi.Services.Interfaces
{
    public interface IResponseCacheService
    {
        void SalvarCache(string key, object value, int tempoDuracaoCache);
        object ObterCache(string key);

    }
}