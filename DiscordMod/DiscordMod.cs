using System;
using System.IO;

using Pipliz;

using TinyDiscord;

namespace Dids.Discord
{
    [ModLoader.ModManager]
    public static class DiscordMod
    {
        private static DiscordClient _discord;

        //public const string ModNamespace = @"Dids.Discord";
        public static string ModFolder;// = $@"gamedata\mods\Dids\Discord";

        [ModLoader.ModCallback(ModLoader.EModCallbackType.OnAssemblyLoaded, "Dids.Discord.OnAssemblyLoaded")]
        public static void OnAssemblyLoaded (string path)
        {
            Log.Write("Trying to find Discord Mod..");
            ModFolder = Path.GetDirectoryName(path);
            Log.Write("Found Discord Mod at {0}", ModFolder);

            try
            {
                _discord = Tiny.New(Environment.GetEnvironmentVariable("token"));
            }
            catch (Exception ex)
            {
                Log.WriteError($"Failed to initialize websocket client: {ex.Message}");
            }
        }

        //[ModLoader.ModCallback(ModLoader.EModCallbackType.AfterModsLoaded, "Dids.Discord.AfterModsLoaded")]
        ////public static void AfterModsLoaded(List<ModLoader.ModAssembly> assemblies)
        //public static void AfterModsLoaded(List<object> assemblies)
        //{
        //    Log.Write("Loading Discord Mod by Dids..");

        //    try
        //    {
        //        // Initialize the websocket client
        //        _websocketClient = new WebSocketClient<WebSocketMessagingService>("ws://demos.kaazing.com/echo");
        //        //_websocketClient = new WebSocketClient<WebSocketMessagingService>("wss://echo.websocket.org"); // TODO: Also test wss:// support!

        //        // Setup the websocket event handlers
        //        _websocketClient.Error += (sender, e) => Log.WriteError($"WebSocket Error: {sender}, {e}");
        //        _websocketClient.Closed += (sender, e) => Log.Write($"WebSocket Closed: {sender}, {e}");
        //        _websocketClient.Handshake += (sender, args) => Log.Write($"WebSocket Handshake: {sender}, {args}");
        //        _websocketClient.Ping += (sender, args) => Log.Write($"WebSocket Ping: {sender}, {args}");
        //        _websocketClient.Pong += (sender, args) => Log.Write($"WebSocket Pong: {sender}, {args}");

        //        // Open the websocket connection
        //        _websocketClient.Open();

        //        // Send a test message
        //        _websocketClient.Send("Dids says hi");

        //        // TODO: How do we handle receiving messages?
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteError($"Failed to initialize websocket client: {ex.Message}");
        //    }

        //    Log.Write("Loaded Discord Mod by Dids!");
        //}
    }
}
