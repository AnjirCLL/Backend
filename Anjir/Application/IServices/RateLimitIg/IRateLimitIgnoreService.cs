using Domain.BaseAnswer;

namespace Application.IServices.RateLimitIg;

public interface IRateLimitIgnoreService
{
    ValueTask<BaseAnswer<Guid>> CreateAsync(string domain);
    ValueTask<BaseAnswer<bool>> CheckAsync(Guid guid);
}
