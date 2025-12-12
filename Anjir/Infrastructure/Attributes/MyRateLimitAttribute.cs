using Application.IServices.RateLimitIg;
using Domain.BaseAnswer;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Infrastructure.Attributes;

public class MyRateLimitAttribute : TypeFilterAttribute
{
    public MyRateLimitAttribute(StatisticActionEnum statisticActionEnum, int maxCountOfUse = 5) : base(typeof(MyRateLimitActionFilter))
    {
        Arguments = [statisticActionEnum, maxCountOfUse];
    }

    private class MyRateLimitActionFilter(IStatisticsService statisticsService, IRateLimitIgnoreService rateLimitIgnoreService, StatisticActionEnum statisticActionEnum, int maxCountOfUse) : IAsyncActionFilter
    {
        private readonly StatisticActionEnum statisticActionEnum = statisticActionEnum;
        private readonly IStatisticsService statisticsService = statisticsService;
        private readonly IRateLimitIgnoreService rateLimitIgnoreService = rateLimitIgnoreService;
        private readonly int maxCountOfUse = maxCountOfUse;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (Guid.TryParse(context.HttpContext.Request.Headers["rli"], out Guid rateLimitIgnoreId))
            {
                var _rliRes = await rateLimitIgnoreService.CheckAsync(rateLimitIgnoreId);
                if (_rliRes?.Data == true)
                    await next();
            }

            var _res = await statisticsService.CheckCountAsync(statisticActionEnum, context.HttpContext, maxCountOfUse);
            if (_res?.Data != true)
            {
                context.Result = new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new BaseAnswer<bool?>
                    {
                        Succeeded = false,
                        Messages = ["Too many requests. Try again later"]
                    },
                  new JsonSerializerSettings
                  {
                      ContractResolver = new CamelCasePropertyNamesContractResolver()
                  }),
                    StatusCode = StatusCodes.Status200OK,
                    ContentType = "application/json; charset=utf-8"
                };
                return;
            }

            await next();
            return;
        }
    }
}
