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
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _repoPlan;
        private readonly IMapper _mapper;
        public PlanService( IMapper mapper , IPlanRepository repoPlan)
        {
            _repoPlan = repoPlan;
            _mapper = mapper ;
        }

        public async Task<object> GetAllPlanCount()
        {
            var res = await _repoPlan.FindAll().Where(x => x.DueDate == DateTime.Now.Date).ToListAsync();
            var items = res.GroupBy(t => t.Car_Name)
                .Select(t => new
                {
                    ID = t.Key,
                    Car = t.FirstOrDefault().Car_Name,
                    Amount = t.Sum(ta => ta.Amount),
                }).ToList();
            return items;
        }

        public async Task<object> Start(int planID)
        {
            try
            {
                var item = _repoPlan.FindById(planID);
                item.IsStart = true;
                return await _repoPlan.SaveAll();
            }
            catch (Exception ex)
            {
                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<object> Stop(int planID)
        {
            try
            {
                var item = _repoPlan.FindById(planID);
                item.IsStart = false;
                return await _repoPlan.SaveAll();
            }
            catch (Exception ex)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<bool> Add(PlanDto model)
        {
            var plan = _mapper.Map<Plan>(model);
            plan.Amount = 0;
            _repoPlan.Add(plan);
            return await _repoPlan.SaveAll();
        }

        public async Task<bool> Delete(object id)
        {
            var item = _repoPlan.FindById(id);
            _repoPlan.Remove(item);
            return await _repoPlan.SaveAll();
        }

        public async Task<object> GetAllAsync()
        {
            return await _repoPlan.FindAll().Where(x => x.DueDate == DateTime.Now.Date).ToListAsync();
        }

        public PlanDto GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<PlanDto>> GetWithPaginations(PaginationParams param)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<PlanDto>> Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PlanDto model)
        {
            throw new NotImplementedException();
        }

        Task<List<PlanDto>> IECService<PlanDto>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

    }
}