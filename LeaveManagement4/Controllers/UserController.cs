using LeaveManagement4.Models;
using LeaveManagement4.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement4.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _service;

		public UserController(IUserService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll() 
		{
			var result = await  _service.GetAll();
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
		public async Task<IActionResult>  Add(UserAddDto dto)
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
		public async Task<IActionResult> Update(int id,UserAddDto dto)
		{
			var result = await _service.Update(id,dto);
			if (result != null)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login(UserLoginDto dto)
		{
			var result = await _service.Login(dto);
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


	}
}
