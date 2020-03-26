import axios from 'axios';

class ProfileService {

    async addFollower(userId, followerId) {
        let data = {
            userId: userId,
            followerId: followerId
        };

        let result = await axios.post("/api/Profiles/Follow", data, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });

        return result;
    }

    async removeFollower(userId, followerId) {
        let data = {
            userId: userId,
            followerId: followerId
        };

        let result = await axios.post("/api/Profiles/Unfollow", data, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })

        return result;
    }

    async isFollowing(currentUserName, followers) {
        if (followers.some(f => currentUserName === f.followerUserName)) {
            return true;
        }

        return false;
    }
}

const profileService = new ProfileService();

export default profileService;