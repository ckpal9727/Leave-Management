using AutoMapper;
using Etickets.ResopnseModel;
using LeaveManagement4.DB;
using LeaveManagement4.DB.Entity;
using LeaveManagement4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LeaveManagement4.Repository
{
	public class UserRepositoty : IUserRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;

		public UserRepositoty(DataContext context, IMapper mapper, IConfiguration configuration)
		{
			_context = context;
			_mapper = mapper;
			_configuration = configuration;
		}

		public async Task<ResponseModel<UserViewDto>> Add(UserAddDto entity)
		{
			var result=await _context.Users.AddAsync(_mapper.Map<User>(entity));

			if (result != null)
			{
				await _context.SaveChangesAsync();
				return new ResponseModel<UserViewDto>() { Data = _mapper.Map<UserViewDto>(result), ErrorMessage = "Succesfully User Added" };
			}
			else
			{
				return new ResponseModel<UserViewDto>() { Data = null, ErrorMessage = "Success" };
			}
			
			
		}

		public async Task<ResponseModel<bool>> Delete(int id)
		{
			var existUser = await _context.Users.FindAsync(id);
			if (existUser != null)
			{
				_context.Users.Remove(existUser);
				await _context.SaveChangesAsync();

				return new ResponseModel<bool>() { Data = true, ErrorMessage = "Successfully user deleted" };
			}
			else
			{
				return new ResponseModel<bool>() { Data = false, ErrorMessage = "Failed to delete" };
			}
		}

		public async Task<ResponseModel<UserViewDto>> Get(int id)
		{
			var existUser = await _context.Users.AsNoTracking().Where(x=>x.Id==id).FirstOrDefaultAsync();	
			if (existUser != null)
			{
				return new ResponseModel<UserViewDto>() { Data = _mapper.Map<UserViewDto>(existUser), ErrorMessage = "Success" };
			}
			else
			{
				return new ResponseModel<UserViewDto>() { Data = null, ErrorMessage = "Success" };
			}
		}

		public async Task<ResponseModel<IEnumerable<UserViewDto>>> GetAll()
		{
			var result = await _context.Users.ToListAsync();
			if (result != null)
			{
				return new ResponseModel<IEnumerable<UserViewDto>>() { Data = _mapper.Map<IEnumerable<UserViewDto>>(result), ErrorMessage = "Success" };
			}
			else
			{
				return new ResponseModel<IEnumerable<UserViewDto>>() { Data =null, ErrorMessage = "Not found" };
			}
		}

		public async Task<ResponseModel<string>> Login(UserLoginDto entity)
		{
			var result=await _context.Users.Where(u=>u.Email==entity.Email && u.Password==entity.Password).FirstOrDefaultAsync();
			if (result != null)
			{
				if (result.IsAdmin)
				{
					var authClaims = new List<Claim>
					{
						new Claim(ClaimTypes.Email, result.Email),
						new Claim(ClaimTypes.Role, "Admin"),
						new Claim("UserId",result.Id.ToString())

					};
					
					return new ResponseModel<string>() { Data = new JwtSecurityTokenHandler().WriteToken(GetToken(authClaims)), ErrorMessage = "Login Success as admin" };
				}
				else
				{
					var authClaims = new List<Claim>
					{
						new Claim(ClaimTypes.Email, result.Email),
						new Claim(ClaimTypes.Role, "User"),
						new Claim("UserId",result.Id.ToString())

					};
					
					
					return new ResponseModel<string>() { Data = new JwtSecurityTokenHandler().WriteToken(GetToken(authClaims)), ErrorMessage = "Login Success as User" };
				}
			}
			
			return new ResponseModel<string>() { Data = null, ErrorMessage = "Invalid creadentials" };
		}

		public async Task<ResponseModel<UserViewDto>> Update( User entity)
		{
			try
			{
				_context.Users.Update(entity);
				await _context.SaveChangesAsync();
				return new ResponseModel<UserViewDto>() { Data = _mapper.Map<UserViewDto>(entity), ErrorMessage = "Successfully user updated" }; // Return the updated entity
			}
			catch (Exception ex)
			{
				// Log the exception
				Console.WriteLine($"Error updating entity: {ex.Message}");
				throw; // Rethrow the exception to propagate it up the call stack
			}

		}
		private JwtSecurityToken GetToken(List<Claim> authClaims)
		{
			var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
			var token = new JwtSecurityToken(
				issuer: _configuration["JwtSettings:Issuer"],
				audience: _configuration["JwtSettings:Audience"],
				expires: DateTime.Now.AddMinutes(1),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
			);
			return token;
		}
	}
}
