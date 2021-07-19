using CPVS_API.DTO;
using CPVS_API.Models;
using AutoMapper;
using System;

namespace CPVS_API.Helpers.AutoMapper
{
    public class DtoToEfMappingProfile : Profile
    {
        
        public DtoToEfMappingProfile()
        {
            CreateMap<UserForDetailDto, User>();

            CreateMap<UserDetail, UserDetailDto>();
            CreateMap<UserDto, User>();
            CreateMap<TimeLineDto, TimeLine>();

            CreateMap<BuildingDto, Building>();
            CreateMap<BuildingUserDto, BuildingUser>();
            CreateMap<CommentDto, Comment>();

            CreateMap<ToDoListDto, ToDoList>();
            CreateMap<Setting, SettingDTO>();
            CreateMap<RoleDto, Role>();
            CreateMap<RoleUserDto, RoleUser>();
            CreateMap<PlanDto, Plan>();
            //CreateMap<AuditTypeDto, MES_Audit_Type_M>();
        }
    }
}