using AlwaysForum.Extensions;
using AutoMapper;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Services.Messages;
using Services.Users;

namespace AlwaysForum.Hubs; 

public class ChatHub : Hub {
    private readonly IMessagesService messagesService;
    private readonly IMapper mapper;

    public ChatHub(IMessagesService messagesService, IMapper mapper) {
        this.messagesService = messagesService;
        this.mapper = mapper;
    }

    public async Task SendMessage(string message) {
        Message sentMessage = await messagesService.SendAsync(Context.User.GetId(), message);

        MessageViewModel messageModel = mapper.Map<MessageViewModel>(sentMessage);
        messageModel.AuthorName = Context.User.Identity.Name;

        await Clients.All.SendAsync("ReceiveMessage", messageModel);
    }
}