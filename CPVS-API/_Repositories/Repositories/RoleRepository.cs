using System.Threading.Tasks;
using CPVS_API._Repositories.Interface;
using CPVS_API.Data;
using CPVS_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CPVS_API.DTO;
using System.Collections.Generic;

namespace CPVS_API._Repositories.Repositories
{
    public class RoleRepository : ECRepository<Role>, IRoleRepository
    {
        private readonly DataContext _context;
        public RoleRepository(DataContext context) : base(context)
        {
            _context = context;
        }

     
        //Login khi them repo
    }
}