﻿using Etickets.ResopnseModel;
using LeaveManagement4.DB.Entity;
using LeaveManagement4.Models;
using System.IdentityModel.Tokens.Jwt;

namespace LeaveManagement4.Repository
{
	public interface IUserRepository
	{
		Task<ResponseModel<IEnumerable<UserViewDto>>> GetAll();
		Task<ResponseModel<UserViewDto>> Get(int id);
		Task<ResponseModel<UserViewDto>> Add(UserAddDto entity);
		Task<ResponseModel<UserViewDto>> Update( User entity);
		Task<ResponseModel<bool>> Delete(int id);

		Task<ResponseModel<string>> Login(UserLoginDto entity);
	}
}
