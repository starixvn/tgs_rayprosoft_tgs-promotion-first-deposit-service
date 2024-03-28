using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Tgs.Promotion.First.Deposit.Core;
using Tgs.Promotion.First.Deposit.Repository.Entities;

namespace Tgs.Promotion.First.Deposit.Repository.DependencyInjection;

public static class TgsRepositoryExtensions
{
    public static IServiceCollection AddFirstDepositRepositories(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> dbConfigure,
        Func<IServiceProvider, IDatabase> resolveRedisDatabase)
        => services
            .AddDbContext<TgsContext>(dbConfigure)
            .AddScoped<ITgsRepository, TgsRepository>();
}