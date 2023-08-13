using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Message = Telegram.Bot.Types.Message;

namespace TradeHelper.Misc
{
    public static class TelegramWorker
    {
        private static ITelegramBotClient _botClient;
        private static CancellationTokenSource _cts;
        private static Chat _chatId;

        public static void StartWorking(string token)
        {
            InitTelegramBot(token);
        }

        public static void StopWorking()
        {
            StopTelegramBot();
        }

        private static void InitTelegramBot(string token)
        {
            try
            {
                _botClient = new TelegramBotClient(token);
                _cts = new CancellationTokenSource();
                var cancellationToken = _cts.Token;
                var receiverOptions = new ReceiverOptions
                {
                    AllowedUpdates = new[] { UpdateType.Message },
                };

                _botClient.StartReceiving(
                    HandleUpdateAsync,
                    HandleErrorAsync,
                    receiverOptions,
                    cancellationToken
                );

                if (!string.IsNullOrWhiteSpace(Settings.UserName) && Settings.UserId != null)
                {
                    UpdateChatId();
                }

                DefaultLogger.Info("Bot started " + _botClient.GetMeAsync().Result.FirstName);
            }
            catch (Exception ex)
            {
                DefaultLogger.Error(ex.Message, ex);
            }
        }

        private static void StopTelegramBot()
        {
            try
            {
                if (_cts == null)
                {
                    return;
                }

                _cts.Cancel();

                DefaultLogger.Info("Bot stopped " + _botClient.GetMeAsync().Result.FirstName);
            }
            catch (AggregateException ex)
            {
                DefaultLogger.Error(ex.Message, ex);
            }
        }

        public static async Task HandleUpdateAsync(
            ITelegramBotClient botClient,
            Update update,
            CancellationToken cancellationToken)
        {
            try
            {
                if (update == null)
                {
                    return;
                }

                if (update.Message?.Text != null && update.Type == UpdateType.Message && update.Message.Text.ToLower() == "/start")
                {
                    var message = Resources.FormStrings.TestMessage;

                    UpdateChatId(update.Message.Chat);

                    await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat,
                        text: message);
                }

                if (update.Message?.Text != null && update.Type == UpdateType.Message && update.Message.Text.ToLower() == "/getid")
                {
                    var message = string.Format(
                        Resources.FormStrings.UserIdMessage,
                        update.Message.Chat.Username,
                        update.Message.Chat.Id);

                    await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat,
                        text: message);
                }
            }
            catch (Exception ex)
            {
                DefaultLogger.Error(ex.Message, ex);
            }
        }

        public static async Task HandleErrorAsync(
            ITelegramBotClient botClient,
            Exception ex,
            CancellationToken cancellationToken)
        {
            DefaultLogger.Error(ex.Message, ex);
        }

        public static async Task<Message> SendMessage(string message)
        {
            return await _botClient.SendTextMessageAsync(
                chatId: _chatId,
                text: message);
        }

        public static bool CheckConnection()
        {
            if (Settings.TelegramToken == null)
            {
                return false;
            }

            InitTelegramBot(Settings.TelegramToken);

            var isConnOk = _botClient.TestApiAsync().Result;

            StopTelegramBot();

            return isConnOk;
        }

        private static void UpdateChatId(Chat chatId = null)
        {
            if (chatId == null)
            {
                chatId = new Chat()
                {
                    Username = Settings.UserName,
                    Id = Settings.UserId.Value
                };
            }

            _chatId = chatId;
        }
    }
}
