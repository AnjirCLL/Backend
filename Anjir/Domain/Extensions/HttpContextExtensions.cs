using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extensions;

public static  class HttpContextExtensions
{
    public static string GetUserAgent(this HttpContext context)
    {
        //return context.Request.Headers.UserAgent.FirstOrDefault() ?? "";  
        return null;
    }

    public static string GetIPAddress(this HttpContext context)
    {
        string ip = (context.Connection.RemoteIpAddress?.ToString() ?? "").Replace("::ffff:", "");
        var _ip = context.Request.Headers["CF-Connecting-IP"].FirstOrDefault();

        if (!string.IsNullOrEmpty(_ip))
        {
            var _ip2 = string.Join(",", _ip.Split(",").Select(x => x.Replace("::ffff:", "").Trim()).Distinct());
            if (!ip.Equals(_ip2))
                ip = $"{ip},{_ip2}";
        }

        return ip;
    }
}
