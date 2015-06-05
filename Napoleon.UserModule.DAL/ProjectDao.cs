
using System;
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
            DataTable dt = new DataTable();
            try
            {
                string sql = string.Format("SELECT ProjectId,ProjectName FROM dbo.System_Project");
                dt = DbHelper.GetDataTable(sql);
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return dt;
        }

        /// <summary>
        ///  获取系统列表
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-23 16:20:56
        public DataTable GetProjectTable(string projectId, string projectName, int startCount, int endCount)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT ProjectId,ProjectName,Remark,Operator FROM (SELECT ROW_NUMBER() OVER(ORDER BY ProjectId) AS number, ProjectId,ProjectName,Remark,Operator FROM dbo.System_Project) AS new WHERE new.ProjectId like @ProjectId and new.ProjectName like @ProjectName and new.number>@StartCount AND new.number <=@EndCount";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ProjectId",string.Format("%{0}%",projectId)), 
                    new SqlParameter("@ProjectName",string.Format("%{0}%",projectName)),
                    new SqlParameter("@StartCount",startCount),
                    new SqlParameter("@EndCount",endCount) 
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
        ///  系统总数
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-23 16:31:28
        public int GetProjectCount()
        {
            int i = 0;
            try
            {
                string sql = string.Format("SELECT * FROM dbo.System_Project");
                DataTable dt = DbHelper.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    i = dt.Rows.Count;
                }
            }
            catch (Exception exception)
            {
                i = -1;
                Log4Dao.InsertLog4(exception.Message);
            }
            return i;
        }

        /// <summary>
        ///  新增系统
        /// </summary>
        /// <param name="project">The project.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 09:42:13
        public int InsertProject(SystemProject project)
        {
            int count;
            try
            {
                string sql = "SELECT * FROM dbo.System_Project WHERE ProjectId=@ProjectId";
                SqlParameter[] parameters = { new SqlParameter("@ProjectId", project.ProjectId) };
                DataTable dt = DbHelper.GetDataTable(sql, parameters);
                if (dt.Rows.Count > 0)
                {
                    count = -1;
                }
                else
                {
                    sql = "INSERT INTO dbo.System_Project ( ProjectId ,ProjectName ,Remark ,Operator) VALUES (@ProjectId,@ProjectName,@Remark,@Operator)";
                    count = DbHelper.ExecuteSql(sql, project);
                }
            }
            catch (Exception exception)
            {
                count = 0;
                Log4Dao.InsertLog4(exception.Message);
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
            SystemProject project = new SystemProject();
            try
            {
                string sql = "SELECT * FROM dbo.System_Project WHERE ProjectId=@ProjectId";
                project = DbHelper.GetEnumerable<SystemProject>(sql, new { @ProjectId = projectId });
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return project;
        }

        /// <summary>
        ///  新增系统
        /// </summary>
        /// <param name="project">The project.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 09:42:13
        public int UpdateProject(SystemProject project)
        {
            int i;
            try
            {
                string sql = "UPDATE dbo.System_Project SET ProjectName=@projectName,Remark=@remark where ProjectId=@projectId";
                i = DbHelper.ExecuteSql(sql, project);
            }
            catch (Exception exception)
            {
                i = 0;
                Log4Dao.InsertLog4(exception.Message);
            }
            return i;
        }

        /// <summary>
        ///  根据ID删除
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 10:43:31
        public int DeleteProject(string projectId)
        {
            int count;
            try
            {
                string sql = "SELECT COUNT(*),(SELECT COUNT(*) FROM dbo.System_Rule AS sr WHERE sr.ProjectId='{0}') FROM dbo.System_Menu AS SM WHERE SM.ProjectId=@ProjectId";
                SqlParameter[] parameters = { new SqlParameter("@ProjectId", projectId) };
                DataTable dt = DbHelper.GetDataTable(sql, parameters);
                if (dt.Rows[0][0].ToString().Equals("0") && dt.Rows[0][1].ToString().Equals("0"))
                {
                    sql = "DELETE FROM dbo.System_Project WHERE ProjectId=@ProjectId";
                    count = DbHelper.ExecuteSql(sql, new { ProjectId = projectId });
                }
                else
                {
                    count = -1;
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

