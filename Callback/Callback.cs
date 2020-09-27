using Telegram.Bot.Args;
using System.IO;
using Telegram.Bot.Types;
namespace Shop.Callback
{
    public static class CallbackController
    {
        public static async void Bot_OnCallbackquery(object sender, CallbackQueryEventArgs e)
        {

            var CallbackFrom = e.CallbackQuery.From;
            var CB = e.CallbackQuery;
            int UserIndex = Models.User.FindUser(Program.Users, e.CallbackQuery.From.Id);

            if (UserIndex == -1)
            {
                Program.Users.Add(new Models.User(CallbackFrom.FirstName, CallbackFrom.LastName, CallbackFrom.Username, CallbackFrom.Id));
                UserIndex = Models.User.FindUser(Program.Users, e.CallbackQuery.From.Id);
            }
            Program.Users[UserIndex].Update(CallbackFrom.FirstName, CallbackFrom.LastName, CallbackFrom.Username);
            var User = Program.Users[UserIndex];
            try
            {
                System.Console.WriteLine($"{User.FirstName} {User.LastName} @{User.UserName} callback: [{CB.Data.Replace('&', ' ')}] with [{User.GetPermissionLevel()}] level permission");
                if (User.GetPermissionLevel() != -1)
                {
                    string[] args = e.CallbackQuery.Data.Split('&');
                    switch (args[0])
                    {
                        case "faq":
                            if (args[1] == "main")
                                using (FileStream stream = new FileStream("images/BotImages/FAQ.png", FileMode.Open))
                                {
                                    InputMedia media = new InputMedia(stream, "FAQ.png");
                                    InputMediaPhoto photo = new InputMediaPhoto(media);

                                    photo.Caption = "FAQ";

                                    await Program.Bot.EditMessageMediaAsync(CB.Message.Chat.Id, CB.Message.MessageId, photo, replyMarkup: Keyboards.Texts("faq"));
                                }
                            if (args[1] == "howgoingorder")
                                using (FileStream stream = new FileStream("images/BotImages/HowGoingOrder.png", FileMode.Open))
                                {
                                    InputMedia media = new InputMedia(stream, "HowGoingOrder.png");
                                    InputMediaPhoto photo = new InputMediaPhoto(media);

                                    photo.Caption = Texts.Get().TextHowGoingOrder;

                                    await Program.Bot.EditMessageMediaAsync(CB.Message.Chat.Id, CB.Message.MessageId, photo, replyMarkup: Keyboards.BackToFaq);
                                }
                            if (args[1] == "p2")
                                using (FileStream stream = new FileStream("images/BotImages/P2.png", FileMode.Open))
                                {
                                    InputMedia media = new InputMedia(stream, "P2.png");
                                    InputMediaPhoto photo = new InputMediaPhoto(media);

                                    photo.Caption = Texts.Get().TextP2;

                                    await Program.Bot.EditMessageMediaAsync(CB.Message.Chat.Id, CB.Message.MessageId, photo, replyMarkup: Keyboards.BackToFaq);
                                }
                            if (args[1] == "p3")
                                using (FileStream stream = new FileStream("images/BotImages/P3.png", FileMode.Open))
                                {
                                    InputMedia media = new InputMedia(stream, "P3.png");
                                    InputMediaPhoto photo = new InputMediaPhoto(media);

                                    photo.Caption = Texts.Get().TextP3;

                                    await Program.Bot.EditMessageMediaAsync(CB.Message.Chat.Id, CB.Message.MessageId, photo, replyMarkup: Keyboards.BackToFaq);
                                }
                            if (args[1] == "howendorder")
                                using (FileStream stream = new FileStream("images/BotImages/HowEndOrder.png", FileMode.Open))
                                {
                                    InputMedia media = new InputMedia(stream, "HowEndOrder.png");
                                    InputMediaPhoto photo = new InputMediaPhoto(media);

                                    photo.Caption = Texts.Get().TextHowEndOrder;

                                    await Program.Bot.EditMessageMediaAsync(CB.Message.Chat.Id, CB.Message.MessageId, photo, replyMarkup: Keyboards.BackToFaq);
                                }
                            break;
                        case "support":
                            await Program.Bot.SendTextMessageAsync(CallbackFrom.Id, "Введите ваш вопрос (Для отмены этого дейстия - напишите /cancel)");
                            Program.Users[UserIndex].Status = "question";
                            
                            await Program.Bot.AnswerCallbackQueryAsync(CB.Id);
                            break;
                        case "stocks":
                            using (FileStream stream = new FileStream("images/BotImages/Stocks.png", FileMode.Open))
                            {
                                InputMedia media = new InputMedia(stream, "MainMenu.png");
                                InputMediaPhoto photo = new InputMediaPhoto(media);

                                photo.Caption = Texts.Get().TextStocks;

                                await Program.Bot.EditMessageMediaAsync(CB.Message.Chat.Id, CB.Message.MessageId, photo, replyMarkup: Message.Keyboards.Menu);
                            }
                            break;
                        case "reviews":
                            using (FileStream stream = new FileStream("images/BotImages/Reviews.png", FileMode.Open))
                            {
                                InputMedia media = new InputMedia(stream, "MainMenu.png");
                                InputMediaPhoto photo = new InputMediaPhoto(media);

                                photo.Caption = Texts.Get().TextReviews;

                                await Program.Bot.EditMessageMediaAsync(CB.Message.Chat.Id, CB.Message.MessageId, photo, replyMarkup: Message.Keyboards.Menu);
                            }
                            break;
                        case "menu":
                            using (FileStream stream = new FileStream("images/BotImages/MainMenu.png", FileMode.Open))
                            {
                                InputMedia media = new InputMedia(stream, "MainMenu.png");
                                InputMediaPhoto photo = new InputMediaPhoto(media);

                                photo.Caption = "Главное меню!";

                                await Program.Bot.EditMessageMediaAsync(CB.Message.Chat.Id, CB.Message.MessageId, photo, replyMarkup: Message.Keyboards.Menu);
                            }
                            break;
                        case "cantconnect":
                            await Program.Bot.SendTextMessageAsync(System.Convert.ToInt32(args[1]), "Оператор не смог с вами связатся.\nПожалуйста, укажите имя пользователя в настройках Telegram и переоформите заказ.");
                            await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, CB.Message.Text);
                            break;
                        case "set":
                            if (User.GetPermissionLevel() == 2)
                            {
                                if (args[1] == "orders")
                                    Config.Get().OrdersGroupId = System.Convert.ToInt64(args[2]);
                                else
                                    Config.Get().QuestionsGroupId = System.Convert.ToInt64(args[2]);
                                Config.Save();
                                await Program.Bot.DeleteMessageAsync(CB.Message.Chat.Id, CB.Message.MessageId);
                                await Program.Bot.AnswerCallbackQueryAsync(CB.Id, "Успешно");
                            }
                            break;
                        case "product":
                            await Program.Bot.AnswerCallbackQueryAsync(CB.Id);
                            if (args[1] == "menuu")
                                using (FileStream stream = new FileStream("images/BotImages/MainMenu.png", FileMode.Open))
                                {
                                    InputMedia media = new InputMedia(stream, "MainMenu.png");
                                    InputMediaPhoto photo = new InputMediaPhoto(media);

                                    photo.Caption = "Главное меню!";

                                    await Program.Bot.EditMessageMediaAsync(CB.Message.Chat.Id, CB.Message.MessageId, photo, replyMarkup: Message.Keyboards.Menu);
                                }
                            if (args[1] == "menu")
                            {
                                using (FileStream stream = new FileStream("images/BotImages/GamesMenu.png", FileMode.Open))
                                {
                                    InputMedia media = new InputMedia(stream, "GamesMenu.png");
                                    InputMediaPhoto photo = new InputMediaPhoto(media);

                                    photo.Caption = "Список доступных игр для аренды:";

                                    await Program.Bot.EditMessageMediaAsync(CB.Message.Chat.Id, CB.Message.MessageId, photo, replyMarkup: Keyboards.ProductsToOrderList("select"));
                                }
                            }
                            if (args[1] == "showlist")
                                using (FileStream stream = new FileStream("images/BotImages/GamesMenu.png", FileMode.Open))
                                {
                                    InputMedia media = new InputMedia(stream, "GamesMenu.png");
                                    InputMediaPhoto photo = new InputMediaPhoto(media);

                                    photo.Caption = "Список доступных игр для аренды:";

                                    await Program.Bot.EditMessageMediaAsync(CB.Message.Chat.Id, CB.Message.MessageId, photo, replyMarkup: Keyboards.ProductsToOrderList("select"));
                                }
                            if (args[1] == "select")
                            {
                                var productid = Models.Product.FindProduct(Program.Products, System.Convert.ToInt32(args[2]));
                                var product = Program.Products[productid];


                                if (System.IO.File.Exists($"images/{product.Id}.png"))
                                {
                                    using (FileStream stream = new FileStream($"images/{product.Id}.png", FileMode.Open))
                                    {
                                        InputMedia media = new InputMedia(stream, $"images/{product.Id}.png");
                                        InputMediaPhoto photo = new InputMediaPhoto(media);
                                        photo.Caption = product.Description;
                                        await Program.Bot.EditMessageMediaAsync(CB.Message.Chat.Id, CB.Message.MessageId, photo, replyMarkup: Keyboards.ProductOrder(product.Id));
                                    }
                                }
                                else
                                {
                                    using (FileStream stream = new FileStream($"images/BotImages/NoGameImage.png", FileMode.Open))
                                    {
                                        InputMedia media = new InputMedia(stream, $"images/BotImages/NoGameImage.png");
                                        InputMediaPhoto photo = new InputMediaPhoto(media);
                                        photo.Caption = $"{product.Name}\n\n{product.Description}";
                                        await Program.Bot.EditMessageMediaAsync(CB.Message.Chat.Id, CB.Message.MessageId, photo, replyMarkup: Keyboards.ProductOrder(product.Id));
                                    }
                                }

                            }
                            if (args[1] == "order")
                            {
                                Program.Users[UserIndex].Status = $"order&{args[2]}&{args[3]}&enterphone";
                                await Program.Bot.SendTextMessageAsync(CallbackFrom.Id, "Введите свой телефон для обратной связи (Если хотите отменить это действие - напишите /cancel)");
                            }
                            if (User.GetPermissionLevel() > 0)
                            {
                                if (args[1] == "delete")
                                {
                                    if (User.GetPermissionLevel() > 0)
                                    {
                                        if (System.IO.File.Exists($@"images/{args[2]}.png"))
                                            System.IO.File.Delete($@"images/{args[2]}.png");
                                        Program.Products.RemoveAt(Models.Product.FindProduct(Program.Products, System.Convert.ToInt32(args[2])));
                                        await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, "Товар удален", replyMarkup: Message.Keyboards.AdminPanel(User.GetPermissionLevel()));
                                        Models.Product.Save();
                                    }
                                }
                                if (args[1] == "switch")
                                {
                                    if (User.GetPermissionLevel() > 0)
                                    {
                                        Program.Products[Models.Product.FindProduct(Program.Products, System.Convert.ToInt32(args[2]))].isVisible = !Program.Products[Models.Product.FindProduct(Program.Products, System.Convert.ToInt32(args[2]))].isVisible;
                                        await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, "Выполнено!", replyMarkup: Keyboards.ProductsToSwitchList("switch"));
                                        Models.Product.Save();
                                    }
                                }
                                if (args[1] == "change")
                                {
                                    if (args[2] == "select")
                                    {
                                        await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, "Что нужно поменять?", replyMarkup: Keyboards.Change(System.Convert.ToInt32(args[3])));
                                    }
                                    if (args[2] == "name")
                                    {
                                        Program.Users[UserIndex].Status = $"changepropproduct&name&{args[3]}";
                                        await Program.Bot.SendTextMessageAsync(CallbackFrom.Id, "Введите новое имя товара (Для отмены используйте /cancel)");
                                    }
                                    if (args[2] == "desc")
                                    {
                                        Program.Users[UserIndex].Status = $"changepropproduct&desc&{args[3]}";
                                        await Program.Bot.SendTextMessageAsync(CallbackFrom.Id, "Введите новое описание товара товара (Для отмены используйте /cancel)");
                                    }
                                    if (args[2] == "image")
                                    {
                                        Program.Users[UserIndex].Status = $"changepropproduct&image&{args[3]}";
                                        await Program.Bot.SendTextMessageAsync(CallbackFrom.Id, "Отправьте новую картинку товара (Для отмены используйте /cancel)");
                                    }
                                }
                            }
                            break;
                        case "admin":
                            if (User.GetPermissionLevel() > 0)
                            {
                                switch (args[1])
                                {
                                    case "save":
                                        Config.Save();
                                        Models.Product.Save();
                                        Models.User.Save();
                                        Texts.Save();
                                        await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, "Текущее состояние бота сохранено", replyMarkup: Message.Keyboards.AdminPanel(User.GetPermissionLevel()));
                                        break;
                                    case "publicmessage":
                                        Program.Users[UserIndex].Status = "publicmessage";
                                        await Program.Bot.SendTextMessageAsync(CallbackFrom.Id, "Отправьте текст для рассылки всем пользователям бота");
                                        break;
                                    case "changetext":
                                        if (args[2] == "getlist")
                                        {
                                            await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, "Какой раздел нужно отредактировать?", replyMarkup: Keyboards.Texts("admin&changetext"));
                                        }
                                        if (args[2] == "stocks")
                                        {
                                            Program.Users[UserIndex].Status = "changetext&stocks";
                                            await Program.Bot.SendTextMessageAsync(CallbackFrom.Id, "Отправьте новый текст для раздела \"Акции\"");
                                        }
                                        if (args[2] == "reviews")
                                        {
                                            Program.Users[UserIndex].Status = "changetext&reviews";
                                            await Program.Bot.SendTextMessageAsync(CallbackFrom.Id, "Отправьте новый текст для раздела \"Отзывы\"");
                                        }
                                        if (args[2] == "howgoingorder")
                                        {
                                            Program.Users[UserIndex].Status = "changetext&howgoingorder";
                                            await Program.Bot.SendTextMessageAsync(CallbackFrom.Id, "Отправьте новый текст для раздела \"Как проходит аренда\"");
                                        }
                                        if (args[2] == "p2")
                                        {
                                            Program.Users[UserIndex].Status = "changetext&p2";
                                            await Program.Bot.SendTextMessageAsync(CallbackFrom.Id, "Отправьте новый текст для раздела \"П2\"");
                                        }
                                        if (args[2] == "p3")
                                        {
                                            Program.Users[UserIndex].Status = "changetext&p3";
                                            await Program.Bot.SendTextMessageAsync(CallbackFrom.Id, "Отправьте новый текст для раздела \"П3\"");
                                        }
                                        if (args[2] == "howendorder")
                                        {
                                            Program.Users[UserIndex].Status = "changetext&howendorder";
                                            await Program.Bot.SendTextMessageAsync(CallbackFrom.Id, "Отправьте новый текст для раздела \"Как заканчивается аренда\"");
                                        }
                                        break;
                                    case "addproduct":
                                        int id = Models.Product.FreeId;
                                        Models.Product.FreeId += 1;
                                        Program.Products.Add(new Models.Product() { Id = id, Name = "Product", isVisible = false });
                                        Program.Users[UserIndex].Status = $"addpropproduct&name&{id}";
                                        await Program.Bot.SendTextMessageAsync(CallbackFrom.Id, "Теперь введите имя товара");
                                        break;
                                    case "deleteproduct":
                                        if (args[2] == "getlist")
                                            await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, "Список товаров", replyMarkup: Keyboards.ProductsList("delete"));
                                        break;
                                    case "switchproduct":
                                        if (args[2] == "getlist")
                                        {
                                            await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, "Список товаров", replyMarkup: Keyboards.ProductsToSwitchList("switch"));
                                        }
                                        break;
                                    case "menu":
                                        await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, "Админ меню", replyMarkup: Message.Keyboards.AdminPanel(User.GetPermissionLevel()));
                                        break;
                                    case "ban":
                                        if (args[2] == "getlist")
                                            await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, "Список пользователей", replyMarkup: Keyboards.ToBanList());
                                        if (args[2] == "userban")
                                        {
                                            Program.Users[Models.User.FindUser(Program.Users, System.Convert.ToInt32(args[3]))].Ban();
                                            await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, "Пользователь забанен", replyMarkup: Message.Keyboards.AdminPanel(User.GetPermissionLevel()));
                                        }
                                        break;
                                    case "unban":
                                        if (args[2] == "getlist")
                                            await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, "Список пользователей", replyMarkup: Keyboards.ToUnBanList());
                                        if (args[2] == "userunban")
                                        {
                                            Program.Users[Models.User.FindUser(Program.Users, System.Convert.ToInt32(args[3]))].Unban();
                                            await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, "Пользователь разбанен", replyMarkup: Message.Keyboards.AdminPanel(User.GetPermissionLevel()));
                                        }
                                        break;
                                    case "addadmin":
                                        if (args[2] == "getlist")
                                            await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, "Список пользователей", replyMarkup: Keyboards.UserList());
                                        if (args[2] == "addadmin")
                                        {
                                            Program.Users[Models.User.FindUser(Program.Users, System.Convert.ToInt32(args[3]))].Permission = "admin";
                                            await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, "Администратор добавлен", replyMarkup: Message.Keyboards.AdminPanel(User.GetPermissionLevel()));
                                        }

                                        break;
                                    case "removeadmin":
                                        if (args[2] == "getlist")
                                            await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, "Список пользователей", replyMarkup: Keyboards.AdminList());
                                        if (args[2] == "removeadmin")
                                        {
                                            Program.Users[Models.User.FindUser(Program.Users, System.Convert.ToInt32(args[3]))].Permission = "user";
                                            await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, "Администратор удален", replyMarkup: Message.Keyboards.AdminPanel(User.GetPermissionLevel()));
                                        }
                                        break;
                                    case "changeproduct":
                                        if (args[2] == "getlist")
                                            await Program.Bot.EditMessageTextAsync(CB.Message.Chat.Id, CB.Message.MessageId, "список товаров", replyMarkup: Keyboards.ProductsList("change&select"));
                                        break;
                                }
                            }
                            break;

                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}
