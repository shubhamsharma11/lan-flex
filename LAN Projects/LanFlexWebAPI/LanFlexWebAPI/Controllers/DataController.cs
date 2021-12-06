using LanFlexWebAPI.Helpers;
using LanFlexWebAPI.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LanFlexWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private const long maxFileSize = 10L * 1024L * 1024L * 1024L;
        private readonly ILogger<DataController> _logger;
        private readonly IWebHostEnvironment _webHost;
        private readonly AppSettings appSettings;

        public DataController(ILogger<DataController> logger, IConfiguration config, IWebHostEnvironment webHost)
        {
            _logger = logger;
            _webHost = webHost;
            appSettings = new AppSettings(config);
        }

        // GET video/GetVideos
        [HttpGet("GetVideos")]
        public IActionResult GetVideos()
        {
            List<VideoModel> videoList = new List<VideoModel>();

            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(appSettings.connectionString))
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

                return Ok(JsonConvert.SerializeObject(videoList));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [RequestSizeLimit(maxFileSize)]
        [RequestFormLimits(MultipartBodyLengthLimit = maxFileSize)]
        // POST video/UploadFile
        [HttpPost("FileUpload")]
        public ActionResult FileUpload(IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    /* 
                     * TODO : 
                     * get other data from the ui along with the file type
                     * use the file type to determine where to save the file (audio/video/image)
                     * save the data into the database along with the file info
                    */

                    var fileName = file.FileName;

                    var filePath = appSettings.fullPath + fileName;

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Ok(new { status = true, message = "File Uploaded Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = false, message = ex.Message });
            }
        }
    }
}



//// D:\Projects\LAN Projects\LanFlexWebAPI\LanFlexWebAPI
//var physicalPath = _webHost.ContentRootPath;
//// D:\Projects\LAN Projects\LanFlexWebAPI\LanFlexWebAPI
//var path = Directory.GetCurrentDirectory();

//var fullPath = @"D:\Projects\LAN Projects\LanUI\public\LanFlexData\Videos\";
//var fileName = file.FileName;

//var filePath = fullPath + fileName;

//using (var stream = System.IO.File.Create(filePath))
//{
//    await file.CopyToAsync(stream);
//}