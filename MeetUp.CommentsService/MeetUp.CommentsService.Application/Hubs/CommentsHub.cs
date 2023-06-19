using MeetUp.CommentsService.Infrastructure.Models;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace MeetUp.CommentsService.Application.Hubs
{
    public class CommentsHub : Hub
    {
        public async Task SetGroupConnectionAsync(string groupname)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
        }

        public async Task SendCommentAsync(string groupname, Comment comment)
        {
            await Clients.Group(groupname).SendAsync(JsonSerializer.Serialize(comment));
        }
    }
}
