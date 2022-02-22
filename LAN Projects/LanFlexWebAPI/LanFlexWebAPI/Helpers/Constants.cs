namespace LanFlexWebAPI.Helpers
{
    public static class Constants
    {
        public static string AllSelectStmt = "SELECT * FROM FilesDetails";
        public static string VideoCountStmt = "SELECT count(1) as VideoCount FROM FilesDetails WHERE Extension in ('mp4', 'mkv', 'webm', 'avi')";
        public static string AudioCountStmt = "SELECT count(1) as AudioCount FROM FilesDetails WHERE Extension in ('mp3', 'm4a')";
        public static string ImagesCountStmt = "SELECT count(1) as ImageCount FROM FilesDetails WHERE Extension in ('jpeg', 'jpg', 'png')";
        public static string TotalCountStmt = "SELECT count(1) as TotalCount FROM FilesDetails";
        public static string VideoSelectStmt = "SELECT * FROM FilesDetails WHERE Extension in ('mp4', 'mkv', 'webm', 'avi')";
        public static string AudioSelectStmt = "SELECT * FROM FilesDetails WHERE Extension in ('mp3', 'm4a')";
        public static string InsertFileStmt = "INSERT INTO `lanflex`.`filesdetails`(`Name`, `Path`, `Extension`, `Size`, `LastUpdatedAt`, `CreatedAt`)" +
            "VALUES (@Name, @Path, @Extension, @Size, @LastUpdatedAt, @CreatedAt)";
        public static string UploadFolderName = "/UploadedFiles/";
    }
}