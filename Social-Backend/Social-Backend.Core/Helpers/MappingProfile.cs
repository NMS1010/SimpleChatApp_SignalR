using AutoMapper;
using Microsoft.AspNetCore.Http;
using Social_Backend.Application.Common.Models.Paging;
using Social_Backend.Application.Dtos;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Interfaces;
using Social_Backend.Core.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Helpers
{
    public class MappingProfile : Profile
    {
        private const string USER_CONTENT_FOLDER = "user-content";

        public string GetFile(string filename, IHttpContextAccessor httpContextAccessor)
        {
            var req = httpContextAccessor.HttpContext.Request;
            var path = $"{req.Scheme}://{req.Host}/{USER_CONTENT_FOLDER}/{filename}";
            return path;
        }

        public MappingProfile(IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService)
        {
            CreateMap<AppUser, UserDTO>()
                .ForMember(des => des.UserId,
                act => act.MapFrom(src => src.Id))
                .ForMember(des => des.Avatar,
                act => act.MapFrom(src => GetFile(src.Avatar, httpContextAccessor)));
            CreateMap<Chat, ChatDTO>()
                .ForMember(des => des.Avatar,
                act => act.MapFrom(src => GetFile(src.Avatar, httpContextAccessor)))
                .ForMember(des => des.OnlyMe,
                act => act.MapFrom(src => src.UserChats.Count < 2))
                .ForMember(des => des.MemberIds,
                act => act.MapFrom(src => src.UserChats.Select(x => x.UserId)))
                .ForMember(des => des.RoomId,
                act => act.MapFrom(src => string.Join("&", src.UserChats.Select(x => x.UserId).ToArray())));
            CreateMap<UserChat, UserChatDTO>();

            CreateMap<Message, MessageDTO>()
                .ForMember(des => des.IsMe,
                act => act.MapFrom(src => src.UserId == currentUserService.UserId))
                .ForMember(des => des.UserAvatar,
                act => act.MapFrom(src => GetFile(src.User.Avatar, httpContextAccessor)))
                .ForMember(des => des.File,
                act => act.MapFrom(src => GetFile(src.File, httpContextAccessor)));
        }
    }
}