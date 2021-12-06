namespace LanFlexWebAPI.Helpers
{
    public static class Constants
    {
        public static string VideoSelectStmt = "SELECT * FROM FilesDetails WHERE Extension in ('mp4', 'mkv', 'webm')";
    }
}