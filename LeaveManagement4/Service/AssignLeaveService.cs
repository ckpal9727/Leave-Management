using AutoMapper;
using Etickets.ResopnseModel;
using LeaveManagement4.DB.Entity;
using LeaveManagement4.Models;
using LeaveManagement4.Repository;

namespace LeaveManagement4.Service
{
	public class AssignLeaveService : IAssignLeaveService
	{
		private readonly IAssignLeaveRepository _repository;
		private readonly IMapper _mapper;

		public AssignLeaveService(IAssignLeaveRepository service, IMapper mapper)
		{
			_repository = service;
			_mapper = mapper;
		}

		public async Task<ResponseModel<AssignLeaveViewDto>> Add(AssignLeaveAddDto entity)
		{
			var result=await _repository.Add(entity);
			return result;
			
		}

		public async Task<ResponseModel<bool>> Delete(int id)
		{
			var result = await _repository.Delete(id);
			return result;
		}

		public async Task<ResponseModel<IEnumerable<AssignLeaveViewDto>>> Get(int id)
		{
			var result = await _repository.Get(id);
			return result;
		}

		public async Task<ResponseModel<IEnumerable<AssignLeaveViewDto>>> GetAll()
		{
			var result = await _repository.GetAll();
			return result;
		}

		public async Task<ResponseModel<AssignLeaveViewDto>> GetSingle(int userId, int leaveTypeId)
		{
			var result = await _repository.GetSingle(userId,leaveTypeId);
			return result;
		}

		public async Task<ResponseModel<AssignLeaveViewDto>> Update(int id, AssignLeaveAddDto entity)
		{
			var existData = await _repository.GetSingle(id,entity.LeaveTypeId);
			if (existData != null)
			{
				existData.Data.NumbserOfLeave = entity.NumbserOfLeave;
				existData.Data.LeaveType.Id = entity.LeaveTypeId;
				existData.Data.User.Id = entity.UserId;
				var result = await _repository.Update(id,_mapper.Map<AssignLeave>(existData.Data));
				return new ResponseModel<AssignLeaveViewDto>() { Data = _mapper.Map<AssignLeaveViewDto>(result.Data), ErrorMessage = "Updated" };
			}
			return new ResponseModel<AssignLeaveViewDto>() { Data = _mapper.Map<AssignLeaveViewDto>(entity), ErrorMessage = "Not found" };
		}
	}
}
