using CPVS_API.DTO;
using CPVS_API.Models;
using AutoMapper;
using System.Linq;

namespace CPVS_API.Helpers.AutoMapper
{
    public class EfToDtoMappingProfile : Profile
    {
        char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        public EfToDtoMappingProfile()
        {
            CreateMap<User, UserForDetailDto>();

            CreateMap<UserDetailDto, UserDetail>();
            CreateMap<User, UserDto>();
            CreateMap<TimeLine, TimeLineDto>();

            CreateMap<Building, BuildingDto>();
            CreateMap<BuildingUser, BuildingUserDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<ToDoList, ToDoListDto>();


            // CreateMap<SchedulesUpdate, ScheduleUpdateDto>()
            // .ForMember(d => d.Parts, o => o.MapFrom(x => x.Part));

            CreateMap<SettingDTO, Setting>();
            CreateMap<Role, RoleDto>();
            CreateMap<RoleUser, RoleUserDto>();
            CreateMap<Plan, PlanDto>();
            // CreateMap<ScheduleDto, Schedules>();

        }

    }
}