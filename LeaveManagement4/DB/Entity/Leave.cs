using LeaveManagement4.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement4.DB.Entity
{
	public class Leave
	{

		//sapce
		[Key]
		public int Id { get; set; }

		public int UserId { get; set; }
		[ForeignKey("UserId")]
		public User User { get; set; }

		public int LeaveTypeId { get; set; }
		[ForeignKey("LeaveTypeId")]
		public LeaveType LeaveType { get;set; }

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int NumbserOfLeave { get; set; }

		public LeaveStatus leaveStatus { get; set; }


	}
}
