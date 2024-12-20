using ChatApplication.API.Infrastructure.AzureSignalRSettings;
using ChatApplication.API.Infrastructure.CORS;
using ChatApplication.API.V1.Hubs;
using ChatApplication.API.V1.Services.HubService;
using ChatApplication.API.V1.Services.MessageService;
using ChatApplication.API.V1.Services.TextAnalyzeService;
using ChatApplication.API.V1.Services.UserService;
using ChatApplication.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDefaultCORS();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning();

builder.Services.AddDbContext<ChatDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTION_STRING")));

builder.Services.AddScoped<NotificationHub>();
builder.Services.AddScoped<IHubConnectionService, HubConnectionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITextAnalyzeService, TextAnalyzeService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.RegisterAzureSignalR(builder.Configuration);


var app = builder.Build();

app.UseCors(DefaultCorsSetting.PolicyName);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();
app.MapHub<NotificationHub>("api/realtime/v1/notificationhub");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();