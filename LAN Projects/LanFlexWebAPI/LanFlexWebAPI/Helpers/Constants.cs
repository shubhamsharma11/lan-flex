namespace LanFlexWebAPI.Helpers
{
    public static class Constants
    {
        public static string AllSelectStmt = "SELECT * FROM FilesDetails";
        public static string VideoCountStmt = "SELECT count(1) as VideoCount FROM FilesDetails WHERE Type = 'Video'";
        public static string AudioCountStmt = "SELECT count(1) as AudioCount FROM FilesDetails WHERE Type = 'Audio'";
        public static string ImagesCountStmt = "SELECT count(1) as ImageCount FROM FilesDetails WHERE Type = 'Image'";
        public static string TotalCountStmt = "SELECT count(1) as TotalCount FROM FilesDetails";
        public static string VideoSelectStmt = "SELECT * FROM FilesDetails WHERE Type = 'Video'";
        public static string AudioSelectStmt = "SELECT * FROM FilesDetails WHERE Type = 'Audio'";
        public static string ImagesSelectStmt = "SELECT * FROM FilesDetails WHERE Type = 'Image'";
        public static string HistorySelectStmt = "SELECT fd.Name, fd.Type, h.UploadedTime FROM History h INNER JOIN filesdetails fd WHERE h.UploadedFileId = fd.FileId ORDER BY h.UploadedTime";
        public static string InsertFileStmt = "INSERT INTO `lanflex`.`filesdetails`(`Name`, `Path`, `Extension`, `Type`, `LastUpdatedAt`, `CreatedAt`)" +
            "VALUES (@Name, @Path, @Extension, @Type, @LastUpdatedAt, @CreatedAt)";
        public static string UploadFolderName = "/UploadedFiles";
    }
}