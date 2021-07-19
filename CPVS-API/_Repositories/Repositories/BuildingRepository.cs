using AutoMapper;
using CPVS_API._Repositories.Interface;
using CPVS_API.Data;
using CPVS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPVS_API._Repositories.Repositories
{
    public class BuildingRepository : ECRepository<Building>, IBuildingRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BuildingRepository(DataContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
    
    }
}
