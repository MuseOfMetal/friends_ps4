using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;
namespace Shop.Message
{
    public static class Keyboards
    {
        public static InlineKeyboardMarkup Menu = new InlineKeyboardMarkup
            (
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Аренда игр на PlayStation 4 🚀", "product&showlist")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Отзывы о нас 💁‍♂️", "reviews")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("FAQ", "faq&main")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Акции 🔥", "stocks"),
                    InlineKeyboardButton.WithCallbackData("Поддержка 👥", "support")
                }
            }

            );
        public static InlineKeyboardMarkup Setup(long id)
        {
            return new InlineKeyboardMarkup
                (
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Заказов", $"set&orders&{id}"),
                        InlineKeyboardButton.WithCallbackData("Вопросов", $"set&questions&{id}")
                    }
                );
        }


        public static InlineKeyboardMarkup CantConnect(int id)
        {
            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Невозможно соединится", $"cantconnect&{id}")
                }
            });
        }

        public static InlineKeyboardMarkup AdminPanel(int permissionlevel)
        {
            List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Добавить товар ➕", "admin&addproduct")});
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Изменить свойства товара ✍️", "admin&changeproduct&getlist") });
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("🔒Скрыть/Отобразить товар 🔓", "admin&switchproduct&getlist") });
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Удалить товар ➖", "admin&deleteproduct&getlist") });
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Заблокировать пользователя ❌", "admin&ban&getlist") });
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Разблокировать пользователя ✅", "admin&unban&getlist") });
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Изменить текст", "admin&changetext&getlist") });
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Рассылка сообщения", "admin&publicmessage") });
            buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Сохранить все", "admin&save") });
            if (permissionlevel == 2)
            {
                buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Добавить администратора", "admin&addadmin&getlist") });
                buttons.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Удалить администратора", "admin&removeadmin&getlist") });
            }
            return buttons.ToArray();
        }
    }
}


