using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UnityBot.Bot.Models.Entities;
using UnityBot.Bot.Services.UserServices;
using UnityBot.Bot.Models.Enums;
using UnityBot.Bot.Services.ReplyKeyboards;

namespace UnityBot.Bot.Services.Handlers
{
    public partial class BotUpdateHandler
    {
        private async Task HandleSherikKerakAsync(ITelegramBotClient client, Message? message, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _userRepository = scope.ServiceProvider.GetRequiredService<IUserService>();

                var user = await _userRepository.GetUser(message.Chat.Id, cancellationToken);

                if(user.Status == UserStatus.MainPage)
                {
                    await _userRepository.UpdateUserStatus(user.Chatid, UserStatus.SherikKerak, cancellationToken);
                }
                switch (user.Status)
                {
                    case UserStatus.SherikKerak:

                        await client.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: $"<strong>🎗 SHERIK KERAK</strong>\r\n" +
                            "\r\nSherik kerakligi haqida e'lon joylashtirish uchun bir nechta savollarga javob bering. Har bir javobingiz to'g'ri va ishonchli ma'lumotlardan iborat bo'lishi kerak ekanligiga e'tiborli bo'ling.\r\n" +
                            "\r\nSo'rovnoma yakunida, agarda kiritilgan barcha ma'lumotlar to'g'ri bo'lsa \"✅ To'g'ri\" tugmasini bosing, aksincha bo'lsa \"❌ Noto'g'ri\" tugmasini bosing va so'rovnomani qaytadan to'ldiring.\r\n" +
                            $"\r\nE'lon tayor bo'lgandan kegin \"✅ E'lonni joylash\" tugmasi bosilsa e'lon o'sha zaxotiyoq \"EFFECT | Katta mehnat bozori\" kanaliga joylashtiriladi.",
                             parseMode: ParseMode.Html,
                            cancellationToken: cancellationToken);

                        await client.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "\r\n⭐️ <strong>Sherik:</strong> \r\nSherikning Ism Familiyasini yozing.",
                            replyMarkup: new ReplyKeyboardRemove(),
                            parseMode: ParseMode.Html,
                            cancellationToken: cancellationToken);

                        await _userRepository.UpdateUserStatus(user.Chatid, UserStatus.SherikKerakKim, cancellationToken);

                        return;

                    case UserStatus.SherikKerakKim:

                        await _userRepository.AddToListMessages(user.Chatid, message.Text, cancellationToken);
                        
                        await client.SendTextMessageAsync(
                           chatId: message.Chat.Id,
                           text: "📋 <strong>Sheriklik yo'nalishi:</strong> \r\nQanday yo'nalish bo'yicha sherik qidirilayotgan bo'lsa, shu yo'nalishni kiriting, misol uchun:\r\n\r\n" +
                           "• <i>IT yo'nalishi</i>\r\n" +
                           "• <i>Savdo-sotiq yo'nalishi</i>\r\n" +
                           "• <i>Ishlab chiqarish yo'nalishi</i>\r\n" +
                           "• <i>Qimmatbaho toshlar yo'nalishi</i>",
                           parseMode: ParseMode.Html,
                           cancellationToken: cancellationToken);

                        await _userRepository.UpdateUserStatus(user.Chatid, UserStatus.SherikKerakYonalishi, cancellationToken);

                        return;

                    case UserStatus.SherikKerakYonalishi:
                        
                        await _userRepository.AddToListMessages(user.Chatid, message.Text, cancellationToken);

                        await client.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "📑 <strong>Sheriklik haqida:</strong> \r\nSheriklik haqida qisqacha ma'lumot bering. Misol uchun, nimalar qilinishi haqida yozing.",
                            parseMode: ParseMode.Html,
                            cancellationToken: cancellationToken);

                        await _userRepository.UpdateUserStatus(message.Chat.Id, UserStatus.SherikKerakHisobKitob, cancellationToken);
                        return;

                    case UserStatus.SherikKerakHaqida:

                        await _userRepository.AddToListMessages(user.Chatid, message.Text, cancellationToken);

