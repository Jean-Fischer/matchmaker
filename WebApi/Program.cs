using Business;
using Business.Services.Matches;
using Business.Services.MatchSimulations;
using Business.Services.Player;
using Business.Services.Rating;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddSwaggerGen();
builder.Services.AddDbContextFactory<MatchMakingContext>(opts => opts.UseSqlite("Data Source=matchmaking.db"));
builder.Services.AddDbContext<MatchMakingContext>(opts => opts.UseSqlite("Data Source=matchmaking.db"));
builder.Services.AddScoped<IMatchService,MatchService>();
builder.Services.AddScoped<IRatingService,RatingService>();
builder.Services.AddScoped<IPlayerService,PlayerService>();
builder.Services.AddScoped<IMatchSimulationService,MatchSimulationService>();
builder.Services.AddAutoMapper(typeof(BusinessMappingProfile));
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        builder =>
        {
            builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("*")
                ;
        });
});

builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromMinutes(60);
});



var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
//for the sake of simplicity, should be hardened
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();




//apply migrations on startup
app.Services.CreateScope().ServiceProvider.GetRequiredService<MatchMakingContext>().Database.Migrate();

app.Run();