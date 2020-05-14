using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Encord.ChannelService.Models;

namespace Encord.ChannelService.MapProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Channel, TextChannel>();
            CreateMap<Channel, VoiceChannel>();
        }
    }
}
