using System.Threading;
using Telegram.Bot.Args;
using System.IO;
namespace Shop.Message
{
    static class MessageController
    {
        public async static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var MessageFrom = e.Message.From;
            int UserIndex = Models.User.FindUser(Program.Users, e.Message.From.Id);

            if (UserIndex == -1)
            {
                Program.Users.Add(new Models.User(MessageFrom.FirstName, MessageFrom.LastName, MessageFrom.Username, MessageFrom.Id));
                UserIndex = Models.User.FindUser(Program.Users, e.Message.From.Id);
            }
            Program.Users[UserIndex].Update(MessageFrom.FirstName, MessageFrom.LastName, MessageFrom.Username);
            var User = Program.Users[UserIndex];
            if (User.UserId == Config.Get().SuperAdminId)
                Program.Users[UserIndex].Permission = "superadmin";
            try
            {

                System.Console.WriteLine($"{User.FirstName} {User.LastName} @{User.UserName} sendmessage: [{e.Message.Text}] with [{User.GetPermissionLevel()}] level permission & with status [{User.Status}]");
                if (User.GetPermissionLevel() != -1)
                {
                    if (e.Message.Text == "/cancel")
                    {
                        Program.Users[UserIndex].Status = null;
                        await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Отменено");
                    }


                    if (!string.IsNullOrEmpty(User.Status))
                    {
                        var args = User.Status.Split('&');
                        if (args[0] == "question")
                        {
                            Program.Users[UserIndex].Status = null;
                            await Program.Bot.SendTextMessageAsync(Config.Get().QuestionsGroupId, "Новый вопрос!!!");
                            await Program.Bot.ForwardMessageAsync(Config.Get().QuestionsGroupId, e.Message.Chat.Id, e.Message.MessageId);
                            await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Ваш вопрос отправлен операторам.");
                        }
                        if (User.GetPermissionLevel() > 0)
                        {
                            if (args[0] == "changetext")
                            {
                                if (args[1] == "stocks")
                                {
                                    Program.Users[UserIndex].Status = null;
                                    Texts.Get().TextStocks = e.Message.Text;
                                    Texts.Save();
                                    await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Раздел успешно изменен!");
                                }
                                if (args[1] == "reviews")
                                {
                                    Program.Users[UserIndex].Status = null;
                                    Texts.Get().TextReviews = e.Message.Text;
                                    Texts.Save();
                                    await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Раздел успешно изменен!");
                                }
                                if (args[1] == "howgoingorder")
                                {
                                    Program.Users[UserIndex].Status = null;
                                    Texts.Get().TextHowGoingOrder = e.Message.Text;
                                    Texts.Save();
                                    await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Раздел успешно изменен!");
                                }
                                if (args[1] == "p2")
                                {
                                    Program.Users[UserIndex].Status = null;
                                    Texts.Get().TextP2 = e.Message.Text;
                                    Texts.Save();
                                    await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Раздел успешно изменен!");
                                }
                                if (args[1] == "p3")
                                {
                                    Program.Users[UserIndex].Status = null;
                                    Texts.Get().TextP3 = e.Message.Text;
                                    Texts.Save();
                                    await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Раздел успешно изменен!");
                                }
                                if (args[1] == "howendorder")
                                {
                                    Program.Users[UserIndex].Status = null;
                                    Texts.Get().TextHowEndOrder = e.Message.Text;
                                    Texts.Save();
                                    await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Раздел успешно изменен!");
                                }
                            }
                            if (args[0] == "publicmessage")
                            {
                                if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                                {
                                    Program.Users[UserIndex].Status = null;
                                    var Users = Program.Users;
                                    int userbanned = 0;
                                    int usersend = 0;
                                    foreach (var item in Users)
                                    {
                                        try
                                        {
                                            if (item.GetPermissionLevel() != -1)
                                            {
                                                await Program.Bot.SendTextMessageAsync(item.UserId, e.Message.Text);
                                                usersend++;
                                            }
                                        }
                                        catch
                                        {
                                            userbanned++;
                                        }
                                    }
                                    await Program.Bot.SendTextMessageAsync(MessageFrom.Id, $"Сведения:\n\nУспешно отправлено: {usersend}\nНе получили сообщение: {userbanned}");
                                }

                            }
                            if (args[0] == "addpropproduct")
                            {
                                int productid = Models.Product.FindProduct(Program.Products, System.Convert.ToInt32(args[2]));
                                if (args[1] == "name")
                                {
                                    Program.Products[productid].Name = e.Message.Text;
                                    var a = await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Имя товара изменено.");
                                    await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Введите описание товара");
                                    Program.Users[UserIndex].Status = $"addpropproduct&description&{args[2]}";
                                }
                                if (args[1] == "description")
                                {
                                    Program.Products[productid].Description = e.Message.Text;
                                    await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Описание товара изменено");
                                    await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Отправьте картинку товара");
                                    Program.Users[UserIndex].Status = $"addpropproduct&photo&{args[2]}";
                                }
                                if (args[1] == "photo")
                                {
                                    if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
                                    {
                                        if (!Directory.Exists("images"))
                                            Directory.CreateDirectory("images");
                                        using (FileStream stream = new FileStream($"images/{args[2]}.png", FileMode.Create))
                                        {
                                            await Program.Bot.GetInfoAndDownloadFileAsync(e.Message.Photo[e.Message.Photo.Length - 1].FileId, stream);
                                        }
                                        await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Изображение товара изменено"); Program.Users[UserIndex].Status = null;
                                        Program.Products[productid].Visible();
                                        await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Товар успешно добавлен и имеет статус (Виден)");
                                    }
                                }
                                Models.Product.Save();
                            }
                            if (args[0] == "changepropproduct")
                            {
                                if (args[1] == "name")
                                {
                                    Program.Products[Models.Product.FindProduct(Program.Products, System.Convert.ToInt32(args[2]))].Name = e.Message.Text;
                                    Program.Users[UserIndex].Status = null;
                                    Models.Product.Save();
                                    await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Имя товара изменено!", replyMarkup: Keyboards.AdminPanel(User.GetPermissionLevel()));
                                }
                                if (args[1] == "desc")
                                {
                                    Program.Products[Models.Product.FindProduct(Program.Products, System.Convert.ToInt32(args[2]))].Description = e.Message.Text;
                                    Program.Users[UserIndex].Status = null;
                                    Models.Product.Save();
                                    await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Описание товара изменено!", replyMarkup: Keyboards.AdminPanel(User.GetPermissionLevel()));
                                }
                                if (args[1] == "image")
                                {
                                    if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
                                    {
                                        if (!Directory.Exists("images"))
                                            Directory.CreateDirectory("images");
                                        if (File.Exists($@"images/{args[2]}"))
                                            File.Delete($@"images/{args[2]}");
                                        using (FileStream stream = new FileStream($@"images/{args[2]}.png", FileMode.Create))
                                        {
                                            await Program.Bot.GetInfoAndDownloadFileAsync(e.Message.Photo[e.Message.Photo.Length - 1].FileId, stream);
                                        }
                                        Program.Users[UserIndex].Status = null;
                                        Models.Product.Save();
                                        await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Картинка товара изменена!", replyMarkup: Keyboards.AdminPanel(User.GetPermissionLevel()));
                                    }

                                }
                            }
                        }
                        if (args[0] == "order")
                        {
                            if (args[3] == "enterphone")
                            {
                                var productid = Models.Product.FindProduct(Program.Products, System.Convert.ToInt32(args[1]));
                                Program.Users[UserIndex].PhoneNumber = e.Message.Text;
                                Program.Users[UserIndex].Status = $"{args[0]}&{args[1]}&{args[2]}&entercoment";
                                await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Введите коментарий к заказу. (Если не требуется - напишите /skip)");
                            }
                            else if (args[3] == "entercoment")
                            {
                                await Program.Bot.SendTextMessageAsync(MessageFrom.Id, "Ваш заказ отправлен на обработку. Ожидайте...");

                                var product = Program.Products[Models.Product.FindProduct(Program.Products, System.Convert.ToInt32(args[1]))];
                                string TextOrder = null;

                                TextOrder += "Новый заказ!!!\n\n";
                                TextOrder += $"Имя товара: {product.Name}\n";
                                TextOrder += $"Время аренды: ";
                                if (args[2] == "week")
                                    TextOrder += "На неделю\n";
                                else if (args[2] == "month")
                                    TextOrder += "На месяц\n";
                                else if (args[2] == "forewer")
                                    TextOrder += "Навсегда\n";
                                TextOrder += "\n\n";
                                TextOrder += "Инфо. о пользователе\n";
                                TextOrder += $"Имя: {User.FirstName}\n";
                                if (!string.IsNullOrEmpty(User.LastName))
                                    TextOrder += $"Фамилия: {User.LastName}\n";
                                if (!string.IsNullOrEmpty(User.UserName))
                                    TextOrder += $"Никнэйм: @{User.UserName}\n";
                                TextOrder += $"Номер телефона: {User.PhoneNumber}\n";
                                if (e.Message.Text != "/skip")
                                    TextOrder += $"Комментарий к заказу: {e.Message.Text}\n";

                                await Program.Bot.SendTextMessageAsync(Config.Get().OrdersGroupId, TextOrder, replyMarkup: Keyboards.CantConnect(User.UserId));

                                Program.Users[UserIndex].Status = null;
                            }


                        }
                    }





                    switch (e.Message.Text)
                    {
                        case "/start":
                            await Program.Bot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);
                            await Hello(MessageFrom.Id);


                        using (FileStream stream = new FileStream(@"images/BotImages/MainMenu.png", FileMode.Open))
                        {
                            await Program.Bot.SendPhotoAsync(MessageFrom.Id, stream, caption: "Добро пожаловать!", replyMarkup: Keyboards.Menu);
                        }

                            break;
                        case "/set":
                            //await Program.Bot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);
                            if (User.GetPermissionLevel() == 2)
                                await Program.Bot.SendTextMessageAsync(e.Message.Chat.Id, "Этот чат будет использоваться для получения:", replyMarkup: Keyboards.Setup(e.Message.Chat.Id));
                            break;
                        case "/admin":
                            if (User.GetPermissionLevel() > 0)
                                await Program.Bot.SendTextMessageAsync(e.Message.From.Id, "Админ панель:", replyMarkup: Keyboards.AdminPanel(User.GetPermissionLevel()));
                            break;
                        case "/test":
                            await Program.Bot.SendTextMessageAsync(e.Message.From.Id, $"{Program.Products.Count}");
                            break;

                    }
                }
            }
            catch (System.Exception vfc)
            {
                System.Console.WriteLine(vfc.Message);
            }
        }

        private async static System.Threading.Tasks.Task Hello(int id)
        {
            Telegram.Bot.Types.Message message = await Program.Bot.SendTextMessageAsync(id, "🖐");
            Thread.Sleep(250);
            await Program.Bot.EditMessageTextAsync(message.Chat.Id, message.MessageId, "👋");
            Thread.Sleep(250);
            await Program.Bot.EditMessageTextAsync(message.Chat.Id, message.MessageId, "🖐");
            Thread.Sleep(250);
            await Program.Bot.EditMessageTextAsync(message.Chat.Id, message.MessageId, "👋");
            Thread.Sleep(250);
            await Program.Bot.EditMessageTextAsync(message.Chat.Id, message.MessageId, "🖐");
            Thread.Sleep(250);
            await Program.Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
        }
    }
}
