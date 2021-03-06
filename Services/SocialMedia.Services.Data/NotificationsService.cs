﻿namespace SocialMedia.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using SocialMedia.Data.Common.Repositories;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;
    using SocialMedia.Web.ViewModels.Notifications;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class NotificationsService : INotificationsService
    {
        private readonly IRepository<Notification> repository;

        public NotificationsService(IRepository<Notification> repository)
        {
            this.repository = repository;
        }

        public async Task CreateNotificationAsync(string userId, int? postId, string info)
        {
            var notification = new Notification
            {
                UserId = userId,
                PostId = postId,
                Info = info,
            };

            await this.repository.AddAsync(notification);
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<NotificationViewModel>> GetNotificationsAsync(string userId, int skipCount, int takeCount)
        {
            var notifications = await this.repository.All()
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedOn)
                .Skip(skipCount)
                .Take(takeCount)
                .To<NotificationViewModel>()
                .ToListAsync();

            return notifications;
        }

        public async Task<int> GetUnreadNotificationsCountAsync(string userId)
        {
            int unreadCount = await this.repository.All()
                .Where(n => n.UserId == userId && n.Read == false)
                .CountAsync();

            return unreadCount;
        }

        public async Task<string> UpdateReadStatusAsync(string userId)
        {
            var notifications = await this.repository.All()
                .Where(n => n.UserId == userId && n.Read == false)
                .ToListAsync();

            if (notifications.Count == 0)
            {
                return "There are no new notifications";
            }

            foreach (var notification in notifications)
            {
                notification.Read = true;
            }

            var result = await this.repository.SaveChangesAsync() > 0;

            return string.Empty;
        }
    }
}
