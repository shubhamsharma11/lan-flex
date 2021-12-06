using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LanApi.Models;
using System.IO;
using NReco.VideoConverter;
using Xabe.FFmpeg;

namespace LanApi.Controllers
{
    [Route("api/[controller]")]
    public class VideoController : Controller
    {
        // POST api/uploadVideo
        //[HttpPost]
        //public ActionResult Post([FromForm]VideoModel videoModel)
        //{
        //    var video = videoModel.Video;

        //    // Saving video on Server
        //    if (video.Length > 0)
        //    {
        //        using (var fileStream = new FileStream(video.FileName, FileMode.Create))
        //        {
        //            video.CopyTo(fileStream);
        //        }
        //    }
        //    return Ok(new { status = true, message = "Student Posted Successfully" });
        //}

        // POST api/Video
        //[HttpPost]
        //public ActionResult Post([FromForm]VideoModel videoModel)
        //{
        //    try
        //    {
        //        var video = videoModel.Video;

        //        // Saving video on Server
        //        if (video.Length > 0)
        //        {
        //            using (var fileStream = new FileStream(video.FileName, FileMode.Create))
        //            {
        //                video.CopyTo(fileStream);
        //            }
        //        }
        //        return Ok(new { status = true, message = "Student Posted Successfully" });
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(new { status = false, message = ex.Message });
        //    }
        //}

        // POST api/Video[FromForm]
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> PostAsync([FromForm] VideoModel videoModel)
        {
            //try
            //{
            //    foreach (var video in videoModel.Videos)
            //    {
            //        // Saving video on Server
            //        if (video.Length > 0)
            //        {
            //            using (var fileStream = new FileStream(video.FileName, FileMode.Create))
            //            {
            //                video.CopyTo(fileStream);
            //            }

            //            //var ffMpeg = new FFMpegConverter();
            //            //ffMpeg.ConvertMedia(video.FileName, video.FileName.Substring(0, video.FileName.LastIndexOf('.')) + ".webm", NReco.VideoConverter.Format.mp4);

            //            await FFmpeg.Conversions.FromSnippet.ToWebM(video.FileName, video.FileName.Substring(0, video.FileName.LastIndexOf('.')) + ".webm");

            //            //var ffMpeg = new FFMpegConverter();
            //            //ffMpeg.ConvertMedia(video.FileName, video.FileName.Substring(0, video.FileName.LastIndexOf('.') + 1) + ".webm", Format.webm);
            //        }
            //    }
            //    return Ok(new { status = true, message = "Video Uploaded Successfully" });
            //}
            //catch (System.Exception ex)
            //{
            //    return BadRequest(new { status = false, message = ex.Message });
            //}
            return Ok();
        }


        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}


        //[HttpPost]
        //public HttpResponseMessage FileUpload()
        //{
        //    HttpResponseMessage result = null;
        //    var httpRequest = HttpContext.Request;
        //    if (httpRequest.Files.Count > 0)
        //    {
        //        var docfiles = new List<string>();
        //        foreach (string file in httpRequest.Files)
        //        {
        //            var postedFile = httpRequest.Files[file];
        //            var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
        //            postedFile.SaveAs(filePath);
        //            docfiles.Add(filePath);
        //        }
        //        result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
        //    }
        //    else
        //    {
        //        result = Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }
        //    return result;
        //}


        // GET api/values
        [HttpGet]
        public IEnumerable<string> Show()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
