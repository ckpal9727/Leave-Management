using Etickets.ResopnseModel;
using LeaveManagement4.DB.Entity;
using LeaveManagement4.Models;

namespace LeaveManagement4.Service
{
	public interface ILeaveService
	{
		Task<ResponseModel<IEnumerable<LeaveViewDto>>> GetAll();
		Task<ResponseModel<LeaveViewDto>> Get(int id);
		Task<ResponseModel<List<LeaveViewDto>>> GetSpecificUserLeaves(int id);
		Task<ResponseModel<LeaveViewDto>> Add(LeaveAddDto entity);
		Task<ResponseModel<LeaveViewDto>> UpdateLeave(int id, LeaveAddDto entity);
		Task<ResponseModel<LeaveViewDto>> Update(int id, LeaveAddDto entity);
		Task<ResponseModel<bool>> Delete(int id);

		Task<ResponseModel<LeaveViewDto>> ApproveLeave(int leaveId);
		Task<ResponseModel<LeaveViewDto>> RejectLeave(int leaveId);
	}
}
