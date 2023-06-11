using AutoMapper;
using Social_Backend.Application.Dtos;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile(IUploadService uploadService)
        {
            CreateMap<AppUser, UserDTO>();
            CreateMap<Chat, ChatDTO>();
            CreateMap<Message, MessageDTO>().ForMember(des => des.Image,
                act => act.MapFrom(src => uploadService.GetFile(src.Image))); ;
        }
    }
}