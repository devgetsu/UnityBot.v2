﻿
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace UnityBot.Bot.Services
{
    public class BotBackgroundService : BackgroundService
    {
        private readonly TelegramBotClient _client;
        private readonly IUpdateHandler _handler;
        public BotBackgroundService(TelegramBotClient client, IUpdateHandler handler)
        {
            _client = client;
            _handler = handler;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var bot = await _client.GetMeAsync(stoppingToken);

                Console.WriteLine($"Bot starting with this username: {bot.Username}");

                _client.StartReceiving(
                    _handler.HandleUpdateAsync,
                    _handler.HandlePollingErrorAsync,
                    new ReceiverOptions()
                    {
                        ThrowPendingUpdates = true
                    }, stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ex in Bcg Service");
            }
        }

    }
}
