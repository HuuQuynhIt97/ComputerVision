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
    public class TimeLineSerivce : ITimeLineService
    {
        private readonly IUserRepository _repoUser;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        public TimeLineSerivce(IUserRepository repoUser, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoUser = repoUser;

        }

        public Task<bool> Add(TimeLineDto model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TimeLineDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public TimeLineDto GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<TimeLineDto>> GetWithPaginations(PaginationParams param)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<TimeLineDto>> Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(TimeLineDto model)
        {
            throw new NotImplementedException();
        }
    }
}