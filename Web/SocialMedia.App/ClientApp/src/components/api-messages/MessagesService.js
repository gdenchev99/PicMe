import axios from 'axios';

export class MessagesService {
    async getChatRooms(userId) {
        let result = await axios.get(`/api/Messages/ChatRooms?userId=${userId}`);
        return result;
    }

    async getMessages(currentId, receiverUsername) {
        let result = await axios.get(`api/Messages/ChatRoom?currentId=${currentId}&receiverUsername=${receiverUsername}`);
        return result;
    }
}

const messagesService = new MessagesService();
export default messagesService;