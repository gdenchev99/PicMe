import axios from 'axios';

export class CommentsService {
    async getComments(postId, skipCount, takeCount) {
        let result = await axios.get(`/api/Comments/All?postId=${postId}
        &skipCount=${skipCount}
        &takeCount=${takeCount}`);

        return result;
    }

    async deleteComment(id) {
        let result = await axios.post(`/api/Comments/Delete?id=${id}`, null, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });

        return result;
    }

    async getFeedComments(postId) {
        let result = await axios.get(`/api/Comments/Feed?postId=${postId}`);
        return result;
    }

    async createComment(userId, postId, text) {
        let data = {
            creatorId: userId,
            postId: postId,
            text: text
        };

        let result = await axios.post("api/Comments/Create", data, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });

        return result;
    }
}

const commentsService = new CommentsService();
export default commentsService;