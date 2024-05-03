using AutoMapper;
using Etickets.ResopnseModel;
using LeaveManagement4.DB.Entity;
using LeaveManagement4.Models;
using LeaveManagement4.Repository;
using System.IdentityModel.Tokens.Jwt;

namespace LeaveManagement4.Service
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _repository;
		private readonly IMapper _mapper;

		public UserService(IUserRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public  async Task<ResponseModel<UserViewDto>> Add(UserAddDto entity)
		{
			var result = await _repository.Add(entity);
			return result;
		}

		public async Task<ResponseModel<bool>> Delete(int id)
		{
			var result = await _repository.Delete(id);
			return result;
		}

		public async Task<ResponseModel<UserViewDto>> Get(int id)
		{
			var result = await _repository.Get(id);
			return result;
		}

		public async Task<ResponseModel<IEnumerable<UserViewDto>>> GetAll()
		{
			var result = await _repository.GetAll();
			return result;

		}

		public async Task<ResponseModel<string>> Login(UserLoginDto entity)
		{
			var result = await _repository.Login(entity);
			return result;
		}

		public async Task<ResponseModel<UserViewDto>> Update(int id, UserAddDto entity)
		{
			var existData = await _repository.Get(id);
			if (existData != null)
			{
				existData.Data.Name = entity.Name;
				existData.Data.Password = entity.Password;
				existData.Data.Email = entity.Email;
				existData.Data.IsAdmin = entity.IsAdmin;
				
				var result = await _repository.Update(_mapper.Map<User>(existData.Data));
				return new ResponseModel<UserViewDto>() { Data = _mapper.Map<UserViewDto>(result.Data), ErrorMessage = "Updated" };
			}
			return new ResponseModel<UserViewDto>() { Data = _mapper.Map<UserViewDto>(entity), ErrorMessage = "Not found" };
		}
	}
}
