using ChatApplication.Shared.V1.Dtos;

namespace ChatApplication.API.V1.Services.MessageService;

public interface IMessageService
{
    Task<List<MessageDTO>> GetMessagesByChatId(int chatId, CancellationToken cancellation);
}
