using Etickets.ResopnseModel;
using LeaveManagement4.Models;

namespace LeaveManagement4.Service
{
	public interface IAssignLeaveService
	{
		Task<ResponseModel<IEnumerable<AssignLeaveViewDto>>> GetAll();
		Task<ResponseModel<IEnumerable<AssignLeaveViewDto>>> Get(int id);
		Task<ResponseModel<AssignLeaveViewDto>> GetSingle(int userId, int leaveTypeId);
		Task<ResponseModel<AssignLeaveViewDto>> Add(AssignLeaveAddDto entity);
		Task<ResponseModel<AssignLeaveViewDto>> Update(int id, AssignLeaveAddDto entity);
		Task<ResponseModel<bool>> Delete(int id);
	}
}
