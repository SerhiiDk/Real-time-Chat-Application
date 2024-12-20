using ChatApplication.DataAccess.Context;
using ChatApplication.Shared.V1.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.API.V1.Services.MessageService;

public class MessageService : IMessageService
{
    private readonly ChatDbContext _context;
    public MessageService(ChatDbContext context)
    {
        _context = context;
    }

    public async Task<List<MessageDTO>> GetMessagesByChatId(int chatId, CancellationToken cancellation)
    {
        var messages = await _context.Messages
            .Where(x => x.ChatId == chatId)
            .Select(x => new MessageDTO
            {
                Sender = x.Sender.UserName,
                CreatedAt = x.CreateAt,
                Message = x.Content,
                Sentiment = x.Sentiment
            })
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellation);

        return messages;
    }
}
