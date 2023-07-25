using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace NewsFeedApp.Data
{
    public class SqlQueryExecutor
    {
        public string ConnectionString { get; set; }

        public SqlQueryExecutor(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, string connection = null)
        {
            connection = connection == null ? ConnectionString : ConfigurationManager.AppSettings[connection];
            using (var conn = new SqlConnection(connection))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                else if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Connecting)
                {
                    conn.Close();
                    conn.Open();
                }
                return conn.Query<T>(sql, param, commandType: commandType, commandTimeout: 60);
            }
        }

        public int Execute(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, string connection = null)
        {
            connection = connection == null ? ConnectionString : ConfigurationManager.AppSettings[connection];
            using (var conn = new SqlConnection(connection))
            {
                if (conn.State == ConnectionState.Closed)
                {

                    conn.Open();
                }
                else if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Connecting)
                {
                    conn.Close();
                    conn.Open();
                }
                return conn.Execute(sql, param, commandType: commandType);
            }
        }

        public object ExecuteScalar(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, string connection = null)
        {
            connection = connection == null ? ConnectionString : ConfigurationManager.AppSettings[connection];
            using (var conn = new SqlConnection(connection))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                else if (conn.State == ConnectionState.Broken || conn.State == ConnectionState.Connecting)
                {
                    conn.Close();
                    conn.Open();
                }
                return conn.ExecuteScalar(sql, param, commandType: commandType);
            }
        }

        public int ExecuteWithParameters(string sql, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure, string connection = null)
        {
            connection = connection == null ? ConnectionString : ConfigurationManager.AppSettings[connection];
            using (var conn = new SqlConnection(connection))
            {
                conn.Open();
                return conn.Execute(sql, parameters, commandType: commandType);
            }
        }

        public DataTable GetDataTable(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, string connection = null)
        {
            return Query<object>(sql, param, commandType, connection: connection).ToList().ToDataTable();
        }
        public DataTable GetDataTableSelect(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, string connection = null)
        {
            return Query<object>(sql, param, commandType, connection: connection).ToList().ToDataTableSelect();
        }
        public IDataReader GetReader<T>(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, string connection = null)
        {
            connection = connection == null ? ConnectionString : ConfigurationManager.AppSettings[connection];

            using (var conn = new SqlConnection(connection))
            {
                conn.Open();
                return conn.ExecuteReader(sql, param, commandType: commandType);
            }
        }

        public IDataReader GetReader(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure, string connection = null)
        {
            connection = connection == null ? ConnectionString : ConfigurationManager.AppSettings[connection];

            using (var conn = new SqlConnection(connection))
            {
                conn.Open();
                return conn.ExecuteReader(sql, param, commandType: commandType);
            }
        }

        public T ExecuteWithReturnValue<T>(string sql, string ReturnParameter, DynamicParameters param = null, CommandType commandType = CommandType.StoredProcedure, string connection = null)
        {
            connection = connection == null ? ConnectionString : ConfigurationManager.AppSettings[connection];
            using (var conn = new SqlConnection(connection))
            {
                conn.Open();
                conn.Execute(sql, param, commandType: commandType);
                return param.Get<T>(ReturnParameter);
            }
        }
        public DataTable DataTableWithReturnValue<T>(string sql, string ReturnParameter, DynamicParameters param = null, CommandType commandType = CommandType.StoredProcedure, string connection = null)
        {
            connection = connection == null ? ConnectionString : ConfigurationManager.AppSettings[connection];
            using (var conn = new SqlConnection(connection))
            {
                conn.Open();
                var list = conn.Query<object>(sql, param, commandType: commandType);
                var dt = new DataTable();
                var returnValue = string.Empty;
                if (!list.Select(x => x).FirstOrDefault().ToString().ToLower().Contains("success"))
                {
                    var error = conn.Query<T>(sql, param, commandType: commandType);
                    dt.Columns.Add("result");
                    dt.Rows.Add(error.Select(x => x).FirstOrDefault());
                    return dt;
                }
                else
                {
                    dt = GetDataTable(sql, param, commandType: commandType);
                    dt.Columns.Add("rid");
                    dt.Rows[0][2] = param.Get<T>(ReturnParameter);
                    return dt;
                }
            }
        }

        internal void Execute(object sp_add_card_payment)
        {
            throw new NotImplementedException();
        }

        internal object Query<T>(object gET_APPLICATION_EXCEMPTION_BY_TYPE, object p, CommandType text)
        {
            throw new NotImplementedException();
        }
    }
    static class ListExtensions
    {
        public static DataTable ToDataTable(this List<object> d)
        {
            dynamic json = JsonConvert.SerializeObject(d);
            return JsonConvert.DeserializeObject<DataTable>(json);
        }
        public static DataTable ToDataTableSelect(this List<object> d)
        {
            dynamic json = JsonConvert.SerializeObject(d);
            if (json != null)
            {
                string text = json;
                text = text.Replace("\"\":", "\"status\":");
                json = text;
            }
            return JsonConvert.DeserializeObject<DataTable>(json);
        }
    }
}
