using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WebSocket4Net;

// TODO: Figure out a way to do logging
// TODO: We need logic for mod close/destroy, so we can close the websocket connection gracefully

namespace TinyDiscord
{
    public sealed class DiscordClient
    {
        private WebSocket _websocketClient;

        public DiscordClient(string token)
        {
            //Log.Write("Setting up websocket client..");

            // Get Discord API websocket url
            var discordAPIResult = Tiny.CallAPI("/gateway");
            var discordAPIURL = discordAPIResult["url"];
           //Log.Write($"Discord API WebSocket URL: {discordAPIURL}");

            // TODO: Validate the API responses above..

            // Initialize the websocket client
            _websocketClient = new WebSocket($"{discordAPIURL}?v=6&encoding=json");

            // Setup the websocket event handlers
            //_websocketClient.Error += (sender, e) => Log.WriteError($"WebSocket Error: {sender}, {e}");
            //_websocketClient.Error += (sender, e) => Log.Write($"WebSocket Error: {e.Exception.Message}");
            _websocketClient.Opened += (sender, e) =>
            {
               //Log.Write($"WebSocket Opened");
                WebsocketReady();
            };
            //_websocketClient.Closed += (sender, e) => Log.Write($"WebSocket Closed");
            _websocketClient.MessageReceived += (sender, e) =>
            {
                var dynamicMessage = JsonConvert.DeserializeObject(e.Message);
                var message = JsonConvert.DeserializeObject<DiscordResponse>(e.Message);
                var formattedMessage = JsonConvert.SerializeObject(dynamicMessage, Formatting.Indented);

                //Log.Write($"WebSocket MessageReceived: {formattedMessage}");

                if (message.Operation == 10) // HELLO
                {
                    //Log.Write("Parsed as Hello message");

                    // FIXME: We need to send a "heartbeat response" every few seconds:
                    //        { "op": 1, "d": <last dispatch message received> }

                    WebsocketAuthenticate(token);
                }
                else if (message.Operation == 0) // DISPATCH
                {
                    //Log.Write("Parsed as Dispatch message");

                    if (message.Type == "MESSAGE_CREATE")
                    {
                        var user = message.Data.Author;
                        var username = !string.IsNullOrEmpty(user.Nickname) ? user.Nickname : user.Username;
                        //Log.Write($"{username} said {message.Data.Content}");
                    }
                }
                else
                {
                    //Log.Write("Parsed as other message");
                }
            };
            //_websocketClient.DataReceived += (sender, e) => Log.Write($"WebSocket DataReceived: {e.Data}");

            // TODO: What about disconnecting/reconnecting?
            // TODO: What about timeouts? Both when connecting and sending?

            // Enable automatic sending of ping messages
            _websocketClient.EnableAutoSendPing = true;
            _websocketClient.AutoSendPingInterval = 1;

            // Open the websocket connection
            //Log.Write("Connecting to websocket server..");
            _websocketClient.Open();
        }

        private void WebsocketReady()
        {
            /*// Send a test message
            Log.Write("Sending message to websocket server..");
            _websocketClient.Send("Dids says hi");*/
        }

        private void WebsocketAuthenticate(string token)
        {
            var authData = new DiscordResponse
            {
                Operation = 2,
                Data = new DiscordResponseData
                {
                    Token = token,
                    Properties = new Dictionary<string, string>(),
                    Compress = false,
                    LargeThreshold = 250
                }
            };

            var authDataString = JsonConvert.SerializeObject(authData);

            //Log.Write($"Sending auth data string: {authDataString}");

            _websocketClient.Send(authDataString);
        }

        public struct DiscordResponse
        {
            [JsonProperty("op")]
            public int Operation { get; set; }

            [JsonProperty("t")]
            public string Type { get; set; }

            [JsonProperty("d")]
            public DiscordResponseData Data { get; set; }
        }

        public struct DiscordResponseData
        {
            [JsonProperty("token")]
            public string Token { get; set; }

            [JsonProperty("properties")]
            public Dictionary<string, string> Properties { get; set; }

            [JsonProperty("compress")]
            public bool Compress { get; set; }

            [JsonProperty("large_threshold")]
            public int LargeThreshold { get; set; }

            [JsonProperty("user")]
            public DiscordResponseUser User { get; set; }

            [JsonProperty("author")]
            public DiscordResponseUser Author { get; set; }

            [JsonProperty("content")]
            public string Content { get; set; }
        }

        public struct DiscordResponseUser
        {
            [JsonProperty("username")]
            public string Username { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("discriminator")]
            public string Discriminator { get; set; }

            [JsonProperty("bot")]
            public bool Bot { get; set; }

            [JsonProperty("avatar")]
            public string Avatar { get; set; }

            [JsonProperty("roles")]
            public string[] Roles { get; set; }

            [JsonProperty("nick")]
            public string Nickname { get; set; }
        }
    }
}
