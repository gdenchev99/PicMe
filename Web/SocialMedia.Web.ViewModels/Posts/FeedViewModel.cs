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

        public string CreatedOn { get; set; }

        public string MediaExtension { get; set; }

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
                m => m.CreatedOn,
                opt => opt.MapFrom(x => x.CreatedOn.ToString("dd MMMM yyyy 'at' HH:MM")));

            configuration.CreateMap<Post, FeedViewModel>().ForMember(
                m => m.MediaExtension,
                opt => opt.MapFrom(x => x.MediaSource.Substring(x.MediaSource.LastIndexOf("."))));
        }
    }
}
