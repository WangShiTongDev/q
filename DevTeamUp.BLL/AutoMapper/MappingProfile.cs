using AutoMapper;
using DevTeamUp.BLL.DTOs;
using DevTeamUp.DAL.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.BLL.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatedProjectDTO, Project>().ReverseMap();
            CreateMap<ProjectDTO, Project>()
                .ReverseMap();
        
            CreateMap<SkillDTO, Skill>().ReverseMap();

            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<ReviewDTO, Review>().ReverseMap();

            CreateMap<ProfileDTO, User>().ReverseMap();

            CreateMap<User, ProfileListItemDTO>();


            CreateMap<Project, ProjectPageDTO>()
                .ForMember(p => p.OwnerProfile, opt => opt.MapFrom(s => s.Owner))
                .ForMember(p => p.Members, opt => opt.MapFrom(s => s.Members));
        }

    }
}
