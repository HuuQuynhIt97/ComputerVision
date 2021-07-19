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
    public class CarService : ICarService
    {

        private readonly ICarRepository _repoCar;
        private readonly IMapper _mapper;
        public CarService( IMapper mapper , ICarRepository repoCar)
        {
            _repoCar = repoCar;

            _mapper = mapper ;
        }

        public async Task<object> GetAllAsync()
        {
            return await _repoCar.FindAll().ToListAsync();

        }

        
       
    }
}