using Api.Contracts.Tests.Dto;
using Api.Contracts.Users;
using Refit;

namespace TG.Bot.Intagrations.BackendApi;

internal interface IBackendApi
{
    [Post("/users/login")]
    Task<IApiResponse<string>> LoginAsync(LoginRequest request);

    [Post("/link/tg")]
    Task<IApiResponse<string>> LinkTelegramAsync(LinkTelegramRequest request);

    [Get("/tests/{id}")]
    Task<IApiResponse<GetTestDto>> GetTestAsync(Guid id);

    [Get("/tests/NamesAndIds")]
    Task<IApiResponse<ICollection<GetTestNameAndIdDto>>> GetTestNamesAndIdsAsync();
}