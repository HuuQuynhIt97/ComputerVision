using CPVS_API.DTO;
using CPVS_API.Helpers;
using CPVS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPVS_API._Services.Interface
{
   public interface IBuildingService : IECService<BuildingDto>
    {
        Task<IEnumerable<HierarchyNode<BuildingDto>>> GetAllAsTreeView();
        Task<List<BuildingDto>> GetBuildings();
        Task<object> CreateMainBuilding(BuildingDto buildingDto);
        Task<object> CreateSubBuilding(BuildingDto buildingDto);
    }
}
