namespace SocialMedia.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using AutoMapper.Configuration.Annotations;
    using SocialMedia.Data.Models;
    using SocialMedia.Services.Mapping;
    using SocialMedia.Web.ViewModels.Comments;

    public class FeedViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public string CreatorUserName { get; set; }

        public string CreatorFirstName { get; set; }

        public string CreatorLastName { get; set; }

        public string CreatorProfilePictureUrl { get; set; }

        public string CreatedOnFormat { get; set; }

        public string MediaSource { get; set; }

        public string MediaPublicId { get; set; }

        public string Description { get; set; }

        public string Id { get; set; }

        public int CommentsCount { get; set; }

        public int LikesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Post, FeedViewModel>().ForMember(
                m => m.CommentsCount,
                opt => opt.MapFrom(x => x.Comments.Count));

            configuration.CreateMap<Post, FeedViewModel>().ForMember(
                m => m.LikesCount,
                opt => opt.MapFrom(x => x.Likes.Count));

            configuration.CreateMap<Post, FeedViewModel>().ForMember(
                m => m.CreatedOnFormat,
                opt => opt.MapFrom(x => x.CreatedOn.ToString("dd MMMM yyyy 'at' HH:MM")));
        }
    }
}
