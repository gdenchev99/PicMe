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

        public ProfilesService(IRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserProfileViewModel> GetUserProfileAsync(string username)
        {
            var user = await this.userRepository
                            .All()
                            .To<UserProfileViewModel>()
                            .FirstOrDefaultAsync(u => u.UserName == username);

            return user;
        }
    }
}
