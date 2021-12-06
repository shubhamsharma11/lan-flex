using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanFlexAPI.Models.Helper
{
    public static class Constants
    {
        public static AppSettings appSettings = new AppSettings();
        public static string VideoSelectStmt = "SELECT * FROM FilesDetails WHERE Extension in ('mp4', 'mkv', 'webm')";
    }
}