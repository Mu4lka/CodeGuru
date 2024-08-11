using Microsoft.Extensions.DependencyInjection;
using TG.Bot.CacheServices.Base;
using TG.Bot.Common;

namespace TG.Bot.CacheServices;

internal static class Entry
{
    public static IServiceCollection AddCacheSrevices(this IServiceCollection services)
    {
        services
            .AddTransient<ICachedBackendApi, CachedBackendApi>()
            .AddTransient<ICachedTestingPlatformApi, CachedTestingPlatformApi>();

        return services;
    }
}