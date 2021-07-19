using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CPVS_API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CPVS_API._Repositories.Interface;
using CPVS_API._Services.Interface;
using CPVS_API.DTO;
using CPVS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CPVS_API._Services.Services
{
    public class UserSerivce : IUserService
    {
        private readonly IUserRepository _repoUser;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public UserSerivce(IUserRepository repoUser, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoUser = repoUser;

        }

        public async Task<bool> Update(UserDto model)
        {
            var item = _repoUser.FindById(model.ID);
            item.EmployeeID = model.EmployeeID;
            item.Username = model.Username;
            item.Email = model.Email;
            item.RoleID = model.RoleID;
            item.ModifyTime = DateTime.Now;
            if (!model.Password.IsNullOrEmpty())
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
                item.PasswordHash = passwordHash;
                item.PasswordSalt = passwordSalt;
            }

            item.ModifyTime = DateTime.Now;

            try
            {
                _repoUser.Update(item);
                await _repoUser.SaveAll();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public async Task<bool> ChangePassword(int userId, string password)
        {
            byte[] passwordHash, passwordSalt;
            var item = _repoUser.FindById(userId);
            string pass = password;
            item.ModifyTime = DateTime.Now;

            CreatePasswordHash(pass, out passwordHash, out passwordSalt);
            try
            {
                item.PasswordHash = passwordHash;
                item.PasswordSalt = passwordSalt;
                await _repoUser.SaveAll();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
        public async Task<object> AddUser(UserDto model)
        {

            var item = await _repoUser.FindAll().FirstOrDefaultAsync(x => x.EmployeeID.ToLower().Equals(model.Username.ToLower()));
            if (item == null)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
                var user = _mapper.Map<User>(model);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.ModifyTime = DateTime.Now;
                _repoUser.Add(user);
                try
                {
                    await _repoUser.SaveAll();
                    return new
                    {
                        status = true,
                        id = user.ID,
                        message = "Created Successfully!"
                    };
                }
                catch (Exception)
                {
                    return new
                    {
                        status = false,
                        message = "Failed on save!"
                    };
                }
            }
            else
            {

                return new
                {
                    status = false,
                    message = "The User Already Exist!"
                };
            }
           
        }
        public async Task<bool> Add(UserDto model)
        {
            var item = await _repoUser.FindAll().FirstOrDefaultAsync(x => x.EmployeeID.ToLower().Equals(model.Username.ToLower()));
            if (item == null)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
                var user = _mapper.Map<User>(model);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.ModifyTime = DateTime.Now;
                _repoUser.Add(user);
                try
                {
                    await _repoUser.SaveAll();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {

                return false;
            }

        }

        public Task<bool> Delete(int userId, int lineID)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(object id)
        {
            var user = _repoUser.FindById(id);
            _repoUser.Remove(user);
            return await _repoUser.SaveAll();
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            return await _repoUser.FindAll().ProjectTo<UserDto>(_configMapper).OrderByDescending(x => x.ID).ToListAsync();
        }

        public UserDto GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<UserDto>> GetWithPaginations(PaginationParams param)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MapUserDetailDto(UserDto mapModel)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<UserDto>> Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        

        
    }
}