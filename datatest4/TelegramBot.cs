using Telegram.Bot;

namespace datatest4
{
    public class TelegramBot
    {
        private readonly IConfiguration _configuration;
        private TelegramBotClient _botClient;
        public TelegramBot(IConfiguration configuration)
        {
            _configuration = configuration;
            _botClient = GetBot().Result;
        }
        public async Task<TelegramBotClient> GetBot()
        {
            if (_botClient != null)
            {
                return _botClient;
            }
            _botClient = new TelegramBotClient(_configuration["Token"]);
            var hook = $"{_configuration["Url"]}/api/message/update";
            await _botClient.SetWebhookAsync(hook);

            return _botClient;
        }
    }
}
