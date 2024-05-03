using LeaveManagement4.Models;
using LeaveManagement4.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement4.Controllers
{
	[Route("api/leave")]
	[ApiController]
	public class LeaveController : ControllerBase
	{
		private readonly ILeaveService _service;

		public LeaveController(ILeaveService service)
		{
			_service = service;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _service.GetAll();
			if (result != null)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var result = await _service.Get(id);
			if (result != null)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}

		}

		[HttpPost]
		public async Task<IActionResult> Add(LeaveAddDto dto)
		{
			var result = await _service.Add(dto);
			if (result != null)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, LeaveAddDto dto)
		{
			var result = await _service.UpdateLeave(id, dto);
			
			if (result != null)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}
		}
		[HttpGet("userleaves/{id}")]
		public async Task<IActionResult> GetSpecificUserLeave(int id)
		{
			var result = await _service.GetSpecificUserLeaves(id);
			if (result != null)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _service.Delete(id);
			if (result != null)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}
		}

		[HttpPost("approve_leave/{id}")]
		public async Task<IActionResult> ApproveLeave(int id)
		{
			var result = await _service.ApproveLeave(id);
			if (result != null)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}
		}
		[HttpPost("reject_leave/{id}")]
		public async Task<IActionResult> RejectLeave(int id)
		{
			var result = await _service.RejectLeave(id);
			if (result != null)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}
		}


	}
}
