using LeaveManagement4.DB.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement4.Models
{
	public class AssignLeaveAddDto
	{
		public int UserId { get; set; }
		
		

		public int LeaveTypeId { get; set; }
		
		public int NumbserOfLeave { get; set; }
	}
}
