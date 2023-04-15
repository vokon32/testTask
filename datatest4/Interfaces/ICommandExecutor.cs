using Telegram.Bot.Types;

namespace datatest4.Interfaces
{
    public interface ICommandExecutor
    {
        Task ExecuteAsync(Update update);
    }
}
