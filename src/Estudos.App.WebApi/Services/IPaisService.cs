using System.Collections.Generic;
using System.Threading.Tasks;
using Estudos.App.WebApi.ViewModels;

namespace Estudos.App.WebApi.Services
{
    public interface IPaisService
    {
        Task<IEnumerable<PaisViewModel>> ObterTodos();
    }
}