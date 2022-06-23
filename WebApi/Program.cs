using Business;
using Business.Services.Matches;
using Business.Services.MatchMaking;
using Business.Services.MatchQueue;
using Business.Services.MatchSimulations;
using Business.Services.Player;
using Business.Services.Rating;
using DAL.Models;
using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.EntityFrameworkCore;
using WebApi.Grpc;
using WebApi.HostedService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddSwaggerGen();
builder.Services.AddDbContextFactory<MatchMakingContext>(opts => opts.UseSqlite("Data Source=matchmaking.db"));
builder.Services.AddDbContext<MatchMakingContext>(opts => opts.UseSqlite("Data Source=matchmaking.db"));
builder.Services.AddScoped<IMatchService,MatchService>();
builder.Services.AddScoped<IRatingService,RatingService>();
builder.Services.AddScoped<IPlayerService,PlayerService>();
builder.Services.AddScoped<IMatchMakingResolver,TrivialMatchMakingResolver>();
builder.Services.AddScoped<IMatchQueueService,MatchQueueService>();
builder.Services.AddScoped<IMatchSimulationService,MatchSimulationService>();
builder.Services.AddAutoMapper(typeof(BusinessMappingProfile),typeof(MapProfile));
builder.Services.AddHostedService<MatchResolver>();
builder.Services.AddHangfire(configuration => configuration
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSQLiteStorage());
builder.Services.AddHangfireServer();
builder.Services.AddGrpc();


var devCorsPolicy = "_devCorsPolicy";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy,
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
app.UseCors(devCorsPolicy);

app.UseAuthorization();
app.MapControllers();
app.MapHangfireDashboard();
app.UseGrpcWeb();
app.MapGrpcService<MatchGrpcService>().EnableGrpcWeb();



//apply migrations on startup
app.Services.CreateScope().ServiceProvider.GetRequiredService<MatchMakingContext>().Database.Migrate();

RecurringJob.AddOrUpdate<IMatchService>($"ResolveAllUnresolvedMatch",x=>x.ResolveAllUnresolvedMatches(), Cron.Minutely);

app.Run();

