using Etickets.ResopnseModel;
using LeaveManagement4.DB.Entity;
using LeaveManagement4.Models;
using System.IdentityModel.Tokens.Jwt;

namespace LeaveManagement4.Service
{
	public interface IUserService
	{
		Task<ResponseModel<IEnumerable<UserViewDto>>> GetAll();
		Task<ResponseModel<UserViewDto>> Get(int id);
		Task<ResponseModel<UserViewDto>> Add(UserAddDto entity);
		Task<ResponseModel<UserViewDto>> Update(int id, UserAddDto entity);
		Task<ResponseModel<bool>> Delete(int id);
		Task<ResponseModel<string>> Login(UserLoginDto entity);
	}
}
