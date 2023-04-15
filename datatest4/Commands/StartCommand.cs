using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using datatest4.Interfaces;
using Telegram.Bot.Types;
using datatest4.Data;

namespace datatest4.Commands
{
    public class StartCommand : BaseCommand
    {
        private readonly IUserRepository _userService;
        private readonly TelegramBotClient _botClient;
        public StartCommand(IUserRepository userRepoitory, TelegramBot telegramBot)
        {
            _userService = userRepoitory;
            _botClient = telegramBot.GetBot().Result;
        }
        public override string Name => CommandNames.StartCommand;

        public override async Task ExecuteAsync(Update update)
        {
          
            await _botClient.SendTextMessageAsync(update.Message.Chat.Id, "Введіть IMEI",
                ParseMode.Markdown);
        }
    }
}
