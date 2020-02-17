using Community.Utils.Models;
using System.Threading.Tasks;

namespace Community.Utils.Interfaces
{
    public interface IApiService
    {
        Task<bool> CheckConnection(string url);

        Task<Response> DeleteAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            int id,
            string tokenType,
            string accessToken);

        Task<Response> GetListAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller);

        Task<Response> GetListAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken);

        Task<Response> GetTokenAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            TokenRequest request);

        Task<Response> PostAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            T model,
            string tokenType,
            string accessToken);

        Task<Response> PutAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            int id,
            T model,
            string tokenType,
            string accessToken);
    }
}
