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
        private readonly ICloudinaryService cloudinaryService;
        private readonly IRepository<ChatRoom> chatRoomRepository;

        public ProfilesService(
            IRepository<ApplicationUser> userRepository, 
            IRepository<UserFollower> userFollowerRepository,
            ICloudinaryService cloudinaryService,
            IRepository<ChatRoom> chatRoomRepository)
        {
            this.userRepository = userRepository;
            this.userFollowerRepository = userFollowerRepository;
            this.cloudinaryService = cloudinaryService;
            this.chatRoomRepository = chatRoomRepository;
        }

        /*
         A method that allows you to follow somebody through their profile, by clicking the follow button.
         */
        public async Task<string> AddFollowerAsync(AddFollowerModel model)
        {
            var follower = this.userFollowerRepository.All()
                .FirstOrDefault(uf => uf.UserId == model.UserId && uf.FollowerId == model.FollowerId);

            if (follower != null)
            {
                return "You are already following this user";
            }

            var userFollower = new UserFollower
            {
                UserId = model.UserId,
                FollowerId = model.FollowerId,
            };

            await this.userFollowerRepository.AddAsync(userFollower);

            await this.userFollowerRepository.SaveChangesAsync();

            return "You followed the user successfully";
        }

        /*
         Returns the profile of the user with the given username.
         */
        public async Task<UserProfileViewModel> GetUserProfileAsync(string username)
        {
            var user = await this.userRepository
                            .All()
                            .To<UserProfileViewModel>()
                            .FirstOrDefaultAsync(u => u.UserName == username);

            return user;
        }

        /*
         A method that allows you to unfollow somebody through their profile.
         */
        public async Task<string> RemoveFollowerAsync(AddFollowerModel model)
        {
            var follower = this.userFollowerRepository.All()
                .FirstOrDefault(uf => uf.UserId == model.UserId && uf.FollowerId == model.FollowerId);

            if (follower == null)
            {
                return "You are not following this user";
            }

            this.userFollowerRepository.Delete(follower);

            await this.userFollowerRepository.SaveChangesAsync();

            return "Unfollowed user successfully";
        }

        /*
         A method that returns all the followers of the user with the given username.
         */
        public async Task<IEnumerable<FollowerViewModel>> GetUserFollowersAsync(string username)
        {
            var user = await this.userRepository.All()
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return null;
            }

            var followers = await this.userFollowerRepository.All()
                .Where(f => f.User.UserName == username)
                .To<FollowerViewModel>()
                .ToListAsync();

            return followers;
        }

        /*
         A method that returns all the people that a user follows.
         */
        public async Task<IEnumerable<FollowingViewModel>> GetUserFollowingsAsync(string username)
        {
            var user = await this.userRepository.All()
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return null;
            }

            var followings = await this.userFollowerRepository.All()
                .Where(f => f.Follower.UserName == username)
                .To<FollowingViewModel>()
                .ToListAsync();

            return followings;
        }

        public async Task<string> UploadProfilePicture(UploadPictureInputModel model)
        {
            var username = model.Username;
            var picture = model.Picture;

            var user = this.userRepository.All()
                .FirstOrDefault(u => u.UserName == username);

            var publicId = user.PicturePublicId;

            // Delete the current profile picture from the cloud if the user has one.
            if (publicId != null)
            {
                await this.cloudinaryService.DeleteFileAsync(publicId);
            }

            // Upload the new picture to the cloud
            var uploadResult = await this.cloudinaryService.UploadFileAsync(picture, user.Id);

            var profilePictureUrl = uploadResult.SecureUri.ToString();
            var picturePublicId = uploadResult.PublicId;

            user.ProfilePictureUrl = profilePictureUrl;
            user.PicturePublicId = picturePublicId;

            this.userRepository.Update(user);
            await this.userRepository.SaveChangesAsync();

            return profilePictureUrl;
        }

        public async Task<IEnumerable<ProfileSearchViewModel>> SearchProfilesAsync(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                throw new ArgumentNullException("The search string is empty.");
            }

            var searchResult = await this.userRepository.All()
                .Where(s => s.UserName.StartsWith(searchString))
                .Take(5)
                .To<ProfileSearchViewModel>()
                .ToListAsync();

            return searchResult;
        }
    }
}
