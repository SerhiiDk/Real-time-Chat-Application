using ChatApplication.API.V1.Hubs;
using ChatApplication.API.V1.Services.MessageService;
using ChatApplication.API.V1.Services.TextAnalyzeService;
using ChatApplication.Shared.V1.Dtos;
using ChatApplication.Shared.V1.Models.JoinModels;
using ChatApplication.Shared.V1.Models.NotificationModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApplication.API.V1.Controllers;

public class RealTimeController : BaseApiController
{
    private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;

    public RealTimeController(IHubContext<NotificationHub, INotificationClient> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost(nameof(SendBroadcastMessage))]
    public async Task SendBroadcastMessage(string message, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.All.BroadcastMessage(message, cancellationToken);
    }

    [HttpPost(nameof(JoinChatById))]
    public async Task<ActionResult<List<MessageDTO>>> JoinChatById([FromServices] IMessageService service, [FromBody] JoinChatModel model, CancellationToken cancellationToken)
    {
        await _hubContext.Groups.AddToGroupAsync(model.ConnectionId, model.ChatId.ToString(), cancellationToken);

        var result = await service.GetMessagesByChatId(model.ChatId, cancellationToken);
        if (result.Count != 0)
        {
            return Ok(result);
        }
        return NoContent();
    }

    [HttpPost(nameof(SendChatNotification))]
    public async Task<ActionResult> SendChatNotification([FromServices] ITextAnalyzeService service, [FromBody]ChatNotificationModel model, CancellationToken cancellationToken)
    {
        model.MessageSentiment = await service.HandleMessage(model, cancellationToken);
        await _hubContext.Clients.Group(model.ChatId.ToString()).SendChatNotification(model, cancellationToken);
        return NoContent(); 
    }
}
