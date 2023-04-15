using datatest4.Interfaces;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using datatest4.Data;


namespace datatest4.Commands
{
    public class MainMenuCommand : BaseCommand
    {
        private readonly IUserRepository _userService;
        private readonly ITrackLocationRepository _trackLocationRepository;
        private readonly TelegramBotClient _botClient;
        public MainMenuCommand(IUserRepository userRepoitory, TelegramBot telegramBot, ITrackLocationRepository trackLocationRepository)
        {
            _userService = userRepoitory;
            _trackLocationRepository = trackLocationRepository;
            _botClient = telegramBot.GetBot().Result;
        }
        public override string Name => CommandNames.MainMenuCommand;


        public override async Task ExecuteAsync(Update update)
        {
            var user = await _userService.GetOrCreate(update);

            var trackLocations = await _trackLocationRepository.GetAllTracks();
            var walks = _trackLocationRepository.GetAllWalks(trackLocations, user);

            double totalDistance = 0;
            double totalDuration = 0;

            foreach (var walk in walks)
            {
                totalDistance += walk.Distance;
                totalDuration += walk.Duration;
            }

            var message = $"Всього прогулянок: {walks.Count()}\nВсього км пройдено: {Math.Round(totalDistance)}\nВсього часу, хв: {Math.Round(totalDuration)}";

            var inlineKeyboard = new ReplyKeyboardMarkup(new[]
               {
                new[]
                {
                    new KeyboardButton("/topten")
                    {
                        Text = "Кнопка \"Топ 10 прогулянок\""
                    }
                }
            });

            await _botClient.SendTextMessageAsync(user.chatId, message,
                ParseMode.Markdown, replyMarkup: inlineKeyboard);
        }
    }
}
