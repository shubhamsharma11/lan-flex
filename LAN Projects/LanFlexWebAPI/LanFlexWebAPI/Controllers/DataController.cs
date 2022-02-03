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
using System.Data;
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
                    var filePath = _webHost.ContentRootPath + Constants.UploadFolderName + fileName;
                    var extension = fileName.Substring(fileName.LastIndexOf('.') + 1);
                    var typeCode = string.Empty;
                    var fileSize = 0L;

                    switch (extension)
                    {
                        case "mp4":
                            typeCode = "video/mp4;codecs=\''avc1.4D401E, mp4a.40.2\''";
                            break;
                        case "webm":
                            typeCode = "video/webm;codecs=\''vp8, opus\''";
                            break;
                    }
                    using (MySqlConnection mySqlConnection = new MySqlConnection(appSettings.connectionString))
                    {
                        mySqlConnection.Open();

                        MySqlCommand cmd = mySqlConnection.CreateCommand();
                        cmd.CommandText = Constants.InsertFileStmt;
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@Name", file.FileName);
                        cmd.Parameters.AddWithValue("@Path", file.FileName);
                        cmd.Parameters.AddWithValue("@Extension", extension);
                        cmd.Parameters.AddWithValue("@Size", fileSize);
                        cmd.Parameters.AddWithValue("@LastUpdatedAt", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                        cmd.Parameters.AddWithValue("@FileTypeCode", typeCode);

                        cmd.ExecuteNonQuery();
                        mySqlConnection.Close();
                    }

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                        fileSize = stream.Length;
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