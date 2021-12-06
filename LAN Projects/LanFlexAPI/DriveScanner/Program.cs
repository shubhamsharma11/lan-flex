using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace DriveScanner
{
    class Program
    {
        public static string connectionString = @"server=127.0.0.1;port=3306;uid=root;pwd=Shivam@2000!;database=lanflex";
        public static string path = @"D:\Projects\LAN Projects\LanUI\public\LanFlexData";
        public static string parentDir = "LanFlexData";

        public static List<FileInfo> FilesInfo = new List<FileInfo>();

        static void Main(string[] args)
        {
            GetAllFiles(path);
            InsertData();
            //FetchData();
        }

        /// <summary>
        /// Get all the files of the provided path
        /// </summary>
        /// <param name="path"></param>
        public static void GetAllFiles(string path)
        {
            GetFiles(path);
            GetDirectories(path);
        }

        /// <summary>
        /// Scans the path to get the directories
        /// </summary>
        /// <param name="path"></param>
        public static void GetDirectories(string path)
        {
            List<string> directories = Directory.GetDirectories(path).ToList();
            foreach (string directory in directories)
            {
                GetFiles(directory);
                GetDirectories(directory);
            }
        }

        /// <summary>
        /// Scans the path to get the files
        /// </summary>
        /// <param name="path"></param>
        public static void GetFiles(string path)
        {
            List<string> files = Directory.GetFiles(path).ToList();
            foreach (string file in files)
            {
                FilesInfo.Add(new FileInfo(file));
            }
        }

        /// <summary>
        /// Draws the progress bar
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="total"></param>
        private static void drawTextProgressBar(int progress, int total)
        {
            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.CursorLeft = 32;
            Console.CursorLeft = 1;
            float onechunk = 30.0f / total;

            //draw filled part
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw unfilled part
            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progress.ToString() + " of " + total.ToString() + "    "); //blanks at the end remove any excess

            if (progress == total)
            {
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// Insert the data into the database
        /// </summary>
        private static void InsertData()
        {

            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    StringBuilder sCommand = new StringBuilder(@"INSERT INTO filesdetails (Name, Path, Extension, Size, LastUpdatedAt, CreatedAt, FileTypeCode) VALUES ");
                    List<string> Rows = new List<string>();

                    Console.WriteLine("Storing the data into the database...");

                    int i = 0;
                    foreach (var fileInfo in FilesInfo)
                    {
                        drawTextProgressBar(i++, FilesInfo.Count - 1);

                        var extension = fileInfo.Extension.Contains('.') ? fileInfo.Extension.Split('.')[1] : fileInfo.Extension;
                        var typeCode = string.Empty;

                        switch (extension)
                        {
                            case "mp4":
                                typeCode = "video/mp4;codecs=\''avc1.4D401E, mp4a.40.2\''";
                                break;
                            case "webm":
                                typeCode = "video/webm;codecs=\''vp8, opus\''";
                                break;
                        }

                        Rows.Add(string.Format(@"('{0}', '{1}', '{2}', {3}, '{4}', '{5}', '{6}')", fileInfo.Name,
                            MySqlHelper.EscapeString(fileInfo.FullName.Substring(fileInfo.DirectoryName.IndexOf(parentDir) - 1)),
                            extension, fileInfo.Length, fileInfo.LastWriteTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),
                            fileInfo.CreationTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"), typeCode));
                    }

                    sCommand.Append(string.Join(",", Rows));
                    sCommand.Append(";");

                    mySqlConnection.Open();
                    using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mySqlConnection))
                    {
                        myCmd.CommandType = CommandType.Text;
                        myCmd.ExecuteNonQuery();
                    }
                    Console.WriteLine("Done.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There is an Exception while storing the data into the database \n" + ex);
            }
        }

        private static void FetchData()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    MySqlCommand cmd = mySqlConnection.CreateCommand();
                    cmd.CommandText = "SELECT * FROM filesdetails";

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine(reader[0] + " -- " + reader[1]);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}






//GetAllFiles(@"D:\Projects");


////for (int i = 0; i < filePath.length; i++)
////{
////    string FileName = Path.GetFileName(filePath[i]);
////    //copy the files
////    oSftp.Put(LocalDirectory + "/" + FileName, _ftpDirectory + "/" + FileName);
////    //Console.WriteLine("Uploading file..." + FileName);
////    drawTextProgressBar(i, totalCount);
////}

////foreach (FileInfo fileInfo in FilesInfo)
////{
////    string FileName = Path.GetFileName(file);
////    //copy the files
////    oSftp.Put(LocalDirectory + "/" + FileName, _ftpDirectory + "/" + FileName);
////    //Console.WriteLine("Uploading file..." + FileName);
////    drawTextProgressBar(0, totalCount);
////}


////Console.WriteLine(FilesInfo[0].FullName);
////Console.WriteLine(FilesInfo[0].Name);
////Console.WriteLine(FilesInfo[0].Length);
////Console.WriteLine(FilesInfo[0].LastWriteTime);
////Console.WriteLine(FilesInfo[0].CreationTime);
////Console.WriteLine(FilesInfo[0].Extension);

////        D:\Projects\LAN Projects\LanApi\LanApi.sln
////LanApi.sln
////1614
////10 / 27 / 2021 10:24:17 AM
////10 / 26 / 2021 8:55:59 PM
////    .sln

//string connectionString = @"server=127.0.0.1;uid=root;pwd=Shivam@2000!;database=lanflex";

//try
//{
//    using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
//    {
//        mySqlConnection.Open();

//        MySqlCommand cmd = mySqlConnection.CreateCommand();
//        cmd.CommandText = "INSERT INTO filesdetails (Name, Path, Extension, Size, LastUpdatedAt, CreatedAt)" +
//            "VALUES (@Name, @Path, @Extension, @Size, @LastUpdatedAt, @CreatedAt)";

//        cmd.Parameters.AddWithValue("@Name", FilesInfo[0].Name);
//        cmd.Parameters.AddWithValue("@Path", FilesInfo[0].FullName);
//        cmd.Parameters.AddWithValue("@Extension", FilesInfo[0].Extension);
//        cmd.Parameters.AddWithValue("@Size", FilesInfo[0].Length);
//        cmd.Parameters.AddWithValue("@LastUpdatedAt", FilesInfo[0].LastWriteTime);
//        cmd.Parameters.AddWithValue("@CreatedAt", FilesInfo[0].CreationTime);

//        cmd.ExecuteNonQuery();

//        //MySqlDataReader reader = cmd.ExecuteReader();

//        //while (reader.Read())
//        //{
//        //    Console.WriteLine(reader[0] + " -- " + reader[1]);
//        //}
//        //reader.Close();
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex);
//}

////Console.WriteLine("Storing the data into the database...");
////for (int i = 0; i < FilesInfo.Count; i++)
////{
////    drawTextProgressBar(i, FilesInfo.Count - 1);
////}

//List<string> Rows = new List<string>();
//for (int i = 0; i < FilesInfo.Count; i++)
//{
//    Rows.Add(string.Format("('{0}','{1}')", MySqlHelper.EscapeString("test"), MySqlHelper.EscapeString("test")));
//}
//sCommand.Append(string.Join(",", Rows));
//sCommand.Append(";");
//mConnection.Open();
//using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
//{
//    myCmd.CommandType = CommandType.Text;
//    myCmd.ExecuteNonQuery();
//}