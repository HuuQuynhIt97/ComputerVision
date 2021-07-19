using CPVS_API.DTO;
using CPVS_API.Helpers;
using CPVS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPVS_API._Services.Interface
{
   public interface IBuildingUserService : IECService<BuildingUserDto>
    {
        Task<object> MappingUserWithBuilding(BuildingUserDto buildingUserDto);
        Task<object> RemoveBuildingUser(BuildingUserDto buildingUserDto);
        Task<List<BuildingUserDto>> GetBuildingUserByBuildingID(int buildingID);
        Task<object> GetBuildingByUserID(int userid);
        Task<object> MapBuildingUser(int userid, int buildingid);
    }
}
