using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace LanApi.Models
{
    public class VideoModel
    {
        public List<IFormFile> Videos { get; set; }
    }
}
