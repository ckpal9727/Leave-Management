using AutoMapper;
using Etickets.ResopnseModel;
using LeaveManagement4.DB;
using LeaveManagement4.DB.Entity;
using LeaveManagement4.Models;
using LeaveTypeManagement4.Repository;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement4.Repository
{
	public class LeaveTypeRepository : ILeaveTypeTypeRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public LeaveTypeRepository(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ResponseModel<LeaveTypeViewDto>> Add(LeaveTypeAddDto entity)
		{
			
			var result = await _context.LeaveTypes.AddAsync(_mapper.Map<LeaveType>(entity));

			if (result != null)
			{
				await _context.SaveChangesAsync();
				return new ResponseModel<LeaveTypeViewDto>() { Data = _mapper.Map<LeaveTypeViewDto>(result), ErrorMessage = "Success" };
			}
			else
			{
				return new ResponseModel<LeaveTypeViewDto>() { Data = null, ErrorMessage = "Success" };

			}
		}

		public async Task<ResponseModel<bool>> Delete(int id)
		{
			var existUser = await _context.LeaveTypes.FindAsync(id);
			if (existUser != null)
			{
				_context.LeaveTypes.Remove(existUser);
				await _context.SaveChangesAsync();

				return new ResponseModel<bool>() { Data = true, ErrorMessage = "Success" };
			}
			else
			{
				return new ResponseModel<bool>() { Data = false, ErrorMessage = "Failed to delete" };
			}
		}

		public async Task<ResponseModel<LeaveTypeViewDto>> Get(int id)
		{
			var existUser = await _context.LeaveTypes.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
			if (existUser != null)
			{
				return new ResponseModel<LeaveTypeViewDto>() { Data = _mapper.Map<LeaveTypeViewDto>(existUser), ErrorMessage = "Success" };
			}
			else
			{
				return new ResponseModel<LeaveTypeViewDto>() { Data = null, ErrorMessage = "Success" };
			}
		}

		public async Task<ResponseModel<IEnumerable<LeaveTypeViewDto>>> GetAll()
		{
			var result = await _context.LeaveTypes.ToListAsync();
			if (result != null)
			{
				return new ResponseModel<IEnumerable<LeaveTypeViewDto>>() { Data = _mapper.Map<IEnumerable<LeaveTypeViewDto>>(result), ErrorMessage = "Success" };
			}
			else
			{
				return new ResponseModel<IEnumerable<LeaveTypeViewDto>>() { Data = null, ErrorMessage = "Not found" };
			}
		}

		public async Task<ResponseModel<LeaveTypeViewDto>> Update(LeaveType entity)
		{
			try
			{
				_context.LeaveTypes.Update(entity);
				await _context.SaveChangesAsync();
				return new ResponseModel<LeaveTypeViewDto>() { Data = _mapper.Map<LeaveTypeViewDto>(entity), ErrorMessage = "Success" }; // Return the updated entity
			}
			catch (Exception ex)
			{
				// Log the exception
				Console.WriteLine($"Error updating entity: {ex.Message}");
				throw; // Rethrow the exception to propagate it up the call stack
			}
			/*var result = _context.LeaveTypes.Update(entity);*/
			
		}
	}
}
