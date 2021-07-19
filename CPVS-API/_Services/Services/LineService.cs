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
    public class LineService : ILineService
    {

        private readonly ILineRepository _repoLine;
        private readonly IMapper _mapper;
        public LineService( IMapper mapper , ILineRepository repoLine)
        {
            _repoLine = repoLine;

            _mapper = mapper ;
        }

        public async Task<object> GetAllAsync()
        {
            return await _repoLine.FindAll().Where(x => x.Status == true).ToListAsync();

        }

        
       
    }
}