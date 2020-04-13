import axios from 'axios';

class ProfileService {

    getFollowersCount(followers) {
        let count = followers.filter(f => f.isApproved === true).length;

        return count;
    }

    getFollowingsCount(followings) {
        let count = followings.filter(f => f.isApproved === true).length;

        return count;
    }

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
        if (followers.some(f => currentUserName === f.followerUserName && f.isApproved === true)) {
            return true;
        }

        return false;
    }

    async isRequested(currentUserName, followers) {
        if (followers.some(f => currentUserName === f.followerUserName && f.isApproved === false)) {
            return true;
        }

        return false;
    }
}

const profileService = new ProfileService();

export default profileService;