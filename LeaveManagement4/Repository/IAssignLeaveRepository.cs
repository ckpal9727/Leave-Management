using Etickets.ResopnseModel;
using LeaveManagement4.DB.Entity;
using LeaveManagement4.Models;

namespace LeaveManagement4.Repository
{
	public interface IAssignLeaveRepository
	{
		Task<ResponseModel<IEnumerable<AssignLeaveViewDto>>> GetAll();
		
		Task<ResponseModel<IEnumerable<AssignLeaveViewDto>>> Get(int id);
		
		Task<ResponseModel<AssignLeaveViewDto>> GetSingle(int userId,int leaveTypeId);
		Task<ResponseModel<AssignLeaveViewDto>> Add(AssignLeaveAddDto entity);
		Task<ResponseModel<AssignLeaveViewDto>> Update(int id, AssignLeave entity);
		Task<ResponseModel<bool>> Delete(int id);
	}
}
