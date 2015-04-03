
using System.Data;
using System.Data.SqlClient;
using Napoleon.Db;
using Napoleon.UserModule.IDAL;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.DAL
{
    public class ProjectDao : IProjectDao
    {

        /// <summary>
        ///  获取系统下拉框
        /// </summary>
        /// <returns>DataTable.</returns>
        /// Author  : Napoleon
        /// Created : 2015-01-27 11:23:40
        public DataTable GetProjecTable()
        {
            string sql = string.Format("SELECT ProjectId,ProjectName FROM dbo.System_Project");
            return DbHelper.GetDataTable(sql);
        }

        /// <summary>
        ///  获取系统列表
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-23 16:20:56
        public DataTable GetProjectTable(string projectId, string projectName, int startCount, int endCount)
        {
            string sql = "SELECT ProjectId,ProjectName,Remark,Operator FROM (SELECT ROW_NUMBER() OVER(ORDER BY ProjectId) AS number, ProjectId,ProjectName,Remark,Operator FROM dbo.System_Project) AS new WHERE new.ProjectId like @ProjectId and new.ProjectName like @ProjectName and new.number>@StartCount AND new.number <=@EndCount";
            SqlParameter[] parameters =
            {
                new SqlParameter("@ProjectId",string.Format("%{0}%",projectId)), 
                new SqlParameter("@ProjectName",string.Format("%{0}%",projectName)),
                new SqlParameter("@StartCount",startCount),
                new SqlParameter("@EndCount",endCount) 
            };
            return DbHelper.GetDataTable(sql, parameters);
        }

        /// <summary>
        ///  系统总数
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-23 16:31:28
        public int GetProjectCount()
        {
            string sql = string.Format("SELECT * FROM dbo.System_Project");
            DataTable dt = DbHelper.GetDataTable(sql);
            if (dt.Rows.Count <= 0)
            {
                return 0;
            }
            return dt.Rows.Count;
        }

        /// <summary>
        ///  新增系统
        /// </summary>
        /// <param name="project">The project.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 09:42:13
        public int InsertProject(SystemProject project)
        {
            string sql = "SELECT * FROM dbo.System_Project WHERE ProjectId=@ProjectId";
            SqlParameter[] parameters = { new SqlParameter("@ProjectId", project.ProjectId) };
            DataTable dt = DbHelper.GetDataTable(sql, parameters);
            int count;
            if (dt.Rows.Count > 0)
            {
                count = -1;
            }
            else
            {
                sql = "INSERT INTO dbo.System_Project ( ProjectId ,ProjectName ,Remark ,Operator) VALUES (@ProjectId,@ProjectName,@Remark,@Operator)";
                count = DbHelper.ExecuteSql(sql, project);
            }
            return count;
        }

        /// <summary>
        ///  根据ID查询
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 10:06:30
        public SystemProject GetProjectById(string projectId)
        {
            string sql = "SELECT * FROM dbo.System_Project WHERE ProjectId=@ProjectId";
            return DbHelper.GetEnumerable<SystemProject>(sql, new { ProjectId = projectId });
        }

        /// <summary>
        ///  新增系统
        /// </summary>
        /// <param name="project">The project.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 09:42:13
        public int UpdateProject(SystemProject project)
        {
            string sql = "UPDATE dbo.System_Project SET ProjectName=@projectName,Remark=@remark where ProjectId=@projectId";
            return DbHelper.ExecuteSql(sql, project);
        }

        /// <summary>
        ///  根据ID删除
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 10:43:31
        public int DeleteProject(string projectId)
        {
            string sql = "SELECT COUNT(*),(SELECT COUNT(*) FROM dbo.System_Rule AS sr WHERE sr.ProjectId='{0}') FROM dbo.System_Menu AS SM WHERE SM.ProjectId=@ProjectId";
            SqlParameter[] parameters = { new SqlParameter("@ProjectId", projectId) };
            DataTable dt = DbHelper.GetDataTable(sql, parameters);
            int count;
            if (dt.Rows[0][0].ToString().Equals("0") && dt.Rows[0][1].ToString().Equals("0"))
            {
                sql = "DELETE FROM dbo.System_Project WHERE ProjectId=@ProjectId";
                count = DbHelper.ExecuteSql(sql, new { ProjectId = projectId });
            }
            else
            {
                count = -1;
            }
            return count;
        }


    }
}

