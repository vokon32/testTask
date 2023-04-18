using datatest4;
using datatest4.Commands;
using datatest4.Data;
using datatest4.Interfaces;
using datatest4.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Db")), ServiceLifetime.Singleton);

builder.Services.AddSingleton<TelegramBot>();

builder.Services.AddSingleton<ITrackLocationRepository, TrackLocationRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ICommandExecutor, CommandExecutor>();

builder.Services.AddSingleton<BaseCommand, StartCommand>();
builder.Services.AddSingleton<BaseCommand, MainMenuCommand>();
builder.Services.AddSingleton<BaseCommand, TopTenCommand>();
builder.Services.AddSingleton<BaseCommand, EnterDateCommand>();
builder.Services.AddSingleton<BaseCommand, WalkPerDateCommand>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.Services.GetRequiredService<TelegramBot>().GetBot().Wait();



app.UseAuthorization();

app.MapControllers();

app.Run();
