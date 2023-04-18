

using datatest4.Interfaces;
using datatest4.Repository;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace datatest4.Commands
{
    public class WalkPerDateCommand : BaseCommand
    {
        private readonly IUserRepository _userRepository;
        private readonly ITrackLocationRepository _trackLocationRepository;
        private readonly TelegramBotClient _botClient;
        public WalkPerDateCommand(IUserRepository userRepository, TelegramBot telegramBot, 
            ITrackLocationRepository trackLocationRepository)
        {
            _userRepository = userRepository;
            _trackLocationRepository = trackLocationRepository;
            _botClient = telegramBot.GetBot().Result;
        }

        public override string Name => "/get-walks-per-date";

        public async override Task ExecuteAsync(Update update)
        {
            var user = await _userRepository.GetOrCreate(update);
            try
            {
                var messageDate = Convert.ToDateTime(update.Message.Text);
                var tracking = await _trackLocationRepository.GetAllTracks();
                var walks = _trackLocationRepository.GetAllWalks(tracking, user);
                var distance = _trackLocationRepository.GetPassedDistancePerDay(walks, user, messageDate);
                var message = new StringBuilder($"Ви пройшли {distance} км за {messageDate.ToShortDateString()}\n");

                await _botClient.SendTextMessageAsync(user.chatId, message.ToString(),
ParseMode.Markdown);
            }
            catch (FormatException)
            {
                await _botClient.SendTextMessageAsync(user.chatId, "Введіть коректну дату",
ParseMode.Markdown);
            }
        }
    }
}
