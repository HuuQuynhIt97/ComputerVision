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
using System.Transactions;

namespace CPVS_API._Services.Services
{
    public class RoleUserService : IRoleUserService
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public RoleUserService( IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;

        }

        public Task<bool> Add(RoleUserDto model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Task<List<RoleUserDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public RoleUserDto GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<RoleUserDto>> GetWithPaginations(PaginationParams param)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<RoleUserDto>> Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(RoleUserDto model)
        {
            throw new NotImplementedException();
        }
    }
}