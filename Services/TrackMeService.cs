using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using POC.Tracking.Models;

namespace POC.Tracking.Services
{
    public class TrackMeService : ITrackMeService
    {
        private readonly HttpContext _context;

        public TrackMeService(IHttpContextAccessor accessor)
        {
            _context = accessor.HttpContext;
        }

        public TrackMe TrackMe(dynamic location)
        {
            TrackMe trackMe = new() 
            {
                Ip = GetIpAddress(),
                UserAgent = GetUserAgent(),
                Location = location
            };
           
            return trackMe;
        }
        
        // adapted from: https://stackoverflow.com/a/36316189
        private string GetIpAddress()
        {
            var ipAddress = GetXForwardedForHeader();

            if(string.IsNullOrEmpty(ipAddress) && _context.Connection.RemoteIpAddress != null)
                ipAddress = _context.Connection.RemoteIpAddress?.ToString();

            return ipAddress;
        }

        private string GetUserAgent() 
            => _context.Request.Headers[HeaderNames.UserAgent];

        // https://en.wikipedia.org/wiki/X-Forwarded-For
        private string GetXForwardedForHeader()
        {
            if(!_context.Request.Headers.TryGetValue("X-Forwarded-For", out var value))
                return default;
            
            if(string.IsNullOrWhiteSpace(value))
                return default;

            return value
                .ToString()
                .TrimEnd(',')
                .Split(',')
                .Select(s => s.Trim())
                .FirstOrDefault();
        }
    }
}