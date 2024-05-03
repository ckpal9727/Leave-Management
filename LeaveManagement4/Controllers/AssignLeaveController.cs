﻿using LeaveManagement4.Models;
using LeaveManagement4.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement4.Controllers
{
	[Route("api/assignLeave")]
	[ApiController]
	public class AssignLeaveController : ControllerBase
	{
		private readonly IAssignLeaveService _service;

		public AssignLeaveController(IAssignLeaveService service)
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

		[HttpGet("getSingle/{userId}/{leaveTypeId}")]
		public async Task<IActionResult> GetSingle(int userId,int leaveTypeId)
		{
			var result = await _service.GetSingle(userId,leaveTypeId);
			if(result!=null)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}

		}
		/*[HttpGet("assign_user_leave/{id}")]
		public async Task<IActionResult> AssignUserLeaves(int id)
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

		}*/

		[HttpPost]
		public async Task<IActionResult> Add(AssignLeaveAddDto dto)
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
		public async Task<IActionResult> Update(int id, AssignLeaveAddDto dto)
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
