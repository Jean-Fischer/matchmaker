using Business;
using Business.Services.Matches;
using Business.Services.MatchSimulations;
using Business.Services.Rating;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextFactory<MatchMakingContext>(opts => opts.UseSqlite("Data Source=matchmaking.db"));
builder.Services.AddDbContext<MatchMakingContext>(opts => opts.UseSqlite("Data Source=matchmaking.db"));
builder.Services.AddScoped<IMatchService,MatchService>();
builder.Services.AddScoped<IRatingService,RatingService>();
builder.Services.AddScoped<IMatchSimulationService,MatchSimulationService>();
builder.Services.AddAutoMapper(typeof(BusinessMappingProfile));
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.SetIsOriginAllowed(origin => true).AllowAnyMethod().AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.



app.UseHttpsRedirection();
//for the sake of simplicity, should be hardened
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();



//apply migrations on startup
app.Services.CreateScope().ServiceProvider.GetRequiredService<MatchMakingContext>().Database.Migrate();

app.Run();