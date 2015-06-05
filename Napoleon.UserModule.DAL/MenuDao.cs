
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Napoleon.Db;
using Napoleon.UserModule.IDAL;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.DAL
{
    public class MenuDao : IMenuDao
    {

        /// <summary>
        ///  根据projectId查询列表
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 15:11:11
        public DataTable GetMenuTable(string projectId)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT ProjectId,Id,Name,ParentId,HtmlId,Url,Icon,Sort,Remark,Operator,'false' as checked FROM dbo.System_Menu WHERE ProjectId=@ProjectId order by Sort";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ProjectId",projectId) 
                };
                dt = DbHelper.GetDataTable(sql, parameters);
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return dt;
        }

        /// <summary>
        ///  菜单父节点
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// <param name="parentId">parentId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 16:35:16
        public DataTable GetTreeParentId(string projectId, string parentId = "")
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT Id,Name FROM dbo.System_Menu WHERE ProjectId=@ProjectId ");
                if (!string.IsNullOrWhiteSpace(parentId))
                {
                    sb.AppendFormat("AND ParentId=@ParentId ");
                }
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ProjectId",projectId),
                    new SqlParameter("@ParentId",parentId)
                };
                dt = DbHelper.GetDataTable(sb.ToString(), parameters);
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return dt;
        }

        /// <summary>
        ///  新增菜单
        /// </summary>
        /// <param name="menu">The menu.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-26 14:18:32
        public int InsertMenu(SystemMenu menu)
        {
            int i;
            try
            {
                string sql = "INSERT INTO dbo.System_Menu (ProjectId ,Id ,Name ,ParentId,HtmlId ,Url ,Icon, Sort ,Remark ,Operator) VALUES(@ProjectId,@Id,@Name,@ParentId,@HtmlId,@Url,@Icon,@Sort,@Remark,@Operator)";
                i = DbHelper.ExecuteSql(sql, menu);
            }
            catch (Exception exception)
            {
                i = -1;
                Log4Dao.InsertLog4(exception.Message);
            }
            return i;
        }

        /// <summary>
        ///  根据Id查询
        /// </summary>
        /// <param name="id">id</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 15:11:11
        public SystemMenu GetMenu(string id)
        {
            SystemMenu menu = new SystemMenu();
            try
            {
                string sql = "SELECT ProjectId,Id,Name,ParentId,HtmlId,Url,Icon,Sort,Remark,Operator FROM dbo.System_Menu WHERE Id=@Id order by Sort";
                menu = DbHelper.GetEnumerable<SystemMenu>(sql, new { @Id = id });
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return menu;
        }

        /// <summary>
        ///  更新菜单
        /// </summary>
        /// <param name="menu">The menu.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-26 16:06:40
        public int UpdateMenu(SystemMenu menu)
        {
            int count;
            try
            {
                string sql = "UPDATE dbo.System_Menu SET Name=@name,ParentId=@parentId,HtmlId=@htmlId,Url=@url,Icon=@icon,Sort=@sort,Remark=@remark,Operator=@operator WHERE Id=@id";
                count = DbHelper.ExecuteSql(sql, menu);
            }
            catch (Exception exception)
            {
                count = -1;
                Log4Dao.InsertLog4(exception.Message);
            }
            return count;
        }

        /// <summary>
        ///  根据ID删除
        /// </summary>
        /// <param name="id">id</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 10:43:31
        public int DeleteMenu(string id)
        {
            int count;
            try
            {
                string sql = "SELECT * FROM dbo.System_Menu WHERE ParentId=@ParentId";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ParentId", id)
                };
                DataTable dt = DbHelper.GetDataTable(sql, parameters);
                if (dt.Rows.Count > 0)
                {
                    count = -1;
                }
                else
                {
                    sql = "DELETE FROM dbo.System_Menu WHERE Id=@Id";
                    count = DbHelper.ExecuteSql(sql, new { @Id = id });
                }
            }
            catch (Exception exception)
            {
                count = 0;
                Log4Dao.InsertLog4(exception.Message);
            }
            return count;
        }

    }
}
