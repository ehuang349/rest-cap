using Microsoft.EntityFrameworkCore;
using rest_cap.Server.Entities;

namespace rest_cap.Server.Helpers
{
    public interface IApiKeyValidator
    {
        Task<bool> IsApiKeyValidAsync(string? apiKey);
    }
    public class ApiKeyValidatorHelper : IApiKeyValidator
    {
        private readonly StorageContext _storageContext;
        public ApiKeyValidatorHelper(StorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public async Task<bool> IsApiKeyValidAsync(string? apiKey)
        {
            if (String.IsNullOrEmpty(apiKey))
            {
                return false;
            }

            return await _storageContext.ApiAccess.AnyAsync(a => a.ApiKey == apiKey && a.IsActive);
        }
    }
}
