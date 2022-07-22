using Business;
using Business.Services.Matches;
using Business.Services.MatchMaking;
using Business.Services.MatchQueue;
using Business.Services.MatchSimulations;
using Business.Services.Player;
using Business.Services.Rating;
using Business.Technical;
using DAL.Models;
using Hangfire;
using Hangfire.Storage.SQLite;
using HotChocolate.AspNetCore.Voyager;
using Microsoft.EntityFrameworkCore;
using WebApi.GraphQL;
using WebApi.Grpc;
using WebApi.HostedService;
using WebApi.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSwaggerGen();
builder.Services.AddDbContextFactory<MatchMakingContext>(opts => opts.UseLazyLoadingProxies().UseSqlite(builder.Configuration["SQLite:Main"],o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
builder.Services.AddDbContext<MatchMakingContext>(opts => opts.UseLazyLoadingProxies().UseSqlite(builder.Configuration["SQLite:Main"],o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IMatchMakingResolver, TrivialMatchMakingResolver>();
builder.Services.AddScoped<IMatchQueueService, MatchQueueService>();
builder.Services.AddScoped<IMatchSimulationService, MatchSimulationService>();
builder.Services.AddSingleton<SocketService>();
builder.Services.AddAutoMapper(typeof(BusinessMappingProfile), typeof(MapProfile));
builder.Services.AddHostedService<MatchResolver>();
builder.Services.AddHangfire(configuration => configuration
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSQLiteStorage(builder.Configuration["SQLite:Hangfire"]));
builder.Services.AddHangfireServer();
builder.Services.AddGrpc();
builder.Services.AddSignalR();

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .RegisterDbContext<MatchMakingContext>();


var devCorsPolicy = "_devCorsPolicy";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy,
        corsPolicyBuilder =>
        {
            corsPolicyBuilder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:4044")
                .AllowCredentials()
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
app.MapHub<MatchHub>("/matchHub");
app.MapGraphQL();
app.UseVoyager("/graphql","/voyager");

//apply migrations on startup
app.Services.CreateScope().ServiceProvider.GetRequiredService<MatchMakingContext>().Database.Migrate();

//RecurringJob.AddOrUpdate<IMatchService>($"ResolveAllUnresolvedMatch",x=>x.ResolveAllUnresolvedMatches(),"0/20 * * ? * *");

app.Run();