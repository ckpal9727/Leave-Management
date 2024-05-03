using LeaveManagement4.Models;
using LeaveManagement4.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement4.Controllers
{
	[Route("api/leavetype")]
	[ApiController]
	public class LeaveTypeController : ControllerBase
	{
		private readonly ILeaveTypeService _service;

		public LeaveTypeController(ILeaveTypeService service)
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
		public async Task<IActionResult> Add(LeaveTypeAddDto dto)
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
		public async Task<IActionResult> Update(int id, LeaveTypeAddDto dto)
		{
			var result = await _service.Update(id, dto);
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
