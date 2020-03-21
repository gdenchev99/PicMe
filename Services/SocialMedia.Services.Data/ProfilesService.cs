namespace SocialMedia.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SocialMedia.Data.Common.Repositories;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;
    using SocialMedia.Web.ViewModels.Profiles;

    public class ProfilesService : IProfilesService
    {
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IRepository<UserFollower> userFollowerRepository;

        public ProfilesService(IRepository<ApplicationUser> userRepository, IRepository<UserFollower> userFollowerRepository)
        {
            this.userRepository = userRepository;
            this.userFollowerRepository = userFollowerRepository;
        }

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

        public async Task<UserProfileViewModel> GetUserProfileAsync(string username)
        {
            var user = await this.userRepository
                            .All()
                            .To<UserProfileViewModel>()
                            .FirstOrDefaultAsync(u => u.UserName == username);

            return user;
        }

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
    }
}
