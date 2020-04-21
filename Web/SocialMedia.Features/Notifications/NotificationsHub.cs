namespace SocialMedia.Features.Notifications
{
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;
    using SocialMedia.Services.Data;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class NotificationsHub : Hub<INotificationsClient>
    {
        private readonly INotificationsService service;

        public NotificationsHub(INotificationsService service)
        {
            this.service = service;
        }

        public void MapConnectionAndUserIds(string userId)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

        public async Task Notify(string postCreatorId, int? postId, string info)
        {
            await this.service.CreateNotificationAsync(postCreatorId, postId, info);
            await Clients.Group(postCreatorId).ReceiveNotification(info);
        }
    }
}
