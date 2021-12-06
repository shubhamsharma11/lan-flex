using LanFlexAPI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Text;
using LanFlexAPI.Models.Helper;
using System.Web;
using System.Linq;
using System.IO;

namespace LanFlexAPI.Controllers
{
    public class VideoController : ApiController
    {
        // GET api/video
        [HttpGet]
        public HttpResponseMessage GetVideos()
        {
            List<VideoModel> videoList = new List<VideoModel>();
            var response = Request.CreateResponse();

            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Constants.appSettings.connectionString))
                {
                    mySqlConnection.Open();

                    MySqlCommand cmd = mySqlConnection.CreateCommand();
                    cmd.CommandText = Constants.VideoSelectStmt;

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        videoList.Add(new VideoModel()
                        {
                            Name = reader.GetString("Name"),
                            Extension = reader.GetString("Extension"),
                            FilePath = reader.GetString("Path"),
                            Type = reader.GetString("FileTypeCode"),
                            FileId = reader.GetInt32("FileId"),
                            ThumbPath = reader.IsDBNull(7) ? null : reader.GetString("ThumbPath"),
                            Details = reader.IsDBNull(8) ? null : reader.GetString("Details")
                        });
                    }
                    reader.Close();
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StringContent(JsonConvert.SerializeObject(videoList), Encoding.UTF8, "application/json");
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Content = new StringContent("There is an Exception " + ex.Message);
            }

            return response;
        }

        [HttpPost]
        public HttpResponseMessage FileUpload()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }


        //POST api/uploadVideo
        //[HttpPost]
        //public System.Web.Mvc.ActionResult FileUpload()
        //{
        //    //var a = data;
        //    //System.Web.Mvc.FormCollection data
        //    var a = Request.Content;

        //    var c = HttpContext.Current.Request.Form;
        //    //var b = file;

        //    HttpContext context = HttpContext.Current;
        //    HttpPostedFile postedFile = context.Request.Files.Count > 0
        //                    ? context.Request.Files.Get(0)
        //                    : null;

        //    //var video = videoModel.Video;

        //    //// Saving video on Server
        //    //if (video.Length > 0)
        //    //{
        //    //    using (var fileStream = new FileStream(video.FileName, FileMode.Create))
        //    //    {
        //    //        video.CopyTo(fileStream);
        //    //    }
        //    //}
        //    //return Ok(new { status = true, message = "Student Posted Successfully" });
        //    return null;
        //}

        //[HttpPost]
        //public System.Web.Mvc.ActionResult FileUpload(HttpPostedFileBase file)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (file == null)
        //        {
        //            ModelState.AddModelError("File", "Please Upload Your file");
        //        }
        //        else if (file.ContentLength > 0)
        //        {
        //            int MaxContentLength = 1024 * 1024 * 3; //3 MB
        //            string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

        //            if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
        //            {
        //                ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
        //            }

        //            else if (file.ContentLength > MaxContentLength)
        //            {
        //                ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
        //            }
        //            else
        //            {
        //                //TO:DO
        //                var fileName = Path.GetFileName(file.FileName);
        //                var path = Path.Combine(Server.MapPath("~/Content/Upload"), fileName);
        //                file.SaveAs(path);
        //                ModelState.Clear();
        //            }
        //        }
        //    }
        //    return View();
        //}


        //[HttpPost]
        //public System.Web.Mvc.ActionResult UploadFile()
        //{
        //    try
        //    {
        //        Request.Content;
        //    }
        //    catch (System.Exception ex)
        //    {

        //    }
        //}

        // =========================================================

        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        //// POST api/values
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
