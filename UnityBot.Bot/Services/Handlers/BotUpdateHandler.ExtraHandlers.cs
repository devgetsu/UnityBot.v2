using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using UnityBot.Bot.Services.ReplyKeyboards;

namespace UnityBot.Bot.Services.Handlers
{
    public partial class BotUpdateHandler
    {
        private async Task HandleNotImplementedMessageAsync(ITelegramBotClient client, Message message, CancellationToken cancellationToken)
        {
            try
            {
                await client.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: $"<strong>Assalomu alaykum, \"EFFECT | Katta mehnat bozori\" kanali uchun e'lon yaratuvchi botiga xush kelibsiz.\r\n\r\n \"EFFECT | Katta mehnat bozori\" - ish izlayotgan odamlarga vakansiyalarni, ish beruvchilarga esa ishchilarni topishda yordam beradi. Qolaversa bir qator boshqa yo'nalishlarni ham qollab quvvatlaydi.</strong>",
                            parseMode: ParseMode.Html, replyMarkup: await InlineKeyBoards.ForMainState(),
                            cancellationToken: cancellationToken);
                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }
        private async Task HandleLocationMessageAsync(ITelegramBotClient client, Message message, CancellationToken cancellationToken)
        {
            try
            {

                var letitude = message.Location.Latitude;
                var longitude = message.Location.Longitude;

                await client.SendTextMessageAsync(
                       chatId: message.Chat.Id,
                       text: $"Your Latitude {letitude} and Longitude {longitude}",
                       cancellationToken: cancellationToken);
                return;
            }
            catch
            {
                return;
            }
        }
        private async Task HandlePhotoMessageAsync(ITelegramBotClient client, Message message, CancellationToken cancellationToken)
        {
            try
            {

                await client.SendTextMessageAsync(
                       chatId: message.Chat.Id,
                       text: "You Send Unknown Message",
                       cancellationToken: cancellationToken);
                return;
            }
            catch
            {
                return;
            }
        }
        private async Task HandleStickerMessageAsync(ITelegramBotClient client, Message message, CancellationToken cancellationToken)
        {
            try
            {

                await client.SendTextMessageAsync(
                       chatId: message.Chat.Id,
                       text: "You Send Sticker Message",
                       cancellationToken: cancellationToken);
                return;
            }
            catch
            {
                Console.WriteLine("Err StickerMessage asyc");
                return;
            }
        }

    }
}
