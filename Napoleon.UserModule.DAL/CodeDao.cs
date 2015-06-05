using System;
using System.Data;
using System.Data.SqlClient;
using Napoleon.Db;
using Napoleon.UserModule.IDAL;

namespace Napoleon.UserModule.DAL
{
    public class CodeDao : ICodeDao
    {

        /// <summary>
        ///  根据父节点获取下拉框内容
        /// </summary>
        /// <param name="parentId">parentId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-19 09:29:19
        public DataTable GetTableByParentId(string parentId)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT Id,Name FROM dbo.System_Code WHERE ParentId=@ParentId";
                SqlParameter[] parameters = { new SqlParameter("@ParentId", parentId) };
                dt = DbHelper.GetDataTable(sql, parameters);
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return dt;
        }

    }
}
