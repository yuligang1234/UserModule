using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Napoleon.Log4Module.Log.Common;

namespace Napoleon.UserModule.DAL.Helper
{
    public static class DbHelper
    {

        private static string _connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        /// <summary>
        ///  MSSQL链接
        /// </summary>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-01 20:09:21
        private static readonly string SqlConnection = _connectionString.DecrypteRc2(LogField.Rc2);
        //private readonly static string SqlConnection = string.Format(@"Data Source=SKY-PC\SQL2005;Initial Catalog=UserModule;User Id=sa;Password=123456;");


        #region MSSQL数据库方法

        /// <summary>
        ///  打开数据库
        /// </summary>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-01 20:18:04
        public static SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(SqlConnection);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        /// <summary>
        ///  Common
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// Author  : Napoleon
        /// Created : 2015-02-09 16:54:18
        public static SqlCommand OpenCommand(string sql, SqlParameter[] parameters = null)
        {
            using (SqlCommand cmd = new SqlCommand(sql, OpenConnection()))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                return cmd;
            }
        }

        /// <summary>
        ///  初始化DataAdapter
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">parameters</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-12-15 13:27:05
        public static SqlDataAdapter OpenDataAdapter(string sql, SqlParameter[] parameters = null)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = OpenCommand(sql, parameters);
            return adapter;
        }

        #endregion

        #region 数据库通用操作

        /// <summary>
        ///  获取DataSet
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">parameters</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-01 20:18:16
        public static DataSet GetDataSet(string sql, SqlParameter[] parameters = null)
        {
            DataSet ds = new DataSet();
            IDbDataAdapter adapter = OpenDataAdapter(sql, parameters);
            adapter.Fill(ds);
            return ds;
        }

        /// <summary>
        ///  获取DateTable
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">parameters</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-22 09:51:02
        public static DataTable GetDataTable(string sql, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            DataSet ds = GetDataSet(sql, parameters);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        ///  增加、删除、修改操作
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">parameters</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-01 21:04:20
        public static int ExecuteSql(string sql, object parameters)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.Execute(sql, parameters);
            }
        }

        /// <summary>
        ///  查询个数
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">parameters</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-01 21:09:47
        public static int QueryCount(string sql, object parameters)
        {
            using (IDbConnection conn = OpenConnection())
            {
                int row;
                try
                {
                    row = (int)conn.ExecuteScalar(sql, parameters);
                }
                catch (Exception)
                {
                    row = 0;
                }
                return row;
            }
        }

        /// <summary>
        ///  获取实体类集合
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">parameters</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-01 20:41:32
        public static List<T> GetEnumerables<T>(string sql, object parameters) where T : new()
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.Query<T>(sql, parameters).ToList();
            }
        }

        /// <summary>
        ///  获取单个实体类
        /// </summary>
        /// <param name="sql">SQL.</param>
        /// <param name="parameters">parameters</param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-22 16:04:50
        public static T GetEnumerable<T>(string sql, object parameters) where T : new()
        {
            List<T> list = GetEnumerables<T>(sql, parameters);
            if (list.Count > 0)
            {
                return list[0];
            }
            return default(T);
        }

        /// <summary>
        ///  批量插入集合数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">SQL</param>
        /// <param name="t">List</param>
        /// Author  : Napoleon
        /// Created : 2015-02-04 20:16:27
        public static int InsertMultiple<T>(string sql, IEnumerable<T> t) where T : new()
        {
            using (IDbConnection conn = OpenConnection())
            {
                int count;
                using (IDbTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        count = conn.Execute(sql, t, transaction, 30, CommandType.Text);
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        count = -1;
                    }
                    transaction.Commit();
                }
                return count;
            }
        }

        #endregion



    }
}
