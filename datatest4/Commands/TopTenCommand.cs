using datatest4.Interfaces;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using System.Text;
using Telegram.Bot.Types;
using datatest4.Data;

namespace datatest4.Commands
{
    public class TopTenCommand : BaseCommand
    {
        private readonly IUserRepository _userService;
        private readonly ITrackLocationRepository _trackLocationRepository;
        private readonly TelegramBotClient _botClient;
        public TopTenCommand(IUserRepository userRepoitory, TelegramBot telegramBot, ITrackLocationRepository trackLocationRepository)
        {
            _userService = userRepoitory;
            _trackLocationRepository = trackLocationRepository;
            _botClient = telegramBot.GetBot().Result;
        }
        public override string Name => CommandNames.TopTenCommand;
        public override async Task ExecuteAsync(Update update)
        {
            var user = await _userService.GetOrCreate(update);

            var trackLocations = await _trackLocationRepository.GetAllTracks();
            var walks = _trackLocationRepository.GetAllWalks(trackLocations, user).OrderByDescending(w => w.Distance).Take(10);


            var message = new StringBuilder("Топ 10 найдовших прогулянок\n");
            foreach (var walk in walks)
            {
                message.AppendLine($"Назва: Прогулянка  Дистанція в км: {walk.Distance}. Хвилин: {walk.Duration}");
            }
            var inlineKeyboard = new ReplyKeyboardMarkup(new[]
               {
                new[]
                {
                    new KeyboardButton("/back")
                    {
                        Text = "Кнопка \"Назад\""
                    }
                }
            });

            await _botClient.SendTextMessageAsync(user.chatId, message.ToString(),
                ParseMode.Markdown, replyMarkup: inlineKeyboard);
        }
    }
}
