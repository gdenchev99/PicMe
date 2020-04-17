namespace SocialMedia.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SocialMedia.Data.Common.Repositories;
    using SocialMedia.Data.Models;
    using SocialMedia.Services;
    using SocialMedia.Services.Mapping;
    using SocialMedia.Web.ViewModels.Profiles;

    public class ProfilesService : IProfilesService
    {
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IRepository<UserFollower> userFollowerRepository;

        public ProfilesService(
            IRepository<ApplicationUser> userRepository,
            IRepository<UserFollower> userFollowerRepository)
        {
            this.userRepository = userRepository;
            this.userFollowerRepository = userFollowerRepository;
        }

        /*
         A method that allows you to follow somebody through their profile, by clicking the follow button.
         */
        public async Task<bool> AddFollowerAsync(AddFollowerModel model)
        {
            var userToFollow = this.userRepository.All().FirstOrDefault(x => x.Id == model.UserId);

            var isApproved = false;

            if (userToFollow.IsPrivate == false)
            {
                isApproved = true;
            }

            var userFollower = new UserFollower
            {
                UserId = model.UserId,
                FollowerId = model.FollowerId,
                IsApproved = isApproved,
            };

            await this.userFollowerRepository.AddAsync(userFollower);

            var result = await this.userFollowerRepository.SaveChangesAsync() > 0;

            return result;
        }

        /*
         Returns the profile of the user with the given username.
         */
        public async Task<UserProfileViewModel> GetUserProfileAsync(string username)
        {
            var profile = await this.userRepository
                            .All()
                            .To<UserProfileViewModel>()
                            .FirstOrDefaultAsync(u => u.UserName == username);

            return profile;
        }

        /*
         A method that allows you to unfollow somebody through their profile.
         */
        public async Task<string> RemoveFollowerAsync(AddFollowerModel model)
        {
            var follower = this.userFollowerRepository.All()
                .FirstOrDefault(uf => uf.UserId == model.UserId && uf.FollowerId == model.FollowerId);

            this.userFollowerRepository.Delete(follower);

            await this.userFollowerRepository.SaveChangesAsync();

            return "Unfollowed user successfully";
        }

        /*
         A method that returns all the followers of the user with the given username.
         */
        public async Task<IEnumerable<FollowerViewModel>> GetUserFollowersAsync(string username)
        {
            var followers = await this.userFollowerRepository.All()
             .Where(f => f.User.UserName == username && f.IsApproved == true)
             .To<FollowerViewModel>()
             .ToListAsync();

            return followers;
        }

        /*
         A method that returns all the people that a user follows.
         */
        public async Task<IEnumerable<FollowingViewModel>> GetUserFollowingsAsync(string username)
        {
            var followings = await this.userFollowerRepository.All()
                .Where(f => f.Follower.UserName == username && f.IsApproved == true)
                .To<FollowingViewModel>()
                .ToListAsync();

            return followings;
        }

        public async Task<string> UploadProfilePictureAsync(string id, string profilePictureUrl, string picturePublicId)
        {
            var user = this.userRepository.All()
                .FirstOrDefault(u => u.Id == id);

            user.ProfilePictureUrl = profilePictureUrl;
            user.PicturePublicId = picturePublicId;

            this.userRepository.Update(user);
            await this.userRepository.SaveChangesAsync();

            return profilePictureUrl;
        }

        public async Task<IEnumerable<ProfileSearchViewModel>> SearchProfilesAsync(string searchString)
        {
            var searchResult = await this.userRepository.All()
                .Where(s => s.UserName.StartsWith(searchString))
                .Take(5)
                .To<ProfileSearchViewModel>()
                .ToListAsync();

            return searchResult;
        }

        public async Task<IEnumerable<RequestViewModel>> GetFollowerRequestsAsync(string id)
        {
            var requests = await this.userFollowerRepository.All()
                .Where(f => f.UserId == id && f.IsApproved == false)
                .To<RequestViewModel>()
                .ToListAsync();

            return requests;
        }

        public async Task<bool> ApproveRequestAsync(string username)
        {
            var requester = await this.userRepository.All()
                .FirstOrDefaultAsync(u => u.UserName == username);

            var requesterId = requester.Id;

            var follower = await this.userFollowerRepository.All()
                .FirstOrDefaultAsync(uf => uf.FollowerId == requesterId && uf.IsApproved == false);

            follower.IsApproved = true;

            var result = await this.userFollowerRepository.SaveChangesAsync() > 0;

            return result;
        }

        public async Task<bool> DeleteRequestAsync(string username)
        {
            var requester = await this.userRepository.All()
                .FirstOrDefaultAsync(u => u.UserName == username);

            var requesterId = requester.Id;

            var follower = await this.userFollowerRepository.All()
                .FirstOrDefaultAsync(uf => uf.FollowerId == requesterId && uf.IsApproved == false);

            this.userFollowerRepository.Delete(follower);

            var result = await this.userFollowerRepository.SaveChangesAsync() > 0;

            return result;
        }

        public async Task<bool> FollowerExistsAsync(string userId, string followerId)
        {
            var follower = await this.userFollowerRepository.All()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.FollowerId == followerId);

            bool exists = follower != null;

            return exists;
        }

        public async Task<bool> UserExistsByIdAsync(string userId)
        {
            var user = await this.userRepository.All()
                .FirstOrDefaultAsync(u => u.Id == userId);

            bool exists = user != null;

            return exists;
        }

        public async Task<bool> UserExistsByNameAsync(string username)
        {
            var user = await this.userRepository.All()
                .FirstOrDefaultAsync(u => u.UserName == username);

            bool exists = user != null;

            return exists;
        }
    }
}
