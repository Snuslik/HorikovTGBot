using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


var botClient = new TelegramBotClient("6591945097:AAH6zsilcR2R8ipDP3_3keHpxeN7i_aWdbQ");

using CancellationTokenSource cts = new();

ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>()
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Message is not { } message)
        return;
    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;


    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
        if (messageText == "проверка" || messageText == "Проверка")
        {
            await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "я работаю корректно",
            cancellationToken: cancellationToken);
        }
        if (messageText == "привет" || messageText == "Привет")
        {
            await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Здраствуйте! что вам показать?",
            cancellationToken: cancellationToken);
        }
        if (messageText == "картинка" || messageText == "Картинка")
        {
            message = await botClient.SendPhotoAsync(
        chatId: chatId,
        photo: InputFile.FromUri("https://upload.wikimedia.org/wikipedia/ru/f/fc/Doom_new_cover_art.jpg"),
        parseMode: ParseMode.Html,
        cancellationToken: cancellationToken);
        }
        if (messageText == "видео" || messageText == "Видео")
        {
            message = await botClient.SendVideoAsync(
         chatId: chatId,
         video: InputFile.FromUri("https://github.com/Snuslik/HorikovRPG/raw/main/video5318981181193073335.mp4"),
         thumbnail: InputFile.FromUri("https://raw.githubusercontent.com/TelegramBots/book/master/src/2/docs/thumb-clock.jpg"),
         supportsStreaming: true,
         cancellationToken: cancellationToken);
        }
        if (messageText == "стикер" || messageText == "Стикер")
        {
            Message message1 = await botClient.SendStickerAsync(chatId: chatId,
            sticker: InputFile.FromUri("https://raw.githubusercontent.com/Snuslik/HorikovRPG/main/5318815790592440875.webp"), cancellationToken: cancellationToken);
        }
        
}


Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
};