using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UnityBot.Bot.Models.Entities;
using UnityBot.Bot.Models.Enums;
using UnityBot.Bot.Services.ReplyKeyboards;
using Update = Telegram.Bot.Types.Update;

namespace UnityBot.Bot.Services.Handlers;

public partial class BotUpdateHandler
{
    private const string LINK = "https://t.me/effect_mehnat";
    private const string BotLINK = "https://t.me/effect_mehnat_bot";
    private const string MainChanel = "-1002230870026";
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
        throw new NotImplementedException();
    }

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
            "joyla" => SentToMainChanelAsync(client, callbackQuery.Message, cancellationToken),
            "skip" => SkipFromModeratorsAsync(client, callbackQuery.Message, cancellationToken),
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

    private async Task NotogriElonJoylashAsync(ITelegramBotClient client, Message? message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task NoAdditionalInfo(ITelegramBotClient client, Message? message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task HandleSherikKerakAsync(ITelegramBotClient client, Message? message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task HandleUstozkerakAsync(ITelegramBotClient client, Message? message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task HandleShogirtKerakAsync(ITelegramBotClient client, Message? message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task HandleRezumeJoylashAsync(ITelegramBotClient client, Message? message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task HandleIshJoylashAsync(ITelegramBotClient client, Message? message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }


    #region Clears
    private async Task ClearMessageMethod(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    private async Task ClearUpdateMethod(ITelegramBotClient botClient, CallbackQuery callback, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    private async Task HandleClearAllReplyKeysAsync(ITelegramBotClient client, Message message, UserModel user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Checker

    public async Task HandleTextCorrectAsync(ITelegramBotClient _client, Message message, CancellationToken cancellation)
    {
        throw new NotImplementedException();

    }
    #endregion

    #region TalabaUchun
    private async Task TalabaEkan(ITelegramBotClient client, Message message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task TalabaEmas(ITelegramBotClient client, Message message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region ElonUchun
    private async Task TogriElonJoylashAsync(ITelegramBotClient client, Message message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

    }

    #endregion

    #region ModeratorlarUchun
    private async Task SentToMainChanelAsync(ITelegramBotClient client, Message message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();


    }

    private async Task SkipFromModeratorsAsync(ITelegramBotClient client, Message message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

    }
    #endregion

}