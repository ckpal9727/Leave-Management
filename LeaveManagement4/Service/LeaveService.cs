using AutoMapper;
using Etickets.ResopnseModel;
using LeaveManagement4.DB.Entity;
using LeaveManagement4.Models;
using LeaveManagement4.Repository;

namespace LeaveManagement4.Service
{
	public class LeaveService : ILeaveService
	{
		private readonly ILeaveRepository _repository;
		
		private readonly IMapper _mapper;

		

		public LeaveService(ILeaveRepository repository, IMapper mapper) 
		{
			_repository = repository;
			_mapper = mapper;
			
		}

		public async Task<ResponseModel<LeaveViewDto>> Add(LeaveAddDto entity)
		{
			if (entity.StartDate > entity.EndDate)
			{
				return new ResponseModel<LeaveViewDto>() { Data = _mapper.Map<LeaveViewDto>(entity), ErrorMessage = "Start date must be smaller" };
			}

			var numberOfLeaves = (entity.EndDate - entity.StartDate).Days+1;
			entity.NumbserOfLeave= numberOfLeaves;
		        var result = await _repository.Add(entity);
				return result;

		}

		

		public async Task<ResponseModel<LeaveViewDto>> ApproveLeave(int leaveId)
		{
			var result = await _repository.ApproveLeave(leaveId);
			return result;
		}

		public async Task<ResponseModel<bool>> Delete(int id)
		{
			var result = await _repository.Delete(id);
			return result;
		}

		public async Task<ResponseModel<LeaveViewDto>> Get(int id)
		{
			var result = await _repository.Get(id);
			return result;
		}

		public async Task<ResponseModel<IEnumerable<LeaveViewDto>>> GetAll()
		{
			var result = await _repository.GetAll();
			return result;
		}

		public async Task<ResponseModel<List<LeaveViewDto>>> GetSpecificUserLeaves(int id)
		{
			var result = await _repository.GetSpecificUserLeaves(id);
			return result;
		}

		public async Task<ResponseModel<LeaveViewDto>> RejectLeave(int leaveId)
		{
			var result = await _repository.RejectLeave(leaveId);
			return result;
		}

		public async Task<ResponseModel<LeaveViewDto>> Update(int id, LeaveAddDto entity)
		{
		
					var numberOfLeaves = (entity.EndDate - entity.StartDate).Days+1;
			entity.NumbserOfLeave= numberOfLeaves;
					var result = await _repository.Update(id,entity);
					return result;
			
		}
		//New Leave function
		public async Task<ResponseModel<LeaveViewDto>> UpdateLeave(int id, LeaveAddDto entity)
		{
			var numberOfLeaves = (entity.EndDate - entity.StartDate).Days + 1;
			entity.NumbserOfLeave = numberOfLeaves;
			if (entity.StartDate > entity.EndDate)
			{
				return new ResponseModel<LeaveViewDto>() { Data = _mapper.Map<LeaveViewDto>(entity), ErrorMessage = "Start date must be smaller" };
			}

			return await _repository.UpdateLeave(id, entity);

			
		}
	}
}
