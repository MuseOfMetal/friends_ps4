using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;
namespace Shop.Callback
{
    class Keyboards
    {

        public static InlineKeyboardMarkup ToBanList()
        {
            List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
            var users = Program.Users;
            foreach (var item in users)
            {
                if (item.GetPermissionLevel() != -1 && !(item.GetPermissionLevel() > 0))
                    buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"{item.FirstName} {(!string.IsNullOrEmpty(item.UserName) ? $"@{item.UserName}" : $"")}", $"admin&ban&userban&{item.UserId}") });
            }
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"Назад", $"admin&menu") });
            return buttons.ToArray();
        }


        public static InlineKeyboardMarkup UserList()
        {
            List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
            var users = Program.Users;
            foreach (var item in users)
            {
                if (item.GetPermissionLevel() == 0)
                    buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"{item.FirstName} {(!string.IsNullOrEmpty(item.UserName) ? $"@{item.UserName}" : $"")}", $"admin&addadmin&addadmin&{item.UserId}") });
            }
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"Назад", $"admin&menu") });
            return buttons.ToArray();
        }

        public static InlineKeyboardMarkup AdminList()
        {
            List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
            var users = Program.Users;
            foreach (var item in users)
            {
                if (item.GetPermissionLevel() == 1)
                    buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"{item.FirstName} {(!string.IsNullOrEmpty(item.UserName) ? $"@{item.UserName}" : $"")}", $"admin&removeadmin&removeadmin&{item.UserId}") });
            }
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"Назад", $"admin&menu") });
            return buttons.ToArray();
        }


        public static InlineKeyboardMarkup ToUnBanList()
        {
            List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
            var users = Program.Users;
            foreach (var item in users)
            {
                if (item.GetPermissionLevel() == -1)
                    buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"{item.FirstName} {(!string.IsNullOrEmpty(item.UserName) ? $"@{item.UserName}" : $"")}", $"admin&unban&userunban&{item.UserId}") });

            }
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"Назад", $"admin&menu") });
            return buttons.ToArray();
        }

        public static InlineKeyboardMarkup ProductsToOrderList(string task)
        {
            List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
            var Products = Program.Products;
            foreach (var product in Products)
            {

                if (product.isVisible)
                    buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"{product.Name}", $"product&{task}&{product.Id}") });
            }
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"Назад", $"product&menuu") });
            return buttons.ToArray();
        }


        public static InlineKeyboardMarkup ProductsToSwitchList(string task)
        {
            List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
            var Products = Program.Products;
            foreach (var product in Products)
            {

                if (product.isVisible)
                    buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"{product.Name} (Виден)", $"product&{task}&{product.Id}") });
                else
                    buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"{product.Name} (Не виден)", $"product&{task}&{product.Id}") });
            }
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"Назад", $"admin&menu") });
            return buttons.ToArray();
        }


        public static InlineKeyboardMarkup ProductsList(string task)
        {
            List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
            var Products = Program.Products;
            foreach (var product in Products)
            {
                buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"{product.Name}", $"product&{task}&{product.Id}") });
            }
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"Назад", $"admin&menu") });
            return buttons.ToArray();
        }

        public static InlineKeyboardMarkup ProductOrder(int product)
        {
            List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();

            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("На неделю", $"product&order&{product}&week") ,
                InlineKeyboardButton.WithCallbackData("На месяц", $"product&order&{product}&month") });
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Навсегда", $"product&order&{product}&forewer") });
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Назад", $"product&menu") });

            return buttons.ToArray();
        }

        public static InlineKeyboardMarkup Menu = new InlineKeyboardMarkup(
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "menu")
            }
        );


        public static InlineKeyboardMarkup Change(int id)
        {
            List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();

            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Имя", $"product&change&name&{id}") });
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Описание", $"product&change&desc&{id}") });
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Картинку", $"product&change&image&{id}") });
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"Назад", $"admin&menu") });
            return buttons.ToArray();
        }

        public static InlineKeyboardMarkup Texts(string param)
        {
            List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
            if (param == "admin&changetext")
            {
                buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Акции", $"{param}&stocks") });
                buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Отзывы", $"{param}&reviews") });
            }

            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Как проходит аренда", $"{param}&howgoingorder") });
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("П2", $"{param}&p2"), InlineKeyboardButton.WithCallbackData("П3", $"{param}&p3") });
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Как заканчивается аренда", $"{param}&howendorder") });
            if (param.Contains("admin"))
                buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"Назад", $"admin&menu") });
            else
                buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData($"Назад", $"menu") });
            return buttons.ToArray();
        }

        public static InlineKeyboardMarkup BackToFaq = new InlineKeyboardMarkup(new[] { InlineKeyboardButton.WithCallbackData("Назад", "faq&main")}); 
    }
}
