using AutoMapper;
using Etickets.ResopnseModel;
using LeaveManagement4.DB.Entity;
using LeaveManagement4.Models;
using LeaveManagement4.Repository;
using LeaveTypeManagement4.Repository;

namespace LeaveManagement4.Service
{
	public class LeaveTypeService : ILeaveTypeService
	{
		private readonly ILeaveTypeTypeRepository _repository;
		private readonly IMapper _mapper;

		public LeaveTypeService(ILeaveTypeTypeRepository repository,IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<ResponseModel<LeaveTypeViewDto>> Add(LeaveTypeAddDto entity)
		{
			var result = await _repository.Add(entity);
			return result;
		}

		public async Task<ResponseModel<bool>> Delete(int id)
		{
			var result = await _repository.Delete(id);
			return result;
		}

		public async Task<ResponseModel<LeaveTypeViewDto>> Get(int id)
		{
			var result = await _repository.Get(id);
			return result;
		}

		public  async Task<ResponseModel<IEnumerable<LeaveTypeViewDto>>> GetAll()
		{
			var result = await _repository.GetAll();
			return result;
		}

		public async Task<ResponseModel<LeaveTypeViewDto>> Update(int id, LeaveTypeAddDto entity)
		{
			var existData = await _repository.Get(id);
			if (existData != null)
			{
					existData.Data.Name = entity.Name;
					var result = await _repository.Update(_mapper.Map<LeaveType>(existData.Data));
				return new ResponseModel<LeaveTypeViewDto>() { Data = _mapper.Map<LeaveTypeViewDto>(result.Data), ErrorMessage = "Updated" };
			}
			return new ResponseModel<LeaveTypeViewDto>() { Data = _mapper.Map<LeaveTypeViewDto>(entity), ErrorMessage = "Not found" };
		}
	}
}
