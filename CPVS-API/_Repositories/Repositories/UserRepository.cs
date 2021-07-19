﻿using CPVS_API._Repositories.Interface;
using CPVS_API.Data;
using CPVS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPVS_API._Repositories.Repositories
{
    public class UserRepository : ECRepository<User>, IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
