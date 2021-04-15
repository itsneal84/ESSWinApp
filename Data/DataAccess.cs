using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using Microsoft.Data.Sqlite;
using ESSWinApp.Core.Models;

namespace ESSWinApp.Data
{
    public static class DataAccess
    {
        #region Initialize

        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("esswin.db",
                CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "esswin.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                string userTableCommand = "CREATE TABLE IF NOT " + "EXISTS User (Primary_Key INTEGER PRIMARY KEY, " +
                                      "username NVARCHAR(2048) NULL," + "password NVARCHAR(2048) NULL," +
                                      "token NVARCHAR(2048) NULL," + "deviceIp NVARCHAR(2048) NULL," +
                                      "unique (username, password))";

                SqliteCommand createUserTable = new SqliteCommand(userTableCommand, db);

                createUserTable.ExecuteReader();

                string newDataTableCommand = "CREATE TABLE IF NOT " + "EXISTS NewData (Primary_Key INTEGER PRIMARY KEY, " +
                                          "type NVARCHAR(2048) NULL," + "device_ip NVARCHAR(2048) NULL," +
                                          "file_name NVARCHAR(2048) NULL," + "name NVARCHAR(2048) NULL," +
                                          "date_detected NVARCHAR(2048) NULL," + "time_detected NVARCHAR(2048) NULL)";

                SqliteCommand createNewDataTable = new SqliteCommand(newDataTableCommand, db);

                createNewDataTable.ExecuteReader();

                string detectedFacesTableCommand = "CREATE TABLE IF NOT " + "EXISTS DetectedFacesView (Primary_Key INTEGER PRIMARY KEY, " +
                                             "file_name NVARCHAR(2048) NULL," + "name NVARCHAR(2048) NULL," +
                                             "date_detected NVARCHAR(2048) NULL," + "time_detected NVARCHAR(2048) NULL)";

                SqliteCommand createDetectedFacesTable = new SqliteCommand(detectedFacesTableCommand, db);

                createDetectedFacesTable.ExecuteReader();

                string unknownImageTableCommand = "CREATE TABLE IF NOT " + "EXISTS UnknownImage (Primary_Key INTEGER PRIMARY KEY, " +
                                                   "file_name NVARCHAR(2048) NULL," + "name NVARCHAR(2048) NULL," +
                                                   "date_found NVARCHAR(2048) NULL," + "time_found NVARCHAR(2048) NULL)";

                SqliteCommand createUnknownImageTable = new SqliteCommand(unknownImageTableCommand, db);

                createUnknownImageTable.ExecuteReader();

                string deviceTableCommand = "CREATE TABLE IF NOT " + "EXISTS Device (Primary_Key INTEGER PRIMARY KEY, " +
                                                  "device_name NVARCHAR(2048) NULL," + "device_type NVARCHAR(2048) NULL," +
                                                  "deviceIp NVARCHAR(2048) NULL," + "face_detection NVARCHAR(2048) NULL," +
                                                  "motion NVARCHAR(2048) NULL," + "stream_link NVARCHAR(2048) NULL)";

                SqliteCommand createDeviceTable = new SqliteCommand(deviceTableCommand, db);

                createDeviceTable.ExecuteReader();
            }
        }

        #endregion

        #region Add

        public static void AddUserData(UserModel userModel)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "esswin.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO User(username, password, token, deviceIp) VALUES (@username, @password, @deviceIp);";
                insertCommand.Parameters.AddWithValue("@username", UserModel.Name);
                insertCommand.Parameters.AddWithValue("@password", userModel.Password);
                insertCommand.Parameters.AddWithValue("@deviceIp", UserModel.DeviceIp);
                insertCommand.Prepare();

                insertCommand.ExecuteNonQuery();
                db.Close();
            }
        }

        public static void AddNewData(string inputText)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "esswin.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO NewData VALUES (NULL, @Entry);";
                insertCommand.Parameters.AddWithValue("@Entry", inputText);

                insertCommand.ExecuteReader();
                db.Close();
            }
        }

        public static void AddDetectedFacesData(string inputText)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "esswin.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO DetectedFacesView VALUES (NULL, @Entry);";
                insertCommand.Parameters.AddWithValue("@Entry", inputText);

                insertCommand.ExecuteReader();
                db.Close();
            }
        }

        public static void AddUnknownImageData(string inputText)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "esswin.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO UnknownImage VALUES (NULL, @Entry);";
                insertCommand.Parameters.AddWithValue("@Entry", inputText);

                insertCommand.ExecuteReader();
                db.Close();
            }
        }

        public static void AddDeviceData(string inputText)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "esswin.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                insertCommand.CommandText = "INSERT INTO Device VALUES (NULL, @Entry);";
                insertCommand.Parameters.AddWithValue("@Entry", inputText);

                insertCommand.ExecuteReader();
                db.Close();
            }
        }

        #endregion

        #region Get

        public static List<string> GetUserData()
        {
            List<string> entries = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "esswin.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand("SELECT Text_Entry from User", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                }

                db.Close();
            }

            return entries;
        }

        #endregion
    }
}
