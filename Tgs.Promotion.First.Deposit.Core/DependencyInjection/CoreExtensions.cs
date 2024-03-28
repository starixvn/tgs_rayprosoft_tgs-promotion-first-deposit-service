using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Tgs.Promotion.First.Deposit.Core.DependencyInjection;

public static class CoreExtensions
{
    public static IServiceCollection AddPromotionFirstDepositCore(this IServiceCollection services)
    {
        services
            .RegisterAllServiceUtilityTypes(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);

        return services;
    }
}