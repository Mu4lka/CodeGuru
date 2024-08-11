using Refit;
using TestingPlatform.Api.Contracts.Dto;

namespace TG.Bot.CacheServices.Base;

internal interface ICachedTestingPlatformApi
{
    Task<IApiResponse<ICollection<GetTestNameAndIdDto>>> GetTestNamesAndIdsAsync();
}
