
using System;
using System.Data;
using System.Data.SqlClient;
using Napoleon.Db;
using Napoleon.UserModule.IDAL;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.DAL
{
    public class UserAndRuleDao : IUserAndRuleDao
    {

        /// <summary>
        ///  获取权限ID
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="projectId">系统代码</param>
        /// Author  : Napoleon
        /// Created : 2015-01-20 13:24:49
        public SystemUserAndRule GetRule(string userId, string projectId)
        {
            string sql = "SELECT Id,UserId,RuleId,RuleParentId FROM dbo.System_UserAndRule WHERE UserId=@UserId AND ProjectId=@ProjectId";
            return DbHelper.GetEnumerable<SystemUserAndRule>(sql, new { UserId = userId, ProjectId = projectId });
        }

        /// <summary>
        ///  获取用户所在角色的列表
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startCount">The start count.</param>
        /// <param name="endCount">The end count.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-21 15:38:44
        public DataTable GetRuleTable(string userId, int startCount, int endCount)
        {
            string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY uar.Id) AS number,uar.ProjectId,uar.Id,uar.UserId,sr1.Name AS Company,sr.Name AS RuleName FROM dbo.System_UserAndRule AS uar LEFT JOIN dbo.System_Rule AS sr ON sr.Id=uar.RuleId LEFT JOIN dbo.System_Rule AS sr1 ON sr1.Id=uar.RuleParentId  WHERE UserId =@UserId) AS new WHERE new.number>@StartCount AND new.number<=@EndCount";
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId",userId),
                new SqlParameter("@StartCount",startCount),
                new SqlParameter("@EndCount",endCount) 
            };
            return DbHelper.GetDataTable(sql, parameters);
        }

        /// <summary>
        ///  用户所在角色的总数
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-21 15:41:22
        public int GetRuleCount(string userId)
        {
            string sql = "SELECT count(*) FROM dbo.System_UserAndRule WHERE UserId =@UserId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId",userId) 
            };
            DataTable dt = DbHelper.GetDataTable(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }

        /// <summary>
        ///  根据ID删除用户对应的权限数据
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-21 15:56:01
        public int DeleteRuleById(string id)
        {
            string sql = "DELETE FROM dbo.System_UserAndRule WHERE Id=@Id";
            return DbHelper.ExecuteSql(sql, new { Id = id });
        }

        /// <summary>
        ///  新增用户权限
        /// </summary>
        /// <param name="rules">rules</param>
        /// Author  : Napoleon
        /// Created : 2015-01-23 10:04:00
        public int InsertUserRule(SystemUserAndRule rules)
        {
            string sql = "SELECT * FROM dbo.System_UserAndRule WHERE UserId=@UserId AND RuleParentId=@RuleParentId AND ProjectId=@ProjectId";
            int count;
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId",rules.UserId),
                new SqlParameter("@RuleParentId",rules.RuleParentId),
                new SqlParameter("@ProjectId",rules.ProjectId) 
            };
            DataTable dt = DbHelper.GetDataTable(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                count = -1;
            }
            else
            {
                sql = "INSERT INTO dbo.System_UserAndRule( Id, UserId, RuleId, RuleParentId,ProjectId ) VALUES  (@Id,@UserId,@RuleId,@RuleParentId,@ProjectId)";
                count = DbHelper.ExecuteSql(sql, rules);
            }
            return count;
        }




    }
}
