using Etickets.ResopnseModel;
using LeaveManagement4.DB.Entity;
using LeaveManagement4.Models;

namespace LeaveManagement4.Service
{
	public interface ILeaveTypeService
	{
		Task<ResponseModel<IEnumerable<LeaveTypeViewDto>>> GetAll();
		Task<ResponseModel<LeaveTypeViewDto>> Get(int id);
		Task<ResponseModel<LeaveTypeViewDto>> Add(LeaveTypeAddDto entity);
		Task<ResponseModel<LeaveTypeViewDto>> Update(int id, LeaveTypeAddDto entity);
		Task<ResponseModel<bool>> Delete(int id);
	}
}
