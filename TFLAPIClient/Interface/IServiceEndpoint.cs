using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TflApiClient.DTO;

namespace TflApiClient.Interface
{
    public interface IServiceEndpoint<T>
    {
        Task<(T, ApiError)> Get(string queryString);
    }
}