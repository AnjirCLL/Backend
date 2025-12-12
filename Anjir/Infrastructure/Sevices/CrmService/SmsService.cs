using Application.IServices.Sms;
using Domain.BaseAnswer;
using Domain.Entities.CRM;
using Domain.Enums;
using Domain.Setting;
using Infrastructure.MyData;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Text;

namespace Infrastructure.Sevices.CrmService;

public class SmsService(MyDbContext db) : ISmsService
{
    private readonly MyDbContext db = db;
    public async ValueTask<BaseAnswer<bool>> SendAsync(string phoneNumber, string message, SmsTypeEnum type, string? data = null)
    {
        Sms sms = new()
        {
            Id = Guid.NewGuid(),
            PhoneNumber = phoneNumber,
            Message = message,
            Type = type,
            Data = data,
        };

        SendSmsRequest smsRequest = new()
        {
            Messages = [
            new() {
          Recipient = phoneNumber,
          MessageId = sms.Id.ToString(),
          Sms = new() {
            Originator = Settings.SmsConfig.Originator,
            Content = new() { Text = message }
          }
        }
          ]
        };

        HttpRequestMessage requestMessage = new(HttpMethod.Post, "");
        string authenticationString = $"{Settings.SmsConfig.Login}:{Settings.SmsConfig.Password}";
        string base64Auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64Auth);
        var jsonData = JsonConvert.SerializeObject(smsRequest, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

        requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpClient httpClient = new() { BaseAddress = new Uri(Settings.SmsConfig.SendUrl) };
        var _res = await httpClient.SendAsync(requestMessage);
        _res.EnsureSuccessStatusCode();
        await _res.Content.ReadAsStringAsync();

        await db.AddAsync(sms);
        await db.SaveChangesAsync();

        return new() { Data = true, Succeeded = true };
    }

    private class SendSmsRequest
    {
        public List<MessageModel> Messages { get; set; } = [];
        public class MessageModel
        {
            public string Recipient { get; set; } = string.Empty;

            [JsonProperty("message-id")]
            public string MessageId { get; set; } = string.Empty;

            public SmsModel Sms { get; set; } = default!;

            public class SmsModel
            {
                public string Originator = string.Empty;
                public ContentModel Content { get; set; } = default!;

                public class ContentModel
                {
                    public string Text { get; set; } = string.Empty;
                }
            }
        }
    }

}
