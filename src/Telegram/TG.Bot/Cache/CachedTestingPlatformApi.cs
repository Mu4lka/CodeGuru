using Microsoft.Extensions.Caching.Memory;
using Refit;
using TestingPlatform.Api.Contracts.Dto;
using TG.Bot.CacheServices.Base;
using TG.Bot.Intagrations.TestingPlatformApi;

namespace TG.Bot.CacheServices;

internal class CachedTestingPlatformApi(
    ITestingPlatformApi _testingPlatformApi,
    IMemoryCache _cache) : ICachedTestingPlatformApi
{
    public async Task<IApiResponse<ICollection<GetTestNameAndIdDto>>> GetTestNamesAndIdsAsync()
    {
        if (_cache.TryGetValue(CreateKey(), out IApiResponse<ICollection<GetTestNameAndIdDto>>? binding))
            return binding!;

        var responce = await _testingPlatformApi.GetTestNamesAndIdsAsync();
        _cache.Set(CreateKey(), responce, new TimeSpan(0, 1, 0));
        return responce;

        static string CreateKey() => $"test-names-and-ids";
    }
}
