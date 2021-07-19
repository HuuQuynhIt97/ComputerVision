using CPVS_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPVS_API._Services.Interface
{
    public interface IUserService : IECService<UserDto>
    {
        Task<bool> MapUserDetailDto(UserDto mapModel);
        Task<bool> Delete(int userId, int lineID);
        Task<object> AddUser(UserDto model);
        Task<bool> ChangePassword(int userId, string password);
    }
}
