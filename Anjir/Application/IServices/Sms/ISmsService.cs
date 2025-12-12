using Domain.BaseAnswer;
using Domain.Enums;

namespace Application.IServices.Sms;

public interface ISmsService
{
    ValueTask<BaseAnswer<bool>> SendAsync(string phoneNumber, string message, SmsTypeEnum type, string? data = null);
}

