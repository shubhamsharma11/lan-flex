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

        /// <summary>
        /// GET data/GetHomeStats
        /// To get list of all the Audios
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetHomeStats")]
        public IActionResult GetHomeStats()
        {
            int video, audio, image, total;

            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(appSettings.connectionString))
                {
                    mySqlConnection.Open();

                    MySqlCommand cmd = mySqlConnection.CreateCommand();

                    cmd.CommandText = Constants.VideoCountStmt;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    video = reader.GetInt32("VideoCount");
                    reader.Close();


                    cmd.CommandText = Constants.AudioCountStmt;
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    audio = reader.GetInt32("AudioCount");
                    reader.Close();


                    cmd.CommandText = Constants.ImagesCountStmt;
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    image = reader.GetInt32("ImageCount");
                    reader.Close();

                    cmd.CommandText = Constants.TotalCountStmt;
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    total = reader.GetInt32("TotalCount");
                    reader.Close();
                }

                return Ok(JsonConvert.SerializeObject(new
                {
                    VideoCount = video,
                    AudioCount = audio,
                    ImageCount = image,
                    TotalCount = total
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// GET video/GetAudios
        /// To get list of all the Audios
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAudios")]
        public IActionResult GetAudios()
        {
            List<FileModel> audioList = new List<FileModel>();

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
                        audioList.Add(new FileModel()
                        {
                            Name = reader.GetString("Name"),
                            Extension = reader.GetString("Extension"),
                            FilePath = reader.GetString("Path"),
                            FileId = reader.GetInt32("FileId"),
                            ThumbPath = reader.IsDBNull(7) ? null : reader.GetString("ThumbPath"),
                            Details = reader.IsDBNull(8) ? null : reader.GetString("Details")
                        });
                    }
                    reader.Close();
                }

                return Ok(JsonConvert.SerializeObject(audioList));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// GET video/GetVideos
        /// To get list of all the videos
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetVideos")]
        public IActionResult GetVideos()
        {
            List<FileModel> videoList = new List<FileModel>();

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
                        videoList.Add(new FileModel()
                        {
                            Name = reader.GetString("Name"),
                            Extension = reader.GetString("Extension"),
                            FilePath = reader.GetString("Path"),
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

        /// <summary>
        /// POST video/UploadFile
        /// To Upload the file from the FormData
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        [RequestSizeLimit(maxFileSize)]
        [RequestFormLimits(MultipartBodyLengthLimit = maxFileSize)]
        [HttpPost("FileUpload")]
        public ActionResult FileUpload(IFormCollection formData)
        {
            IFormFile file = formData.Files[0];
            string fileTitle = formData["name"];
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
                    var fileSize = 0L;

                    using (MySqlConnection mySqlConnection = new MySqlConnection(appSettings.connectionString))
                    {
                        mySqlConnection.Open();

                        MySqlCommand cmd = mySqlConnection.CreateCommand();
                        cmd.CommandText = Constants.InsertFileStmt;
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@Name", string.IsNullOrEmpty(fileTitle) ? file.FileName : fileTitle);
                        cmd.Parameters.AddWithValue("@Path", file.FileName);
                        cmd.Parameters.AddWithValue("@Extension", extension);
                        cmd.Parameters.AddWithValue("@Size", fileSize);
                        cmd.Parameters.AddWithValue("@LastUpdatedAt", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

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
