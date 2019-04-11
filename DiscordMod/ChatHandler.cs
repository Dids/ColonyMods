// FIXME: This seems to require a major refactor for the 0.7 branch

//using System;
//using ChatCommands;
//using Pipliz;
//using Pipliz.Chatting;

//namespace Dids.Discord
//{
//    [ModLoader.ModManager]
//    public static class ChatHandler
//    {
//        [ModLoader.ModCallback(ModLoader.EModCallbackType.AfterItemTypesDefined, "Dids.Discord.AfterItemTypesDefined")]
//        public static void AfterItemTypesDefined()
//        {
//            Log.Write("Registering ChatHandlerCommand..");
//            CommandManager.RegisterCommand(new ChatHandlerCommand());
//            Log.Write("ChatHandlerCommand registered!");

//            Chat.ReceiveServer("");
//        }
//    }

//    public class ChatHandlerCommand : IChatCommand
//    {
//        #region IChatCommand
//        public bool IsCommand(string chat)
//        {
//            var isRealCommand = chat.StartsWith("/", StringComparison.InvariantCulture);
//            Log.Write($"isRealCommand? {chat} -> {isRealCommand}");

//            return true;
//            //return !chat.StartsWith("/", StringComparison.InvariantCulture);
//        }

//        public bool TryDoCommand(Players.Player player, string chat)
//        {
//            // TODO: Relay messages to Discord
//            // TODO: Intercept messages from Discord and send to the game
//            // TODO: Add support for a config file to setup Discord tokens etc.

//            Log.Write($"ChatHandler -> {player.Name}: {chat}");
//            Chat.Send(player, $"You said: {chat}");

//            using (var client = new HttpClient())
//            {
//                client.BaseAddress = new Uri(apiBasicUri);
//                var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json");
//                var result = await client.PostAsync(url, content);
//                result.EnsureSuccessStatusCode();
//            }

//            // Chat.SendToAll (string text, ChatSenderType type = ChatSenderType.Server);

//            return true;
//        }
//        #endregion
//    }
//}
