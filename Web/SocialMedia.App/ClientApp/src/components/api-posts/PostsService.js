import axios from 'axios';

export class PostsService {

    async createPost(creatorId, description, media) {
        let data = new FormData();
        data.set("creatorId", creatorId);
        data.set("description", description);
        data.append("mediaSource", media);

        let result = await axios.post("/api/Posts/Create", data, {
            headers: {
                'Content-Type': 'multipart/form-data',
            }
        });

        return result;
    }

    async fetchFeedPosts(userId, skipCount, takeCount) {
        let result = await axios.get(`/api/Posts/Feed?id=${userId}
        &skipCount=${skipCount}
        &takeCount=${takeCount}`);

        return result;
    }
}

const postsService = new PostsService();
export default postsService;