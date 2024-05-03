using AutoMapper;
using Etickets.ResopnseModel;
using LeaveManagement4.DB.Entity;
using LeaveManagement4.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LeaveManagement4.Profiles
{
	public class Profiles : Profile
	{
		public Profiles()
		{
			CreateMap<int, string>();
			
			CreateMap < EntityEntry<User>, UserAddDto>();
			CreateMap < EntityEntry<User>, UserViewDto>();
			CreateMap<User, UserViewDto>();
			CreateMap<UserAddDto, User>();
			CreateMap<UserViewDto,User>();

			CreateMap<LeaveType, LeaveTypeAddDto>();
			CreateMap<EntityEntry<LeaveType>, LeaveTypeViewDto>();
			
			CreateMap<LeaveType, LeaveTypeViewDto>();
			CreateMap<LeaveTypeAddDto,LeaveType>();
			CreateMap<LeaveTypeViewDto,LeaveType>();
			CreateMap<LeaveTypeViewDto,LeaveTypeAddDto>();

			CreateMap<Leave, LeaveAddDto>().ReverseMap();
			CreateMap<EntityEntry<Leave>, LeaveViewDto>().ReverseMap();
			CreateMap<Leave, LeaveViewDto>().ReverseMap();
			CreateMap<ResponseModel<AssignLeaveViewDto>, AssignLeave>();
			CreateMap<AssignLeave, AssignLeaveAddDto>().ReverseMap();
			CreateMap<AssignLeave, AssignLeaveViewDto>().ReverseMap();
			CreateMap<EntityEntry<AssignLeave>, AssignLeaveViewDto>().ReverseMap();
			
		}
	}
}
