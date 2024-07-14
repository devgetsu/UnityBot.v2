using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using UnityBot.Bot.Services.ReplyKeyboards;
using UnityBot.Bot.Services.UserServices;

namespace UnityBot.Bot.Services.Handlers
{
    public partial class BotUpdateHandler
    {
        public async Task HandleTextCorrectAsync(ITelegramBotClient _client, Message message, CancellationToken cancellation)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _userRepository = scope.ServiceProvider.GetRequiredService<IUserService>();
                var msg = await _client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: $"E'lonni joylash narxi: \"BEPUL 🕑\"\r\n\r\nℹ️ E'lon joylashtirilgandan so'ng, u moderatorlar tomonidan ko'rib chiqiladi. Zaruriyat tug'ilganda, ma'lumotlar to'g'riligini tekshirish maqsadida e'lon muallifi bilan bog'laniladi.\r\n\r\nTayyor e'lonni \"EFFECT | Katta mehnat bozori\" kanaliga joylash uchun \"✅ E'lonni joylash\" tugmasini bosing, bekor qilish uchun \"❌ Bekor qilish\" tugmasini bosing 👇",
                    replyMarkup: await InlineKeyBoards.ForConfirmation(),
                    parseMode: ParseMode.Html,
                    cancellationToken: cancellation
               );

                await _userRepository.UpdateUserShouldDeleteId(message.Chat.Id, msg.MessageId, cancellation);
                return;
            }
        }
    }
}
