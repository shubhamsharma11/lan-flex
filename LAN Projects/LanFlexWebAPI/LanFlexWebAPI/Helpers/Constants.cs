namespace LanFlexWebAPI.Helpers
{
    public static class Constants
    {
        public static string VideoSelectStmt = "SELECT * FROM FilesDetails WHERE Extension in ('mp4', 'mkv', 'webm')";
        public static string InsertFileStmt = "INSERT INTO `lanflex`.`filesdetails`(`Name`, `Path`, `Extension`, `Size`, `LastUpdatedAt`, `CreatedAt`, `FileTypeCode`)" +
            "VALUES (@Name, @Path, @Extension, @Size, @LastUpdatedAt, @CreatedAt, @FileTypeCode)";
        public static string UploadFolderName = "/UploadedFiles/";
    }
}