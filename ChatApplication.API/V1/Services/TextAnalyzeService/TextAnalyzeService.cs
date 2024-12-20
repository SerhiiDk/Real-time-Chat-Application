using Azure.AI.TextAnalytics;
using Azure;
using ChatApplication.Shared.V1.Models.NotificationModels;
using ChatApplication.DataAccess.Context;
using ChatApplication.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.API.V1.Services.TextAnalyzeService;

public class TextAnalyzeService : ITextAnalyzeService
{
    private readonly IConfiguration _configuration;
    private readonly ChatDbContext _context;
    public TextAnalyzeService(IConfiguration configuration, ChatDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<MessageSentiment> HandleMessage(ChatNotificationModel model, CancellationToken cancellation)
    {
        var sentiment = await AnalyzeMessageSentiment(model.Message);
        await SaveMessage(model, sentiment, cancellation);
        return sentiment;  
    }

    private async Task<MessageSentiment> AnalyzeMessageSentiment(string message)
    {
        var apiKey = _configuration.GetSection("TextAnalyticsCredential").GetValue<string>("ApiKey");
        var endpoint = _configuration.GetSection("TextAnalyticsCredential").GetValue<string>("Endpoint");

        var azureCredential = new AzureKeyCredential(apiKey!);
        var client = new TextAnalyticsClient(new Uri(endpoint!), azureCredential);

        DocumentSentiment docSentiment = await client.AnalyzeSentimentAsync(message);

        var maxValue = docSentiment.Sentiment;

        switch (docSentiment.Sentiment)
        {
            case TextSentiment.Positive:
                return MessageSentiment.Positive;
            case TextSentiment.Neutral:
                return MessageSentiment.Neutral;
            case TextSentiment.Negative:
                return MessageSentiment.Negative;
        }

        return MessageSentiment.None;
    }

    private async Task SaveMessage(ChatNotificationModel model, MessageSentiment sentiment, CancellationToken token)
    {
        var user = await _context.Users
            .Where(x => x.UserName == model.Sender)
            .FirstOrDefaultAsync(token);

        if (user is null)
            return;

        var chat = await _context.Chats
            .Where(x => x.Id == model.ChatId)
            .FirstOrDefaultAsync(token);

        var message = new Message
        {
            ChatId = model.ChatId,
            CreateAt = DateTime.UtcNow,
            Content = model.Message,
            Sentiment = sentiment.ToString(),
            SenderId = user.Id,
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();  
    }
}
