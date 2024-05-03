using LeaveManagement4.DB.Entity;
using LeaveManagement4.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement4.Models
{
	public class LeaveViewDto
	{
		public int Id { get; set; }

		
		public User User { get; set; }

		
		public LeaveType LeaveType { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int NumbserOfLeave { get; set; }
		public LeaveStatus leaveStatus { get; set; }
	}
}
