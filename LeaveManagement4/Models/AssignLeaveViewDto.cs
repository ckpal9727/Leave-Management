using LeaveManagement4.DB.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement4.Models
{
	public class AssignLeaveViewDto
	{
	
		public User User { get; set; }

		
		public LeaveType LeaveType { get; set; }
		public int NumbserOfLeave { get; set; }
	}
}
