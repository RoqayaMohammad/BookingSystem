using AutoMapper;
using DAL.Models;
using webApi.DTOs;

namespace webApi.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles() 
        {
            CreateMap<BranchDto, Branch>().ReverseMap();
            CreateMap<Room, RoomDto>()
            //.ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Location))
            .ReverseMap()
            ; 
            CreateMap<Booking,BookindDTO>().ReverseMap();
        }
    }
}
