using Telegram.Bot.Types;
using Telegram.Bot;
using UnityBot.Bot.Models.Enums;
using UnityBot.Bot.Services.UserServices;

namespace UnityBot.Bot.Services.Handlers
{
    public partial class BotUpdateHandler
    {
        private async Task TalabaEkan(ITelegramBotClient client, Message message, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _userRepository = scope.ServiceProvider.GetRequiredService<IUserService>();
                var user = await _userRepository.GetUser(message.Chat.Id, cancellationToken);
                if (user == null)
                {
                    throw new Exception();
                }
                if (user.Status == UserStatus.RezumeJoylashStudent)
                {
                    await _userRepository.UpdateUserStatus(message.Chat.Id, UserStatus.RezumeJoylashAbout, cancellationToken);
                    await HandleRezumeJoylashBotAsync(client, message, user, cancellationToken);
                }
                else if (user.Status == UserStatus.UstozKerakStudent)
                {
                    await _userRepository.UpdateUserStatus(message.Chat.Id, UserStatus.UstozKerakHaqida, cancellationToken);
                    await HandleUstozKerakBotAsync(client, message, user, cancellationToken);
                }
                return;
            }
        }

        private async Task TalabaEmas(ITelegramBotClient client, Message message, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _userRepository = scope.ServiceProvider.GetRequiredService<IUserService>();
                var user = await _userRepository.GetUser(message.Chat.Id, cancellationToken);
                if (user == null)
                {
                    throw new Exception();
                }
                if (user.Status == UserStatus.RezumeJoylashStudent)
                {
                    await _userRepository.UpdateUserStatus(message.Chat.Id, UserStatus.RezumeJoylashAbout, cancellationToken);
                    await HandleRezumeJoylashBotAsync(client, message, user, cancellationToken);
                }
                else if (user.Status == UserStatus.UstozKerakStudent)
                {
                    await _userRepository.UpdateUserStatus(message.Chat.Id, UserStatus.UstozKerakHaqida, cancellationToken);
                    await HandleUstozKerakBotAsync(client, message, user, cancellationToken);
                }
                return;
            }
        }
    }
}
