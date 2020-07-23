using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace ProDatabase.ACDataBase
{
    public static class OleDbHelper
    {
        private static OleDbConnection connection;
        /// <summary>
        /// 获得一个唯一的CONNECTION 实例
        /// </summary>
        public static OleDbConnection Connection
        {
            get
            {
                string mdbPath = "E://Project//szxwx//VisionBasedAutomationProject//ProSplitBoard//bin//Debug//Data//ProSplitBoard.mdb";
                //string mdbPath = "E://Personal File//PersonalProject//SplitBoard//ChenLing//VisionBasedAutomationProject//ProSplitBoard//bin//Debug//Data//ProSplitBoard.mdb";
                string connectionstring 
                    = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + mdbPath;

                if (connection == null)
                {
                    connection = new OleDbConnection(connectionstring);
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }
                return connection;
            }
        }
        /// 返回执行SQL 语句所影响数据的行数
        public static int ExecuteCommand(string sql)
        {
            OleDbCommand com = new OleDbCommand(sql, Connection);
            int result = com.ExecuteNonQuery();
            return result;
        }
        /// 获取结果集的第一行第一列
        public static int GetScalar(string sql)
        {
            OleDbCommand com = new OleDbCommand(sql, Connection);
            int result = int.Parse(com.ExecuteScalar().ToString());
            return result;
        }
        /// 获得第数据
        public static OleDbDataAdapter GetDBDataAdapter(string sql)
        {
            OleDbDataAdapter da = new OleDbDataAdapter(sql,Connection);
            OleDbCommandBuilder builder = new OleDbCommandBuilder(da);
            da.InsertCommand = builder.GetInsertCommand();
            da.UpdateCommand = builder.GetUpdateCommand();
            da.DeleteCommand = builder.GetDeleteCommand();
            return da;
        }
        public static OleDbDataAdapter GetDBDataAdapterEx(string sql)
        {
            OleDbDataAdapter da = new OleDbDataAdapter(sql, Connection);
            OleDbCommandBuilder builder = new OleDbCommandBuilder(da);
            return da;
        }
        public static OleDbDataReader GetReader(string sql)
        {
            OleDbCommand com = new OleDbCommand(sql, Connection);
            OleDbDataReader reader = com.ExecuteReader();
            return reader;
        }
    }
}
