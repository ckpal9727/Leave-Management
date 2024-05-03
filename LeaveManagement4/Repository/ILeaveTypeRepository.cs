using Etickets.ResopnseModel;
using LeaveManagement4.DB.Entity;
using LeaveManagement4.Models;

namespace LeaveTypeManagement4.Repository
{
	public interface ILeaveTypeTypeRepository
	{
		Task<ResponseModel<IEnumerable<LeaveTypeViewDto>>> GetAll();
		Task<ResponseModel<LeaveTypeViewDto>> Get(int id);
		Task<ResponseModel<LeaveTypeViewDto>> Add(LeaveTypeAddDto entity);
		Task<ResponseModel<LeaveTypeViewDto>> Update( LeaveType entity);
		Task<ResponseModel<bool>> Delete(int id);
	}
}
