using datatest4.Models;
using Telegram.Bot.Types;

namespace datatest4.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetOrCreate(Update update);
    }
}
