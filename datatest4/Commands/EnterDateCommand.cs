using datatest4.Data;
using datatest4.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace datatest4.Commands
{
    public class EnterDateCommand : BaseCommand
    {
        private readonly IUserRepository _userRepository;
        private readonly TelegramBotClient _botClient;
        public override string Name => CommandNames.EnterDayCommand;
        public EnterDateCommand(IUserRepository userRepository, TelegramBot telegramBot)
        {
            _userRepository = userRepository;
            _botClient = telegramBot.GetBot().Result;
        }
        public async override Task ExecuteAsync(Update update)
        {
            var user = await _userRepository.GetOrCreate(update);

            await _botClient.SendTextMessageAsync(user.chatId, "Введіть дату прогулянки",
     ParseMode.Markdown);
        }
    }
}
