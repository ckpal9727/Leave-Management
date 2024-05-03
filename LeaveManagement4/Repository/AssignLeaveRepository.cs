using AutoMapper;
using Etickets.ResopnseModel;
using LeaveManagement4.DB;
using LeaveManagement4.DB.Entity;
using LeaveManagement4.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement4.Repository
{
	public class AssignLeaveRepository : IAssignLeaveRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public AssignLeaveRepository(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ResponseModel<AssignLeaveViewDto>> Add(AssignLeaveAddDto entity)
		{

			var exitLeave = await _context.AssignTypes.Where(u => u.UserId == entity.UserId && u.LeaveTypeId == entity.LeaveTypeId).FirstOrDefaultAsync();
			if (exitLeave != null)
			{
				exitLeave.NumbserOfLeave = entity.NumbserOfLeave;
				exitLeave.LeaveTypeId=entity.LeaveTypeId;
				_context.Update(exitLeave);
				await _context.SaveChangesAsync();
				return new ResponseModel<AssignLeaveViewDto>() { Data = _mapper.Map<AssignLeaveViewDto>(exitLeave), ErrorMessage = "Leave Updated" };
			}
			var result = await _context.AssignTypes.AddAsync(_mapper.Map<AssignLeave>(entity));

			if (result != null)
			{
				await _context.SaveChangesAsync();
				return new ResponseModel<AssignLeaveViewDto>() { Data = _mapper.Map<AssignLeaveViewDto>(result), ErrorMessage = "Success" };
			}
			else
			{
				return new ResponseModel<AssignLeaveViewDto>() { Data = null, ErrorMessage = "Success" };

			}
		}

		public async Task<ResponseModel<bool>> Delete(int id)
		{
			var existUser = await _context.AssignTypes.FindAsync(id);
			if (existUser != null)
			{
				_context.AssignTypes.Remove(existUser);
				await _context.SaveChangesAsync();

				return new ResponseModel<bool>() { Data = true, ErrorMessage = "Success" };
			}
			else
			{
				return new ResponseModel<bool>() { Data = false, ErrorMessage = "Failed to delete" };
			}
		}

		public async Task<ResponseModel<IEnumerable<AssignLeaveViewDto>>> Get(int id)
		{
			var existUser = await _context.AssignTypes
				.Include(u => u.User)
				.Include(l => l.LeaveType).AsNoTracking()
				.Where(x => x.UserId == id)
				.ToListAsync();

			if (existUser != null)
			{
				return new ResponseModel<IEnumerable<AssignLeaveViewDto>>() { Data = _mapper.Map<IEnumerable<AssignLeaveViewDto>>(existUser), ErrorMessage = "Success" };
			}
			else
			{
				return new ResponseModel<IEnumerable<AssignLeaveViewDto>>() { Data = null, ErrorMessage = "Success" };
			}
		}

		public async Task<ResponseModel<IEnumerable<AssignLeaveViewDto>>> GetAll()
		{
			var result = await _context.AssignTypes
	.Include(u => u.User)
	.Include(lt => lt.LeaveType)
	.ToListAsync();

			if (result != null)
			{
				return new ResponseModel<IEnumerable<AssignLeaveViewDto>>() { Data = _mapper.Map<IEnumerable<AssignLeaveViewDto>>(result), ErrorMessage = "Success" };
			}
			else
			{
				return new ResponseModel<IEnumerable<AssignLeaveViewDto>>() { Data = null, ErrorMessage = "Not found" };
			}
		}

		public async Task<ResponseModel<AssignLeaveViewDto>> GetSingle(int userId, int leaveTypeId)
		{
			var result = await _context.AssignTypes
			.Include(u => u.User)
			.Include(lt => lt.LeaveType)
			.Where(u => u.UserId == userId && u.LeaveTypeId == leaveTypeId)
			.FirstOrDefaultAsync();

			if (result != null)
			{
				await _context.SaveChangesAsync();
				return new ResponseModel<AssignLeaveViewDto>() { Data = _mapper.Map<AssignLeaveViewDto>(result), ErrorMessage = "Success" };
			}
			else
			{
				return new ResponseModel<AssignLeaveViewDto>() { Data = null, ErrorMessage = "Success" };

			}
		}

		public async Task<ResponseModel<AssignLeaveViewDto>> Update(int id, AssignLeave entity)
		{

			try
			{
				var existUser = await _context.AssignTypes
					.Include(u => u.User)
					.Include(l => l.LeaveType).AsNoTracking()
					.Where(x => x.UserId == id)
					.FirstOrDefaultAsync();

				existUser.NumbserOfLeave=entity.NumbserOfLeave;
				existUser.UserId=entity.UserId;
				existUser.LeaveTypeId=entity.LeaveTypeId;

				_context.AssignTypes.Update(existUser);
				await _context.SaveChangesAsync();
				return new ResponseModel<AssignLeaveViewDto>() { Data = _mapper.Map<AssignLeaveViewDto>(entity), ErrorMessage = "Success" }; // Return the updated entity
			}
			catch (Exception ex)
			{
				// Log the exception
				Console.WriteLine($"Error updating entity: {ex.Message}");
				throw; // Rethrow the exception to propagate it up the call stack
			}
		}
	}
}
