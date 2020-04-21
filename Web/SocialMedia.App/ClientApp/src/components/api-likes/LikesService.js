import axios from 'axios';

export class LikesService {
    async addLike(userId, postId) {
        let data = {userId: userId, postId: postId};

        let result = await axios.post("/api/Likes/Add", data, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });
        
        return result;
    }

    async removeLike(userId, postId) {
        let data = { userId: userId, postId: postId };

        let result = await axios.post("/api/Likes/Remove", data, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });

        return result;
    }

    async isLiked(userId, postId) {
        let result = await axios.get("/api/Likes/Liked", {
            params: {
                userId: userId,
                postId: postId
            }
        });

        return result.data; // Boolean
    }

    async latestLikes(postId) {
        let result = await axios.get(`/api/Likes/Feed?postId=${postId}`);
        return result.data; // Return just the data.
    }
}

const likesService = new LikesService();
export default likesService;