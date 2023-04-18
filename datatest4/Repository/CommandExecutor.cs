using datatest4.Commands;
using datatest4.Data;
using datatest4.Interfaces;
using Telegram.Bot.Types;

namespace datatest4.Repository
{
    public class CommandExecutor : ICommandExecutor
    {

        private readonly List<BaseCommand> _commands;
        private BaseCommand _lastCommand;
        public CommandExecutor(IServiceProvider serviceProvider)
        {
            _commands = serviceProvider.GetServices<BaseCommand>().ToList();
        }

        public async Task ExecuteAsync(Update update)
        {
            if (update.Message != null && update.Message.Text.Contains(CommandNames.StartCommand))
            {
                await ExecuteCommand(CommandNames.StartCommand, update);
                return;
            }

            if (update.Message != null && update.Message.Text.Contains(CommandNames.MainMenuCommand))
            {
                await ExecuteCommand(CommandNames.MainMenuCommand, update);
                return;
            }
            
            if (update.Message != null && update.Message.Text.Contains(CommandNames.EnterDayCommand))
            {
                await ExecuteCommand(CommandNames.EnterDayCommand, update);
                return;
            }

            if (update.Message != null && update.Message.Text.Contains(CommandNames.TopTenCommand))
            {
                await ExecuteCommand(CommandNames.TopTenCommand, update);
                return;
            }

            switch (_lastCommand?.Name)
            {
                case CommandNames.GetWalkPerDateCommand:
                    {
                        await ExecuteCommand(CommandNames.GetWalkPerDateCommand, update);
                        break;
                    }
                case CommandNames.StartCommand:
                    {
                        await ExecuteCommand(CommandNames.MainMenuCommand, update);
                        break;
                    }
                case CommandNames.EnterDayCommand:
                    {
                        await ExecuteCommand(CommandNames.GetWalkPerDateCommand, update);
                        break;
                    }
            }
        }

        private async Task ExecuteCommand(string commandName, Update update)
        {
            _lastCommand = _commands.First(c => c.Name == commandName);
            await _lastCommand.ExecuteAsync(update);
        }


    }
}
