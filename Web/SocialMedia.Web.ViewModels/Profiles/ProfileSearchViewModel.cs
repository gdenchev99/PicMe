namespace SocialMedia.Web.ViewModels.Profiles
{
    using AutoMapper;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;

    public class ProfileSearchViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string UserName { get; set; }

        public string FullName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, ProfileSearchViewModel>().ForMember(
                m => m.FullName,
                opt => opt.MapFrom(x => x.FirstName + " " + x.LastName));
        }
    }
}
