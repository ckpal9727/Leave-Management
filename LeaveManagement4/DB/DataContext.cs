using LeaveManagement4.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement4.DB
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Leave> Leaves { get; set; }	
		public DbSet<LeaveType> LeaveTypes { get; set; }

		public DbSet<AssignLeave> AssignTypes { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AssignLeave>()
				.HasKey(asi => new 
				{
					asi.LeaveTypeId,
					asi.UserId
				});
			base.OnModelCreating(modelBuilder);

			
				
		}
	}
}
