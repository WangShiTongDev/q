using AutoMapper;
using DevTeamUp.BLL.DTOs;
using DevTeamUp.Models;
using DevTeamUp.Models.Profile;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.AutoMapper
{
    public class ViewModelMapper : Profile
    {
        public ViewModelMapper()
        {
            CreateMap<ProfileDTO, ProfileInitVM>()
                .ForMember(p => p.AvailableSkills, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CreateProjectViewModel, CreatedProjectDTO>()
                .ForMember( p => p.SkillsIds, opt => opt.MapFrom(d => d.SelectedSkillsIds));

            CreateMap<ProfileInitVM, ProfileDTO>()
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.SelectedSkills.Select(id => new SkillDTO { Id = id })));

            CreateMap<ProfileDTO, ProfileViewModel>();
            CreateMap<SkillDTO, SkillViewModel>().ReverseMap();

            CreateMap<ProjectPageDTO, ProjectPageViewModel>();
                //.ForMember(p => p.Members, opt => opt.Ignore())
                //.ForMember(p => p.OwnerProfile, opt => opt.Ignore());
        }
    }
}
