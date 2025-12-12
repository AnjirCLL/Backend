using Domain.BaseAnswer;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.IServices.RateLimitIg;

public interface IStatisticsService
{
    ValueTask SetAsync<T>(StatisticActionEnum action, T? data, DeviceTypeEnum deviceType, HttpContext httpContext);
    ValueTask<BaseAnswer<bool>> CheckCountAsync(StatisticActionEnum action, HttpContext httpContext, int maxCountOfUse);
}
