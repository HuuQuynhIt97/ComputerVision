using CPVS_API.DTO;
using CPVS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPVS_API._Services.Interface
{
   public interface IToDoListService : IECService<ToDoListDto>
    {
        Task<bool> UploadFile(List<ToDoList> entity);
        Task<List<int>> CheckWhoManUser(int userid);
        Task<object> GetAllToDoList(int userid);
        Task<object> Approval(int todolistID, int userid);
        Task<object> Signed(int todolistID, int userid);
        Task<object> Reject(RejectDTO model);
        object GetListTreeClient(int userid);
        Task<object> LoadTimeLine(int todolistID); 
        Task<object> Start(int todolistID);

    }
}
