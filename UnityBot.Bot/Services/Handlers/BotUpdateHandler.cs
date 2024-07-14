using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UnityBot.Bot.Models.Entities;
using UnityBot.Bot.Services.UserServices;

namespace UnityBot.Bot.Services.Handlers
{
    public partial class BotUpdateHandler : IUpdateHandler
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BotUpdateHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _userRepository = scope.ServiceProvider.GetRequiredService<IUserService>();

                    var fromUser = update.Message?.From ?? update.CallbackQuery?.From;
                    if (fromUser == null)
                    {
                        throw new InvalidOperationException("Both Message.From and CallbackQuery.From are null.");
                    }

                    var chatId = update.Message?.Chat?.Id;
                    if (chatId == null)
                    {
                        throw new InvalidOperationException("Chat Id is null.");
                    }

                    var model = new UserModel()
                    {
                        Userid = fromUser.Id,
                        Chatid = chatId.Value,
                        Username = fromUser.Username,
                        FirstName = fromUser.FirstName,
                        LastName = fromUser.LastName,
                        Status = Models.Enums.UserStatus.MainPage
                    };

                    await _userRepository.CreateUser(model, cancellationToken);


                }

                var handler = update.Type switch
                {
                    UpdateType.Message => HandleMessageAsync(botClient, update.Message, cancellationToken),
                    UpdateType.CallbackQuery => HandleCallbackQueryAsync(botClient, update.CallbackQuery, cancellationToken),
                    _ => HandleUnknownMessageAsync(botClient, update, cancellationToken)
                };

                try
                {
                    await ClearMessageMethod(botClient, update.Message, cancellationToken);
                    await ClearUpdateMethod(botClient, update.CallbackQuery, cancellationToken);
                    await handler;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Handler or CLear ex in UpdateAsync");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateAysncda xatolik : {0}", ex.Message.ToString());
                return;
            }
        }

        private async Task HandleUnknownMessageAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("Not impelemented mesage");
            }
            catch
            {
                Console.WriteLine("HandleUnknownMessageAsync error");
            }
        }
    }
}
