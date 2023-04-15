
using datatest4.Data;
using datatest4.Interfaces;
using datatest4.Models;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace datatest4.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AppUser> GetOrCreate(Update update)
        {
            var newUser = update.Type switch
            {
                UpdateType.CallbackQuery => new AppUser
                {
                    UserName = update.CallbackQuery.From.Username,
                    chatId = update.CallbackQuery.Message.Chat.Id,
                    FirstName = update.CallbackQuery.Message.From.FirstName,
                    LastName = update.CallbackQuery.Message.From.LastName,
                    Imei = update.Message.Text
                },
                UpdateType.Message => new AppUser
                {
                    UserName = update.Message.Chat.Username,
                    chatId = update.Message.Chat.Id,
                    FirstName = update.Message.Chat.FirstName,
                    LastName = update.Message.Chat.LastName,
                    Imei = update.Message.Text
                }
            };
          
            var user = await _context.Users.FirstOrDefaultAsync(u => u.chatId == newUser.chatId);
            if (user != null) return user;

            var result = await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return result.Entity;
        }
    }
}
