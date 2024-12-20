using BlazorBootstrap;
using ChatApplication.Shared.V1.Dtos;
using ChatApplication.Shared.V1.Models.JoinModels;
using ChatApplication.Shared.V1.Models.NotificationModels;
using ChatApplication.Shared.V1.Models.SignalRMethods;
using ChatApplication.UI.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace ChatApplication.UI.Pages.Chat;
public partial class CommonChat
{
    [Inject] 
    protected ToastService ToastService { get; set; } = default!;
    [Inject]
    private HttpClient HttpClient { get; set; } = default!;
    [Inject]
    private IAccountService AccountService { get; set; } = default!;
    [Inject]
    private IConfiguration Configuration { get; set; } = default!;

    private HubConnection? _hubConnection;
    private string MessageText = string.Empty;
    private ObservableCollection<MessageDTO> Messages = new();
    private const int CHAT_ID = 1;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await InitializeSignalRConnection();
    }

    public async Task SendMessage()
    {
        var data = new ChatNotificationModel() { ChatId = CHAT_ID, Message = MessageText, Sender = AccountService.User?.UserName, MessageSentiment = MessageSentiment.None};

        var response = await PostAsync("/api/realtime/v1/RealTime/SendChatNotification", data);
        if (!response.IsSuccessStatusCode)
        {
            ToastService.Notify(new(ToastType.Warning, $"Error has occurred"));
        }
        MessageText = string.Empty;
    }

    public async Task JoinChat()
    {
        if (_hubConnection?.ConnectionId == null || AccountService.User?.UserName == null)
        {
            ToastService.Notify(new(ToastType.Warning, $"Unable to join chat: Missing connection or user information."));
            return;
        }

        var joinChatModel = new JoinChatModel() { ChatId = CHAT_ID, ConnectionId = _hubConnection.ConnectionId, UserName = AccountService.User?.UserName };
        var response = await PostAsync("/api/realtime/v1/RealTime/JoinChatById", joinChatModel);

        if (response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var result = await response.Content.ReadFromJsonAsync<List<MessageDTO>>();

            if (result != null && result?.Count != 0)
            {
                Messages = new(result);
            }
        }
    }
    private async Task InitializeSignalRConnection()
    {
        try
        {
            var url = Configuration.GetSection("SignalR").GetValue<string>("Connection")!;
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();
            _hubConnection.On<ChatNotificationModel>(nameof(SignalRMethodName.SendChatNotification), async (model) =>
            {
                ProcessIncomingMessage(model);
                StateHasChanged();
            });

            await _hubConnection.StartAsync();
            await JoinChat();
        }
        catch (Exception)
        {

            ToastService.Notify(new(ToastType.Warning, $"An error occurred while connecting to the chat"));
        }
    }

    private void ProcessIncomingMessage(ChatNotificationModel model)
    {
        if (string.IsNullOrEmpty(model.Message))
            return;

        Messages.Add(new MessageDTO() { Message = model.Message, Sender = model.Sender, Sentiment = model.MessageSentiment.ToString() });
    }

    private async Task<HttpResponseMessage> PostAsync<T>(string url, T data)
    {
        var content = JsonContent.Create(data);
        return await HttpClient.PostAsync(url, content);
    }

}