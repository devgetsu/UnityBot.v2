using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UnityBot.Bot.Models.Entities;
using UnityBot.Bot.Models.Enums;
using UnityBot.Bot.Services.ReplyKeyboards;
using UnityBot.Bot.Services.UserServices;
using Update = Telegram.Bot.Types.Update;

namespace UnityBot.Bot.Services.Handlers;

public partial class BotUpdateHandler
{
    private async Task HandleMessageAsync(ITelegramBotClient client, Message message, CancellationToken cancellationToken)
    {
        var messageType = message.Type switch
        {
            MessageType.Text => HandleTextMessageAsnyc(client, message, cancellationToken),
            MessageType.Sticker => HandleStickerMessageAsync(client, message, cancellationToken),
            MessageType.Photo => HandlePhotoMessageAsync(client, message, cancellationToken),
            MessageType.Location => HandleLocationMessageAsync(client, message, cancellationToken),
            _ => HandleNotImplementedMessageAsync(client, message, cancellationToken),
        };
        try
        {
            await messageType;
        }
        catch (Exception ex)
        {
            await messageType;
        }

    }

    private async Task HandleTextMessageAsnyc(ITelegramBotClient client, Message message, CancellationToken cancellationToken)
    {
        try
        {

            if (message.Text == "/start")
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _userRepository = scope.ServiceProvider.GetRequiredService<IUserService>();

                    await _userRepository.UpdateUserStatus(message.Chat.Id, UserStatus.MainPage, cancellationToken);

                    var msg = await client.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: $"\r\n<strong>Assalomu alaykum {message.From.FirstName} {message.From.LastName}, \"EFFECT | Katta mehnat bozori\" kanali uchun e'lon yaratuvchi botiga xush kelibsiz.</strong>\r\n\r\n \"EFFECT | Katta mehnat bozori\" kanali - ish izlayotgan odamlarga vakansiyalarni, ish beruvchilarga esa ishchilarni topishda yordam beradi. Qolaversa bir qator boshqa yo'nalishlarni ham qollab quvvatlaydi.\r\r\n\n<strong>Yo'nalishlar:</strong>\r\n• \"🏢 Ish joylash\" - ishchi topish uchun.\r\n• \"\U0001f9d1🏻‍💼 Rezyume joylash\" - ish topish uchun.\r\n• \"\U0001f9d1🏻 Shogirt kerak\" - shogirt topish uchun.\r\n• \"\U0001f9d1🏻‍🏫 Ustoz kerak\" - ustoz topish uchun.\r\n• \"🎗 Sherik kerak\" - sherik topish uchun.\r\n\r\n<strong>E'lon berish uchun yo'nalishni tanlang 👇</strong>",
                            replyMarkup: await InlineKeyBoards.ForMainState(),
                            parseMode: ParseMode.Html,
                            cancellationToken: cancellationToken);

                    var isChanged = await _userRepository.UpdateUserShouldDeleteId(message.Chat.Id, msg.MessageId, cancellationToken);
                    return;
                };
            }


            else if (!string.IsNullOrWhiteSpace(message.Text.ToString()))
            {
                await HandleRandomTextAsync(client, message, cancellationToken);
            };
        }
        catch
        {
            return;
        }
    }

    private async Task HandleRandomTextAsync(ITelegramBotClient client, Message message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }


    public async Task HandleCallbackQueryAsync(ITelegramBotClient client, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        var myMessage = callbackQuery.Data switch
        {
            "ish_joylash" => HandleIshJoylashAsync(client, callbackQuery.Message, cancellationToken),
            "rezyume_joylash" => HandleRezumeJoylashAsync(client, callbackQuery.Message, cancellationToken),
            "shogirt_kerak" => HandleShogirtKerakAsync(client, callbackQuery.Message, cancellationToken),
            "ustoz_kerak" => HandleUstozkerakAsync(client, callbackQuery.Message, cancellationToken),
            "sherik_kerak" => HandleSherikKerakAsync(client, callbackQuery.Message, cancellationToken),
            "togrri" => TogriElonJoylashAsync(client, callbackQuery.Message, cancellationToken),
            "notogrri" => NotogriElonJoylashAsync(client, callbackQuery.Message, cancellationToken),
            "noinfo" => NoAdditionalInfo(client, callbackQuery.Message, cancellationToken),
            "talabaekan" => TalabaEkan(client, callbackQuery.Message, cancellationToken),
            "talabaemas" => TalabaEmas(client, callbackQuery.Message, cancellationToken),
            "hatextcorrect" => HandleTextCorrectAsync(client, callbackQuery.Message, cancellationToken),
            "yoqtextincorrect" => NotogriElonJoylashAsync(client, callbackQuery.Message, cancellationToken),
        };

        try
        {
            await myMessage;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private async Task NoAdditionalInfo(ITelegramBotClient client, Message? message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}