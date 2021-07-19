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
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace CPVS_API._Services.Services
{
    public class ToDoListService : IToDoListService
    {

        private readonly IBuildingUserRepository _repoBuildingUser;
        private readonly IBuildingRepository _repoBuilding;
        private readonly IUserRepository _repoUser;
        private readonly ITimeLineRepository _repoTimeLine;
        private readonly IToDoListRepository _repoToDoList;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _repoRole;
        private readonly IRoleUserRepository _repoRoleUser;
        private readonly IMailExtension _mailExtension;
        private readonly MapperConfiguration _configMapper;
        private readonly IConfiguration _configuration;
        public ToDoListService(IMailExtension mailExtension, IConfiguration configuration, ITimeLineRepository repoTimeLine,IRoleUserRepository repoRoleUser ,IRoleRepository repoRole, IUserRepository repoUser, IBuildingRepository repoBuilding, IToDoListRepository repoToDoList , IBuildingUserRepository repoBuildingUser, IMapper mapper, MapperConfiguration configMapper)
        {
            _configMapper = configMapper;
            _mapper = mapper;
            _repoBuildingUser = repoBuildingUser;
            _repoBuilding = repoBuilding;
            _repoToDoList = repoToDoList;
            _repoUser = repoUser;
            _repoRole = repoRole;
            _repoRoleUser = repoRoleUser;
            _repoTimeLine = repoTimeLine;
            _configuration = configuration;
            _mailExtension = mailExtension;
        }

        public async Task<object> Start(int todolistID)
        {
            try
            {
                var item = _repoToDoList.FindById(todolistID);
                await _repoToDoList.SaveAll();
            }
            catch (Exception)
            {

                throw;
            }
            return await _repoToDoList.SaveAll();
            //throw new NotImplementedException();
        }

        //Ham load Time Line
        public async Task<object> LoadTimeLine(int todolistID)
        {
            return await _repoTimeLine.FindAll().Where(x => x.ToDoList_ID == todolistID).ToListAsync();
        }

        // Lay danh sach OC theo dang tree
        public object GetListTreeClient(int userid)
        {
            var levels = new List<TreeView>();
            List<TreeView> hierarchy = new List<TreeView>();

            var listLevels = _repoBuilding.FindAll().OrderBy(x => x.Level).ToList();

            var user = _repoUser.FindAll().FirstOrDefault(x => x.ID == userid);

            var levelNumber = _repoBuilding.FindAll().FirstOrDefault(x => x.Level == user.LevelOC);

            if (levelNumber == null)
            {
                return new List<TreeView>();
            }

            listLevels = listLevels.Where(x => x.Level >= levelNumber.Level).OrderBy(x => x.Level).ToList();
            foreach (var item in listLevels)
            {
                var levelItem = new TreeView();
                levelItem.key = item.ID;
                levelItem.title = item.Name;
                levelItem.levelnumber = item.Level;
                levelItem.parentid = item.ParentID;
                levels.Add(levelItem);
            }
            var itemLevel = _repoBuilding.FindAll().FirstOrDefault(x => x.Level == user.LevelOC);
            hierarchy = levels.Where(c => c.parentid == itemLevel.ParentID)
                       .Select(c => new TreeView()
                       {
                           key = c.key,
                           title = c.title,
                           code = c.code,
                           levelnumber = c.levelnumber,
                           parentid = c.parentid,
                           children = GetChildren(levels, c.key)
                       }).ToList();

            HieararchyWalk(hierarchy);
            var obj = new TreeView();
            foreach (var item in hierarchy)
            {
                if (item.key == itemLevel.ID)
                {
                    obj = item;
                    break;
                }
            }
            var model = hierarchy.Where(x => x.key == itemLevel.ID).ToList();
            return model;
        }

        public List<TreeView> GetChildren(List<TreeView> levels, int parentid)
        {
            return levels
                    .Where(c => c.parentid == parentid)
                    .Select(c => new TreeView()
                    {
                        key = c.key,
                        title = c.title,
                        code = c.code,
                        levelnumber = c.levelnumber,
                        parentid = c.parentid,
                        children = GetChildren(levels, c.key)
                    }).ToList();
        }

        private void HieararchyWalk(List<TreeView> hierarchy)
        {
            if (hierarchy != null)
            {
                foreach (var item in hierarchy)
                {
                    //Console.WriteLine(string.Format("{0} {1}", item.Id, item.Text));
                    HieararchyWalk(item.children);
                }
            }
        }

        public async Task<object> Signed(int todolistID, int userid)
        {
            var item = _repoToDoList.FindById(todolistID);
            item.Signed_By = userid;

            try
            {
                item.Signed_Date = DateTime.Now;
                return await _repoToDoList.SaveAll();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<object> Approval(int todolistID, int userid)
        {

            var RoleID = _repoUser.FindAll().FirstOrDefault(x => x.ID == userid).RoleID;
            var Role_Name = _repoRole.FindAll().FirstOrDefault(x => x.ID == RoleID).Name;
            var item = _repoToDoList.FindById(todolistID);

            try
            {
                if (Role_Name == "Leader")
                {
                    item.Signed_By = userid;
                    item.Signed_Date = DateTime.Now;
                    item.Leader_Status = true;
                    //add TimeLine
                    var time_line = new TimeLine();
                    time_line.ToDoList_ID = todolistID;
                    time_line.Signed_Time = DateTime.Now;
                    time_line.Approve_Time = null;
                    time_line.TimeLine_Action = "Signed";
                    time_line.UserID = userid;
                    time_line.UserName = _repoUser.FindAll().FirstOrDefault(x => x.ID == userid).Username;
                    time_line.Role_Name = Role_Name;
                    _repoTimeLine.Add(time_line);
                    await _repoTimeLine.SaveAll();

                } else if (Role_Name == "Supervisor") {

                    item.Approve_By = userid;
                    item.Approve_Date = DateTime.Now;
                    item.Supervisor_Status = !item.Supervisor_Status;
                    item.Complete_Status = !item.Complete_Status;
                    item.Pending_Status = !item.Pending_Status;
                    item.isShow = false;
                    item.Approve_Date = DateTime.Now;
                    //add TimeLine
                    var time_line = new TimeLine();
                    time_line.ToDoList_ID = todolistID;
                    time_line.Signed_Time = null;
                    time_line.Approve_Time = DateTime.Now;
                    time_line.TimeLine_Action = "Signed";
                    time_line.UserID = userid;
                    time_line.UserName = _repoUser.FindAll().FirstOrDefault(x => x.ID == userid).Username;
                    time_line.Role_Name = Role_Name;
                    _repoTimeLine.Add(time_line);
                    await _repoTimeLine.SaveAll();
                }
                return await _repoToDoList.SaveAll();
            }
            catch (Exception ex)
            {
                return false;
            }
            throw new NotImplementedException();
        }

        public async Task<object> Reject(RejectDTO model)
        {
            var RoleID = _repoUser.FindAll().FirstOrDefault(x => x.ID == model.UserID).RoleID;
            var Email = _repoUser.FindAll().FirstOrDefault(x => x.ID == model.UserSenderID).Email;
            var Role_Name = _repoRole.FindAll().FirstOrDefault(x => x.ID == RoleID).Name;
            var item = _repoToDoList.FindById(model.ID);
            item.Reject_Status = true;
            item.Pending_Status = !item.Pending_Status;
            item.Leader_Status = !item.Leader_Status;
            item.Reasion = model.Remark;
            try
            {
                if (Role_Name == "Leader")
                {
                    //add TimeLine
                    var time_line = new TimeLine();
                    time_line.ToDoList_ID = model.ID;
                    time_line.Signed_Time = null;
                    time_line.Approve_Time = null;
                    time_line.Reject_Time = DateTime.Now;
                    time_line.TimeLine_Action = "Reject";
                    time_line.UserID = model.UserID;
                    time_line.UserName = _repoUser.FindAll().FirstOrDefault(x => x.ID == model.UserID).Username;
                    time_line.Role_Name = Role_Name;
                    _repoTimeLine.Add(time_line);
                    await _repoTimeLine.SaveAll();

                }
                else if (Role_Name == "Supervisor")
                {
                    //add TimeLine
                    var time_line = new TimeLine();
                    time_line.ToDoList_ID = model.ID;
                    time_line.Signed_Time = null;
                    time_line.Approve_Time = null;
                    time_line.Reject_Time = DateTime.Now;
                    time_line.TimeLine_Action = "Reject";
                    time_line.UserID = model.UserID;
                    time_line.UserName = _repoUser.FindAll().FirstOrDefault(x => x.ID == model.UserID).Username;
                    time_line.Role_Name = Role_Name;
                    _repoTimeLine.Add(time_line);
                    await _repoTimeLine.SaveAll();
                }
                await SendMailForSender(Email, model.Remark);
                return await _repoToDoList.SaveAll();
            }
            catch (Exception ex)
            {
                return false;
            }
            throw new NotImplementedException();
        }

        // ham gui mai cho nguoi tao & submit file khi bi reject
        public async Task SendMailForSender(string email, string content)
        {
            string subject = "(SHC-902) E-Signature System - Notification";
            string url = _configuration["MailSettings:API_URL"].ToSafetyString();
            string message = @"
                Notification from E-Signature System <br />
                <b>*PLEASE DO NOT REPLY*</b> this email was automatically sent from the E-Signature System <br />
                The file has been rejected by Supervisor!!!<br />
                <p> Content: <b> " + content + " </b> </p>";
            message += $"<a href='{url}'>Click here to go to the system</a>";
            var emails = new List<string> { email };
            Thread thread = new Thread(async () =>
            {
                await _mailExtension.SendEmailRangeAsync(emails, subject, message);
            });
            thread.Start();
            
        }

        public async Task<object> GetAllToDoList(int userid)
        {
            var RoleID = _repoUser.FindAll().FirstOrDefault(x => x.ID == userid).RoleID;
            var Role_Name = _repoRole.FindAll().FirstOrDefault(x => x.ID == RoleID).Name;
            var list = new List<ToDoListDto>();

            if (Role_Name == "Supervisor" || Role_Name == "Admin")
            {
                return _repoToDoList.FindAll().Where(x => x.Leader_Status == true && x.isShow == true).OrderByDescending(x => x.Created_Date).ToList();
            } 
            else if (Role_Name == "Leader")
            {
                var listUser = await CheckWhoManUser(userid);
                foreach (var item in listUser)
                {
                    var model = await _repoToDoList.FindAll().Where(x => x.Created_By == item)
                    .Select(x => new ToDoListDto
                    {
                        ID = x.ID,
                        File_Name = x.File_Name,
                        Topic = x.Topic,
                        Created_By = x.Created_By,
                        Approve_By = x.Approve_By,
                        Created_Date = x.Created_Date,
                        Approve_Date = x.Approve_Date,
                        Status = x.Status,
                        Seen_Status = x.Seen_Status,
                        Seen_Time = x.Seen_Time,
                        Pending_Status = x.Pending_Status,
                        Complete_Status = x.Complete_Status,
                        Delete_By = x.Delete_By,
                        Delete_Time = x.Delete_Time,
                        Reasion = x.Reasion,
                        Reject_Status = x.Reject_Status,
                        Leader_Status = x.Leader_Status,
                        Supervisor_Status = x.Supervisor_Status,
                        Signed_By = x.Signed_By,
                        Signed_Date = x.Signed_Date,
                        URL = x.URL,
                    }).Where(x => x.Leader_Status == false).OrderByDescending(x => x.Created_Date)
                    .ToListAsync();
                    foreach (var item2 in model)
                    {
                        list.Add(item2);
                    }
                }
            }
            else
            {
                //return _repoToDoList.FindAll().Where(x => x.Leader_Status == true && x.isShow == true).OrderByDescending(x => x.Created_Date).ToList();
                var listUser = await CheckWhoManUser(userid);
                foreach (var item in listUser)
                {
                    var model = await _repoToDoList.FindAll().Where(x => x.Created_By == item)
                    .Select(x => new ToDoListDto
                    {
                        ID = x.ID,
                        File_Name = x.File_Name,
                        Topic = x.Topic,
                        Created_By = x.Created_By,
                        Approve_By = x.Approve_By,
                        Created_Date = x.Created_Date,
                        Approve_Date = x.Approve_Date,
                        Status = x.Status,
                        Seen_Status = x.Seen_Status,
                        Seen_Time = x.Seen_Time,
                        Pending_Status = x.Pending_Status,
                        Complete_Status = x.Complete_Status,
                        Delete_By = x.Delete_By,
                        Delete_Time = x.Delete_Time,
                        Reasion= x.Reasion,
                        Reject_Status = x.Reject_Status,
                        Leader_Status = x.Leader_Status,
                        Supervisor_Status = x.Supervisor_Status,
                        Signed_By = x.Signed_By,
                        Signed_Date = x.Signed_Date,
                        URL = x.URL

                    }).OrderByDescending(x => x.Created_Date)
                    .ToListAsync();
                    foreach (var item2 in model)
                    {
                        list.Add(item2);
                    }
                }
            }
            return list;
            //throw new NotImplementedException();
        }

        // check xem ai la leader cua userid gui len
        public async Task<List<int>> CheckWhoManUser(int userid)
        {
            var list = new List<int>();
            var checkUser = _repoBuildingUser.FindAll().AnyAsync(x => x.UserID == userid);
            if (checkUser.Result == false )
            {
                return list;
            }
            var BuildingUserID = _repoBuildingUser.FindAll().FirstOrDefault(x => x.UserID == userid).BuildingID;
            var BuildingID = _repoBuilding.FindAll().Where(x => x.ParentID == BuildingUserID).ToList();
            
            if (BuildingID.Count > 0)
            {
                foreach (var item in BuildingID)
                {
                    var Result = _repoBuildingUser.FindAll().Where(x => x.BuildingID == item.ID).Select(x => x.UserID).ToList();
                    if (Result.Count > 0)
                    {
                        foreach (var item2 in Result)
                        {
                            list.Add(item2);
                        }
                    }
                }
            }
            else
            {
                var Result = _repoBuildingUser.FindAll().Where(x => x.BuildingID == BuildingUserID && x.UserID == userid).Select(x => x.UserID).ToList();
                if (Result.Count > 0)
                {
                    foreach (var item in Result)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
            //throw new NotImplementedException();
        }

        public async Task<bool> UploadFile(List<ToDoList> entity)
        {
            var ListUpload = new List<ToDoList>();
            foreach (var item in entity)
            {
                ListUpload.Add(new ToDoList
                {
                    File_Name = item.File_Name,
                    Created_By = item.Created_By,
                    URL = item.URL,
                    Status = true,
                    isShow = true,
                    Pending_Status = true
                });
            }
            try
            {
                _repoToDoList.AddRange(ListUpload);
                await _repoToDoList.SaveAll();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<bool> Add(ToDoListDto model)
        {
            //var artProcess = _mapper.Map<ToDoList>(model);
            //_repoProcess.Add(artProcess);
            //return await _repoProcess.SaveAll();
            throw new NotImplementedException();
        }


        public async Task<bool> Delete(object id)
        {
            var todo = _repoToDoList.FindById(id);
            _repoToDoList.Remove(todo);
            return await _repoToDoList.SaveAll();
            throw new NotImplementedException();
        }

        public async Task<bool> Update(ToDoListDto model)
        {
            //var artProcess = _mapper.Map<Process>(model);
            //_repoProcess.Update(artProcess);
            //return await _repoProcess.SaveAll();
            throw new NotImplementedException();
        }
        public async Task<List<ToDoListDto>> GetAllAsync()
        {
            return await _repoToDoList.FindAll().ProjectTo<ToDoListDto>(_configMapper).OrderBy(x => x.ID).ToListAsync();
            //throw new NotImplementedException();
        }


        public Task<PagedList<ToDoListDto>> GetWithPaginations(PaginationParams param)
        {
            throw new NotImplementedException();
        }

        public Task<PagedList<ToDoListDto>> Search(PaginationParams param, object text)
        {
            throw new NotImplementedException();
        }

        public ToDoListDto GetById(object id)
        {
            throw new NotImplementedException();
        }

        
    }
}