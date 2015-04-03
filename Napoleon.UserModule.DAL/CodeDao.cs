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
            string sql = "SELECT Id,Name FROM dbo.System_Code WHERE ParentId=@ParentId";
            SqlParameter[] parameters = { new SqlParameter("@ParentId", parentId) };
            return DbHelper.GetDataTable(sql, parameters);
        }

    }
}
