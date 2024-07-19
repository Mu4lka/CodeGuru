using Api.Contracts.Links;
using Api.Contracts.Tests.Dto;
using Api.Contracts.Users;
using Refit;

namespace TG.Bot.Intagrations.BackendApi;

internal interface IBackendApi
{
    [Post("/users/login")]
    Task<IApiResponse<string>> LoginAsync(LoginRequest request);

    [Post("/api/link/tg")]
    Task<IApiResponse<string>> LinkTelegramAsync(LinkTelegramRequest request);

    [Get("/api/tests/{id}")]
    Task<IApiResponse<GetTestDto>> GetTestAsync(Guid id);

    [Get("/api/tests/NamesAndIds")]
    Task<IApiResponse<ICollection<GetTestNameAndIdDto>>> GetTestNamesAndIdsAsync();
}