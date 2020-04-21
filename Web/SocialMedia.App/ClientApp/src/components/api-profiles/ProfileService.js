import axios from 'axios';

class ProfileService {

    getFollowersCount(followers) {
        let count = 0;

        if(followers != null) {
            count = followers.filter(f => f.isApproved === true).length;
        }

        return count;
    }

    getFollowingsCount(followings) {
        let count = 0;

        if(followings != null) {
            count = followings.filter(f => f.isApproved === true).length;
        }

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

    async uploadProfilePicture(userId, picture) {
        let data = new FormData();
        data.append("picture", picture);
        data.set("id", userId);

        let result = await axios.post('/api/Profiles/ProfilePicture', data, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });

        return result;
    }

    async getFollowers(username) {
        let result = await axios.get(`/api/Profiles/Followers?username=${username}`);
        return result;
    }

    async getFollowings(username) {
        let result = await axios.get(`/api/Profiles/Followings?username=${username}`);
        return result;
    }

    async getProfile(username) {
        let result = await axios.get(`/api/Profiles/Get?username=${username}`)
        return result;
    }

    async getProfilePosts(username) {
       let result = await axios.get(`/api/Posts/Profile?username=${username}`);
       return result;
    }

    async getRequests(id) {
        let result = await axios.get(`api/Profiles/Requests?id=${id}`);
        return result;
    }

    async approveRequest(username) {
        let result = await axios.post(`api/Profiles/Approve?username=${username}`);
        return result;
    }

    async deleteRequest(username) {
        let result = await axios.post(`api/Profiles/Delete?username=${username}`)
        return result;
    }

    async searchUsers(query) {
        let result = await axios.post(`api/Profiles/Search?searchString=${query}`);
        return result;
    }
}

const profileService = new ProfileService();

export default profileService;