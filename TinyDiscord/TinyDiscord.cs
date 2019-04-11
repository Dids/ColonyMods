using System;
using System.Collections.Generic;
using System.Net;

using Newtonsoft.Json;

using WebSocket4Net;

namespace TinyDiscord
{
    public static class Tiny
    {
        public const string APIBaseURL = "https://discordapp.com/api";

        public static Dictionary<string, string> CallAPI(string endpoint)
        {
            // Validate endpoint
            if (string.IsNullOrEmpty(endpoint))
                throw new Exception("Cannot call Discord API without a valid endpoint");

            // Add forward slash if missing
            endpoint = endpoint.StartsWith("/", StringComparison.InvariantCulture) ? endpoint : "/" + endpoint;

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(client.DownloadString(new Uri($"{APIBaseURL}{endpoint}")));
            }
        }

        public static DiscordClient New(string token)
        {
            return new DiscordClient(token);
        }
    }
}
