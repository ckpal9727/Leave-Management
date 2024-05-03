using System.ComponentModel.DataAnnotations;

namespace LeaveManagement4.DB.Entity
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		public string Name{ get; set; }
		public string Email { get; set; }

		public string Password { get; set; }
		public bool IsAdmin{ get; set; }

		public List<Leave> Leaves { get; set; }
		public List<AssignLeave> AssignLeaves { get; set; }
	}
}
