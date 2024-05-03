using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Etickets.ResopnseModel
{
	public class ResponseModel<T>
	{
		public T Data { get; set; }
		public string ErrorMessage { get; set; }

	}
}
