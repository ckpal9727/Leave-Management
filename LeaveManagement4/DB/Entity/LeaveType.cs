using System.ComponentModel.DataAnnotations;

namespace LeaveManagement4.DB.Entity
{
	public class LeaveType
	{

		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
