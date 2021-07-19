using CPVS_API._Services.Interface;
using CPVS_API.DTO;
using CPVS_API.Helpers;
using CPVS_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CPVS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListService _todolistService;
        public readonly IWebHostEnvironment _environment;
        public ToDoListController(IToDoListService todolistService ,  IWebHostEnvironment environment)
        {
            _environment = environment;
            _todolistService = todolistService;
        }

        [HttpGet("{todolistID}")]
        public async Task<IActionResult> Start(int todolistID)
        {
            return Ok(await _todolistService.Start(todolistID));
        }

        [HttpGet("{todolistID}")]
        public async Task<IActionResult> LoadTimeLine(int todolistID)
        {
            return Ok(await _todolistService.LoadTimeLine(todolistID));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetListTreeClient(int id)
        {
            return Ok(_todolistService.GetListTreeClient(id));
        }

        [HttpDelete("{id}/{file}")]
        public async Task<IActionResult> Delete(int id, string file)
        {

            if (await _todolistService.Delete(id))
            {
                DeleteFile(file);
            }
            return NoContent();
            throw new Exception("Error deleting the To Do");
        }

        [HttpGet("{todolistID}/{userid}")]
        public async Task<IActionResult> Signed(int todolistID, int userid)
        {
            return Ok(await _todolistService.Signed(todolistID, userid));

        }

        [HttpGet("{todolistID}/{userid}")]
        public async Task<IActionResult> Approval(int todolistID, int userid)
        {
            return Ok(await _todolistService.Approval(todolistID, userid));

        }

        [HttpPost]
        public async Task<IActionResult> Reject(RejectDTO model)
        {
            return Ok(await _todolistService.Reject(model));
        }

        [AllowAnonymous]
        [HttpGet("{filename}")]
        public ActionResult DownloadExcel(string filename)
        {
            string Files = $"wwwroot/FileUpload/{filename}";
            byte[] fileBytes = System.IO.File.ReadAllBytes(Files);
            System.IO.File.WriteAllBytes(Files, fileBytes);
            MemoryStream ms = new MemoryStream(fileBytes);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }

        [AllowAnonymous]
        [HttpPost("{filename}")]
        public ActionResult DeleteFile(string filename)
        {
            string Files = $"wwwroot/FileUpload/{filename}";
            FileInfo TheFileInfo = new FileInfo(Files);
            if (TheFileInfo.Exists)
            {
                System.IO.File.Delete(Files);
            }
            return Ok();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SaveFile(IFormFile formFile)
        {
            try
            {
                IFormFile file = Request.Form.Files["formFile"];
                if (file != null)
                {
                    string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    filename = _environment.WebRootPath + "\\ExcelUpload" + $@"\{filename}";
                    if (!System.IO.File.Exists(filename))
                    {
                        using (FileStream fs = System.IO.File.Create(filename))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                    else
                    {
                        Response.Clear();
                        Response.StatusCode = 204;
                        Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "File already exists.";
                        return Ok(new { status = false, ExistFile = true });
                    }
                }

            }
            catch (Exception ex)
            {
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.StatusCode = 204;
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "No Content";
                Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = ex.Message;
            }
            return Content("");
        }

        [HttpPost]
        public async Task<ActionResult<ToDoList>> Import([FromForm] List<ToDoList> entity)
        {
            if (ModelState.IsValid)
            {
                var list = new List<ToDoList>();

                var file = Request.Form.Files["UploadedFile"];

                // cat lay ten file & phan mo rong
                var arr = file.FileName.Split(new char[] {'.'});
                var fileNameWithoutExt = arr[0];
                var fileExtension = arr[1];

                var file_code = Request.Form["file_code"];

                var uploadBy = Request.Form["uploadBy"];

                if (file != null)
                {
                    //kiem tra da ton tai thu muc de upload file hay chua

                    //neu chua co thi tao moi
                    if (!Directory.Exists(_environment.WebRootPath + "\\FileUpload\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\FileUpload\\");
                    }

                    //lap file duoc truyen len
                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {
                        var currentFile = Request.Form.Files[i];
                        using FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\FileUpload\\" + currentFile.FileName);
                        await currentFile.CopyToAsync(fileStream);
                        fileStream.Flush();

                        list.Add(new ToDoList
                        {
                            File_Name = currentFile.FileName,
                            Created_By = uploadBy.ToInt(),
                            URL = $"/FileUpload/{file.FileName}"
                        });
                    }
                }
                var model = await _todolistService.UploadFile(list);
                return Ok(model);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return Ok(entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var model = await _todolistService.GetAllAsync();
            return Ok(model);
        }

        [HttpGet("{userid}")]
        public async Task<IActionResult> GetAllToDoListByUserID(int userid)
        {
            var model = await _todolistService.GetAllToDoList(userid);
            return Ok(model);
        }

        [HttpGet("{userid}")]
        public async Task<IActionResult> CheckWhoManUser(int userid)
        {
            var model = await _todolistService.CheckWhoManUser(userid);
            return Ok(model);
        }

    }
}
