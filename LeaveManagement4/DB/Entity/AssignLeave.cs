using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement4.DB.Entity
{
	public class AssignLeave
	{
		public int UserId { get; set; }
		[ForeignKey("UserId")]
		public User User { get; set; }

		public int LeaveTypeId { get; set; }
		[ForeignKey("LeaveTypeId")]
		public LeaveType LeaveType { get; set; }
		public int NumbserOfLeave { get; set; }
	}
}
