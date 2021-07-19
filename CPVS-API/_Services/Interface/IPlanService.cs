﻿using CPVS_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPVS_API._Services.Interface
{
    public interface IPlanService : IECService<PlanDto>
    {
        Task<object> GetAllAsync();
        Task<object> GetAllPlanCount();
        Task<object> Start(int planID); 
        Task<object> Stop(int planID);

    }
}
