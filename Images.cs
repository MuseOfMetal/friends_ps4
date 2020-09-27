using System;
using System.IO;
using Telegram.Bot.Types;
namespace Shop
{
    public static class Images
    {
        public static InputMediaPhoto MainMenu;
        public static FileStream MainMenuS;
        public static InputMediaPhoto GamesMenu;

        public static InputMediaPhoto FAQ;

        public static InputMediaPhoto HowGoingOrder;

        public static InputMediaPhoto P1;

        public static InputMediaPhoto P2;

        public static InputMediaPhoto HowEndOrder;

        public static InputMediaPhoto Help;

        public static void Load()
        {
            Console.WriteLine("HellO?");
            if (!Directory.Exists(@"images/BotImages"))
            {
                Console.WriteLine("lol chto");
                Directory.CreateDirectory(@"images/BotImages");
                return;
            }
            //using (FileStream stream = new FileStream(@"images/BotImages/Default.png", FileMode.Open))
            //{
            //    MainMenu = GamesMenu = FAQ = HowEndOrder = P1 = P2 = HowEndOrder = Help = new InputMediaPhoto(new InputMedia(stream, "Default"));
            //}

            Console.WriteLine("MainMenu");
            if (System.IO.File.Exists(@"images/BotImages/MainMenu.png"))
            {
                using (FileStream stream = new FileStream(@"images/BotImages/MainMenu.png", FileMode.Open))
                {
                    MainMenuS = stream;
                    MainMenu = new InputMediaPhoto(new InputMedia(stream, "MainMenu"));
                }
            }

            if (System.IO.File.Exists(@"images/BotImages/GamesMenu.png"))
            {
                using (FileStream stream = new FileStream(@"images/BotImages/GamesMenu.png", FileMode.Open))
                {
                    GamesMenu = new InputMediaPhoto(new InputMedia(stream, "GamesMenu"));
                }
            }

            if (System.IO.File.Exists(@"images/BotImages/FAQ.png"))
            {
                using (FileStream stream = new FileStream(@"images/BotImages/FAQ.png", FileMode.Open))
                {
                    FAQ = new InputMediaPhoto(new InputMedia(stream, "FAQ"));
                }
            }

            if (System.IO.File.Exists(@"images/BotImages/HowGoingOrder.png"))
            {
                using (FileStream stream = new FileStream(@"images/BotImages/HowGoingOrder.png", FileMode.Open))
                {
                    HowGoingOrder = new InputMediaPhoto(new InputMedia(stream, "HowGoingOrder"));
                }
            }


            if (System.IO.File.Exists(@"images/BotImages/P1.png"))
            {
                using (FileStream stream = new FileStream(@"images/BotImages/P1.png", FileMode.Open))
                {
                    P1 = new InputMediaPhoto(new InputMedia(stream, "P1"));
                }
            }

            if (System.IO.File.Exists(@"images/BotImages/P2.png"))
            {
                using (FileStream stream = new FileStream(@"images/BotImages/P2.png", FileMode.Open))
                {
                    P2 = new InputMediaPhoto(new InputMedia(stream, "P2"));
                }
            }


            if (System.IO.File.Exists(@"images/BotImages/HowEndOrder.png"))
            {
                using (FileStream stream = new FileStream(@"images/BotImages/HowEndOrder.png", FileMode.Open))
                {
                    HowEndOrder = new InputMediaPhoto(new InputMedia(stream, "HowEndOrder"));
                }
            }

            if (System.IO.File.Exists(@"images/BotImages/Help.png"))
            {
                using (FileStream stream = new FileStream(@"images/BotImages/Help.png", FileMode.Open))
                {
                    Help = new InputMediaPhoto(new InputMedia(stream, "Help"));
                }
            }
        }


    }
}
