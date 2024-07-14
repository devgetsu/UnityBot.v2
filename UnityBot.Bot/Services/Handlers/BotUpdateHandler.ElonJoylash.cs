using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using UnityBot.Bot.Models.Enums;
using UnityBot.Bot.Services.ReplyKeyboards;
using UnityBot.Bot.Services.UserServices;

namespace UnityBot.Bot.Services.Handlers
{
    public partial class BotUpdateHandler
    {
        private async Task NotogriElonJoylashAsync(ITelegramBotClient client, Message? message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        private async Task TogriElonJoylashAsync(ITelegramBotClient client, Message message, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _userRepository = scope.ServiceProvider.GetRequiredService<IUserService>();

                await client.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: $@"<strong>✅ Sizning e'loningiz ""EFFECT | Katta mehnat bozori"" kanaliga joylashtirildi.</strong>

Bizning xizmatimizdan foydalanganingiz uchun hursandmiz, ishlaringizga rivoj tilaymiz ⭐️",
                            parseMode: ParseMode.Html,
                            cancellationToken: cancellationToken);

                var user = await _userRepository.GetUser(message.Chat.Id, cancellationToken);

                if (user != null)
                {
                    await _userRepository.UpdateUserStatus(message.Chat.Id, UserStatus.MainPage, cancellationToken);
                }

                await client.SendTextMessageAsync(
                   chatId: message.Chat.Id,
                   text: "<strong>Yo'nalishlar:</strong>\r\n• \"🏢 Ish joylash\" - ishchi topish uchun.\r\n• \"\U0001f9d1🏻‍💼 Rezyume joylash\" - ish topish uchun.\r\n• \"\U0001f9d1🏻 Shogirt kerak\" - shogirt topish uchun.\r\n• \"\U0001f9d1🏻‍🏫 Ustoz kerak\" - ustoz topish uchun.\r\n• \"🎗 Sherik kerak\" - sherik topish uchun." +
                        "\r\n\r\n<strong>Yangi e'lon berish uchun yo'nalishni tanlang 👇</strong>",
                   replyMarkup: await InlineKeyBoards.ForMainState(),
                   parseMode: ParseMode.Html,
                   cancellationToken: cancellationToken);
                return;
            }

        }
    }
}
