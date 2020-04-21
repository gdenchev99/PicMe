namespace SocialMedia.Web.ViewModels.Administration
{
    using AutoMapper;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;
    using System;

    public class UsersViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CreatedOn { get; set; }

        public DateTimeOffset LockoutEnd { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UsersViewModel>().ForMember(
                m => m.CreatedOn,
                opt => opt.MapFrom(u => u.CreatedOn.ToString("dd/MM/yyyy")));
        }
    }
}
