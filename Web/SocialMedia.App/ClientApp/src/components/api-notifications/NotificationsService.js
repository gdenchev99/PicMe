import * as signalR from '@aspnet/signalr';
import axios from 'axios';

class NotificationsService {
    _connection = new signalR.HubConnectionBuilder().withUrl("/notificationsHub").build();

    async getNotifications(userId, skipCount, takeCount) {
        let result = await axios.get(`api/Notifications/Get?userId=${userId}
        &skipCount=${skipCount}
        &takeCount=${takeCount}`);

        return result.data;
    }

    async getUnreadNotifications(userId) {
        let result = await axios.get(`api/Notifications/Unread?userId=${userId}`);

        return result.data;
    }

    async setStatusRead(userId) {
        await axios.post(`api/Notifications/UpdateStatus?userId=${userId}`)
            .catch(e => console.log(e));

        let notificationIcon = document.getElementsByClassName("unread")[0];
        if (notificationIcon !== undefined) {
            notificationIcon.remove();
        }
    }

    async startConnection(userId) {
        // Start the connection if it's not started already.
        if (this._connection.state === 0) {
            await this._connection.start()
                .then(() => {
                    console.log("Connection established");
                    this.mapConnectionIdWithUserId(userId);
                })
                .catch(() => console.log("Connection failed."));
        }

        return this._connection;
    }

    mapConnectionIdWithUserId(userId) {
        this._connection.invoke("MapConnectionAndUserIds", userId);
    }

    invokeNotificationMessage(receiverId, postId, info) {
        this._connection.invoke("Notify", receiverId, postId, info);
    }
}

const notificationsService = new NotificationsService();

export default notificationsService;