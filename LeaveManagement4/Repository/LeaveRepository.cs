using AutoMapper;
using Etickets.ResopnseModel;
using LeaveManagement4.DB;
using LeaveManagement4.DB.Entity;
using LeaveManagement4.Enum;
using LeaveManagement4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LeaveManagement4.Repository
{
	public class LeaveRepository : ILeaveRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public LeaveRepository(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		

		public async Task<ResponseModel<LeaveViewDto>> Add(LeaveAddDto entity)
		{
			var leavebalance=await _context.AssignTypes.Where(u=>u.UserId==entity.UserId && u.LeaveTypeId==entity.LeaveTypeId).FirstOrDefaultAsync();
			if (entity.NumbserOfLeave <= leavebalance.NumbserOfLeave)
			{
				leavebalance.NumbserOfLeave = leavebalance.NumbserOfLeave - entity.NumbserOfLeave;

				_context.AssignTypes.Update(leavebalance);
				entity.leaveStatus = LeaveStatus.Pending;
				var result = await _context.Leaves.AddAsync(_mapper.Map<Leave>(entity));

				if (result != null)
				{
					await _context.SaveChangesAsync();
					return new ResponseModel<LeaveViewDto>() { Data = _mapper.Map<LeaveViewDto>(result), ErrorMessage = "Leave Applied" };
				}
				else
				{
					return new ResponseModel<LeaveViewDto>() { Data = null, ErrorMessage = "Problem in leave Apply" };

				}
			}
			return new ResponseModel<LeaveViewDto>() { Data = null, ErrorMessage = "No leave balance found" };
		}

		public async Task<ResponseModel<LeaveViewDto>> ApproveLeave(int leaveId)
		{
			var exitLeave = await _context.Leaves.FindAsync(leaveId);
			exitLeave.leaveStatus = LeaveStatus.Approve;
			_context.Leaves.Update(exitLeave);
			await _context.SaveChangesAsync();
			if (exitLeave != null)
			{

				return new ResponseModel<LeaveViewDto>() { Data = _mapper.Map<LeaveViewDto>(exitLeave), ErrorMessage = "Leave Approved succesfully" };
			}
			else
			{
				return new ResponseModel<LeaveViewDto>() { Data = null, ErrorMessage = "Fail" };
			}
		}

		public async Task<ResponseModel<bool>> Delete(int id)
		{

			var existUser = await _context.Leaves.FindAsync(id);
			var leavebalance = await _context.AssignTypes.Where(u => u.UserId == existUser.UserId && u.LeaveTypeId == existUser.LeaveTypeId).FirstOrDefaultAsync();
			leavebalance.NumbserOfLeave = existUser.NumbserOfLeave+leavebalance.NumbserOfLeave;
			_context.AssignTypes.Update(leavebalance);
			/*&& existUser.leaveStatus == LeaveStatus.Pending*/
			if (existUser != null  )
			{
				_context.Leaves.Remove(existUser);
				await _context.SaveChangesAsync();

				return new ResponseModel<bool>() { Data = true, ErrorMessage = "Leave cancelled succesfully" };
			}
			else
			{
				return new ResponseModel<bool>() { Data = false, ErrorMessage = "Failed to cancell" };
			}
		}

		public async Task<ResponseModel<LeaveViewDto>> Get(int id)
		{
			var existUser = await _context.Leaves.Include(u=>u.User).Include(l=>l.LeaveType).Where(u=>u.Id == id).FirstOrDefaultAsync();
			if (existUser != null)
			{
				return new ResponseModel<LeaveViewDto>() { Data = _mapper.Map<LeaveViewDto>(existUser), ErrorMessage = "Success" };
			}
			else
			{
				throw new Exception("Leave is not found");
			}
		}

		public async Task<ResponseModel<IEnumerable<LeaveViewDto>>> GetAll()
		{
			var result = await _context.Leaves.Include(u=>u.User).Include(l=>l.LeaveType).ToListAsync();
			if (result != null)
			{
				return new ResponseModel<IEnumerable<LeaveViewDto>>() { Data = _mapper.Map<IEnumerable<LeaveViewDto>>(result), ErrorMessage = "Successfully get all leaves" };
			}
			else
			{
				return new ResponseModel<IEnumerable<LeaveViewDto>>() { Data = null, ErrorMessage = "Not found" };
			}
		}

		public async Task<ResponseModel<List<LeaveViewDto>>> GetSpecificUserLeaves(int id)
		{
			var existUser = await _context.Leaves.Include(u => u.User).Include(l => l.LeaveType).Where(u=>u.UserId== id).ToListAsync();
			if (existUser != null)
			{

				return new ResponseModel<List<LeaveViewDto>>() { Data = _mapper.Map<List<LeaveViewDto>>(existUser), ErrorMessage = "Succesfull" };
			}
			else
			{
				return new ResponseModel<List<LeaveViewDto>>() { Data = null, ErrorMessage = "Fail" };
			}
		}

		public async Task<ResponseModel<LeaveViewDto>> RejectLeave(int leaveId)
		{
            var exitLeave = await _context.Leaves.FindAsync(leaveId);
			exitLeave.leaveStatus = LeaveStatus.Reject;
			_context.Leaves.Update(exitLeave);
			await _context.SaveChangesAsync();

			if (exitLeave != null)
			{

				return new ResponseModel<LeaveViewDto>() { Data = _mapper.Map<LeaveViewDto>(exitLeave), ErrorMessage = "Succesfully rejected " };
			}
			else
			{
				return new ResponseModel<LeaveViewDto>() { Data = null, ErrorMessage = "Fail" };
			}

		}

		public async Task<ResponseModel<LeaveViewDto>> Update(int id, LeaveAddDto entity)
		{
			var leavebalance = await _context.AssignTypes.Where(u => u.UserId == entity.UserId && u.LeaveTypeId == entity.LeaveTypeId).FirstOrDefaultAsync();
			var existleave = await _context.Leaves.FindAsync(id);
			var entityStartDate = (entity.StartDate).Day+1;
			var entityEndDate= (entity.EndDate).Day+1;
			var existStartDate= (existleave.StartDate).Day;
			var existEndDate= (existleave.EndDate).Day;
			entity.leaveStatus = LeaveStatus.Pending;
			if (entityStartDate!= existStartDate || entityEndDate != existEndDate && entity.NumbserOfLeave==existleave.NumbserOfLeave)
			{
				existleave.StartDate = entity.StartDate.AddDays(1);
				existleave.EndDate = entity.EndDate.AddDays(1);
				_context.Leaves.Update(existleave);
				await _context.SaveChangesAsync();
				return new ResponseModel<LeaveViewDto>() { Data = _mapper.Map<LeaveViewDto>(existleave), ErrorMessage = "Successfully leave updated" };
			}
			if (entity.NumbserOfLeave <= leavebalance.NumbserOfLeave  && entity.leaveStatus==LeaveStatus.Pending )
			{
				leavebalance.NumbserOfLeave =  entity.NumbserOfLeave;
				_context.AssignTypes.Update(leavebalance);

				
				existleave.StartDate=entity.StartDate;
				existleave.EndDate=entity.EndDate;
				existleave.NumbserOfLeave = entity.NumbserOfLeave;

				var result =  _context.Leaves.Update(existleave);

				if (result != null)
				{
					await _context.SaveChangesAsync();
					return new ResponseModel<LeaveViewDto>() { Data = _mapper.Map<LeaveViewDto>(result), ErrorMessage = "Success" };
				}
				else
				{
					return new ResponseModel<LeaveViewDto>() { Data = null, ErrorMessage = "Success" };

				}
			}
			return new ResponseModel<LeaveViewDto>() { Data = null, ErrorMessage = "No leave balance found" };
			
		}

		public async Task<ResponseModel<LeaveViewDto>> UpdateLeave(int id, LeaveAddDto entity)
		{
			var leavebalance = await _context.AssignTypes
				.Where(u => u.UserId == entity.UserId && u.LeaveTypeId == entity.LeaveTypeId)
				.FirstOrDefaultAsync();
			var existleave = await _context.Leaves.FindAsync(id);
			if ( entity.NumbserOfLeave <=leavebalance.NumbserOfLeave+existleave.NumbserOfLeave)
			{
				if(entity.NumbserOfLeave > existleave.NumbserOfLeave)
				{
					leavebalance.NumbserOfLeave = leavebalance.NumbserOfLeave - entity.NumbserOfLeave +existleave.NumbserOfLeave;
					_context.Update(leavebalance);


					existleave.StartDate = entity.StartDate;
					existleave.EndDate = entity.EndDate;
					existleave.NumbserOfLeave = entity.NumbserOfLeave;

					var result=_context.Leaves.Update(existleave);
					await _context.SaveChangesAsync();
					return new ResponseModel<LeaveViewDto>() { Data = _mapper.Map<LeaveViewDto>(result), ErrorMessage = "Successfully leave updated" };

				}
				else
				{
					leavebalance.NumbserOfLeave = leavebalance.NumbserOfLeave + (existleave.NumbserOfLeave - entity.NumbserOfLeave);
					_context.Update(leavebalance);

					existleave.StartDate = entity.StartDate;
					existleave.EndDate = entity.EndDate;
					existleave.NumbserOfLeave = entity.NumbserOfLeave;

					var result = _context.Leaves.Update(existleave);
					await _context.SaveChangesAsync();
					return new ResponseModel<LeaveViewDto>() { Data = _mapper.Map<LeaveViewDto>(result), ErrorMessage = "Successfully leave updated" };
				}
			}
				return new ResponseModel<LeaveViewDto>() { Data = null, ErrorMessage = "No leave balance found" };
		}
	}
}
