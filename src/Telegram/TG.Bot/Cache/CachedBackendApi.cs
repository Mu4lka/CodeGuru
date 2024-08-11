using Api.Contracts.AccountBindings;
using Microsoft.Extensions.Caching.Memory;
using Refit;
using TG.Bot.CacheServices.Base;
using TG.Bot.Intagrations.BackendApi;

namespace TG.Bot.Common;

internal class CachedBackendApi(
    IBackendApi _backendApi,
    IMemoryCache _cache) : ICachedBackendApi
{
    public async Task<IApiResponse<GetTelegramAccountBindingResponse>> GetTelegramAccountBindingAsync(long id)
    {
        if (_cache.TryGetValue(CreateKey(), out IApiResponse<GetTelegramAccountBindingResponse>? binding))
            return binding!;

        var responce = await _backendApi.GetTelegramAccountBindingAsync(id);
        _cache.Set(CreateKey(), responce, new TimeSpan(0, 0, 15));
        return responce;

        string CreateKey() => $"account-binding:telegram:{id}";
    }
}
