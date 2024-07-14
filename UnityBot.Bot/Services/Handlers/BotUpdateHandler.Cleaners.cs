using Telegram.Bot.Types;
using Telegram.Bot;
using UnityBot.Bot.Models.Entities;
using UnityBot.Bot.Services.UserServices;

namespace UnityBot.Bot.Services.Handlers
{
    public partial class BotUpdateHandler
    {
        private async Task ClearMessageMethod(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            if (message != null)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _userRepository = scope.ServiceProvider.GetRequiredService<IUserService>();

                    var user = await _userRepository.GetUser(message.Chat.Id, cancellationToken);

                    if (user.ShouldDeleteMessage != 0)
                    {
                        await HandleClearAllReplyKeysAsync(botClient, message, user, cancellationToken);
                    }
                }
            }
        }
        private async Task ClearUpdateMethod(ITelegramBotClient botClient, CallbackQuery callback, CancellationToken cancellationToken)
        {
            if (callback != null)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _userRepository = scope.ServiceProvider.GetRequiredService<IUserService>();

                    var user = await _userRepository.GetUser(callback.Message.Chat.Id, cancellationToken);

                    if (user.ShouldDeleteMessage != 0)
                    {
                        await HandleClearAllReplyKeysAsync(botClient, callback.Message, user, cancellationToken);
                    }
                }
            }
        }
        private async Task HandleClearAllReplyKeysAsync(ITelegramBotClient client, Message message, UserModel user, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _userRepository = scope.ServiceProvider.GetRequiredService<IUserService>();

                await client.EditMessageReplyMarkupAsync(
                    chatId: message.Chat.Id,
                    messageId: user.ShouldDeleteMessage,
                    replyMarkup: null,
                    cancellationToken: cancellationToken);

                await _userRepository.UpdateUserShouldDeleteId(user.Chatid, 0, cancellationToken);
            }
        }
    }
}