                        await client.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "📞 <strong>Aloqa:</strong> \r\nBog'lanish uchun telefon raqam yoki elektron pochta manzilini kiriting. Misol uchun:\r\n" +
                            "\r\n• <i>+998912345678</i>" +
                            "\r\n• <i>example@gmail.com</i>",
                            parseMode: ParseMode.Html,
                            cancellationToken: cancellationToken);

                        await _userRepository.UpdateUserStatus(message.Chat.Id, UserStatus.SherikKerakContact, cancellationToken);

                        return;

                    case UserStatus.SherikKerakContact:

                        await _userRepository.AddToListMessages(user.Chatid, message.Text, cancellationToken);

                        await client.SendTextMessageAsync(
                             chatId: message.Chat.Id,
                             text: "🕰 <strong>Murojaat qilish vaqti:</strong> \r\nMurojaat qilish mumkin bo'lgan vaqtlarni kiriting. Misol uchun:\r\n" +
                             "\r\n• <i>9:00 - 18:00</i>",
                             parseMode: ParseMode.Html,
                             cancellationToken: cancellationToken);

                        await _userRepository.UpdateUserStatus(message.Chat.Id, UserStatus.SherikKerakMurojaat, cancellationToken);

                        return;
                    case UserStatus.SherikKerakMurojaat:

                        await _userRepository.AddToListMessages(user.Chatid, message.Text, cancellationToken);

                       var msg = await client.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "📌 <strong>Qo'shimcha ma'lumotlar:</strong>\r\n" +
                            "Qo'shimcha ma'lumotlarni kiriting. Agarda ular yo'q bo'lsa \"Qo'shimcha ma'lumotlar yo'q\" tugmasini bosing.",
                            parseMode: ParseMode.Html,
                            replyMarkup: await InlineKeyBoards.AdditionalInfo(),
                            cancellationToken: cancellationToken);

                        await _userRepository.UpdateUserStatus(message.Chat.Id, UserStatus.IshJoylashAdditionalInfo, cancellationToken);
                        
                        return;
                    case UserStatus.Elonjoylash:

                        await _userRepository.AddToListMessages(user.Chatid, message.Text, cancellationToken);
                        
                        var telegramLine = user.Username != null ? $"\n<strong>✉️ Telegram:</strong> @{user.Username}" : "";

                        await client.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: @$" <strong>🎗 SHERIK KERAK</strong> 

<strong>⭐️ Sherik:</strong> {user.Messages[0]}
<strong>📋 Sheriklik yo'nalishi:</strong> {user.Messages[1]}
<strong>💰 Hisob-kitob:</strong> {user.Messages[2]}
<strong>🌏 Manzil:</strong> {user.Messages[3]}

<strong>📑 Sheriklik haqida:</strong> {user.Messages[4]}

<strong>📞 Aloqa:</strong> {user.Messages[5]}{telegramLine}
<strong>🕰 Murojaat qilish vaqti:</strong> {user.Messages[6]}

<strong>📌 Qo'shimcha ma'lumotlar:</strong> {user.Messages[7]}

#SherikKerak

<strong><a href='{LINK}'>🌐 ""EFFECT | Katta mehnat bozori"" kanaliga obuna bo'lish</a></strong>
•
<strong><a href='{BotLINK}'>⏺ ""EFFECT | Katta mehnat bozori"" kanaliga e'lon joylash</a></strong>",
                        parseMode: ParseMode.Html,
                        disableWebPagePreview: true,
                        cancellationToken: cancellationToken);


                        var inlineBarcha = await client.SendTextMessageAsync(
                               chatId: message.Chat.Id,
                               text: "Barcha ma'lumotlar to'g'rimi?",
                               replyMarkup: await InlineKeyBoards.ForHaYuqButton(),
                               parseMode: ParseMode.Html,
                               cancellationToken: cancellationToken);

                        await _userRepository.UpdateUserShouldDeleteId(user.Chatid, inlineBarcha.MessageId, cancellationToken);
                        return;
                }
            }
        }
    }
}
