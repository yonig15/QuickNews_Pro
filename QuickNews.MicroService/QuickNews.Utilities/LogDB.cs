﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNews.Utilities
{
    public class LogDB : ILogger
    {
        public static string ConnectionString = @"Integrated Security=SSPI;    Persist Security Info=False;    Initial Catalog=QuickNewsDB;    Data Source=localhost\\sqlexpress";

        string LogTableName = "LogTable";
        public void Init()
        {
            try
            {
                SqlQueryLog.ConnectionStringInit(ConnectionString);
                string CreateTableQuery = "IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LogTable' AND schema_name(schema_id) = 'dbo') \r\nBEGIN\r\n    CREATE TABLE dbo.LogTable(Code int primary key identity, \r\n    LogType nvarchar(max), LogTime datetime, messege nvarchar(max), ExceptionMsg nvarchar(MAX))\r\nEND";
                SqlQueryLog.WriteToDB(CreateTableQuery);
                LogCheckHouseKeeping();
            }
            catch (Exception ex)
            {
                LogException($"[Utilities] An Exception occurred while initializing the Init function in the LogDB class: {ex.Message}", ex);
            }
        }

        public void LogEvent(string message)
        {
            try
            {
                string sqlQuery = $"insert into {LogTableName} values('Event', getdate(),'{message}',null)";
                SqlQueryLog.WriteToDB(sqlQuery);
            }
            catch (Exception ex)
            {
                LogException($"[Utilities] An Exception occurred while initializing the LogEvent function in the LogDB class: {ex.Message}", ex);
            }
        }
        public void LogError(string message)
        {
            string sqlQuery = $"insert into {LogTableName} values('Error', getdate(),'{message}',null)";
            SqlQueryLog.WriteToDB(sqlQuery);
        }

        public void LogException(string message, Exception ex)
        {
            string sqlQuery = $"insert into {LogTableName} values('Exception', getdate(),'{message}', '{ex.Message}')";
            SqlQueryLog.WriteToDB(sqlQuery);
        }

        public void LogCheckHouseKeeping()
        {
            string sqlQuery = $"DELETE FROM {LogTableName}\r\nWHERE Date < DATEADD(month, -3, GETDATE());";
            SqlQueryLog.WriteToDB(sqlQuery);
        }
    }
}