using System.Collections.Generic;
using Telegram.Bot;
namespace Shop
{
    sealed class Program
    {
        public static List<Models.User> Users;
        public static List<Models.Product> Products;
        public static TelegramBotClient Bot;
        static void Main(string[] args)
        {
            Config.Get();
            Config.Save();

            Bot = new TelegramBotClient(Config.Get().BotToken);
            Users = new List<Models.User>();
            Products = new List<Models.Product>();
            Models.Product.Load();
            Models.User.Load();
            Texts.Load();

            Bot.OnMessage += Message.MessageController.Bot_OnMessage;
            Bot.OnCallbackQuery += Callback.CallbackController.Bot_OnCallbackquery;

            Bot.StartReceiving();

            while (true)
            {
                System.Threading.Thread.Sleep(120000);
                Models.User.Save();
            }
        }


    }
}
