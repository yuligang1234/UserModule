
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Napoleon.Db;
using Napoleon.UserModule.Common;
using Napoleon.UserModule.IDAL;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.DAL
{
    public class MenuAndRuleDao : IMenuAndRuleDao
    {

        /// <summary>
        ///  获取菜单
        /// </summary>
        /// <param name="ruleId">角色ID</param>
        /// <param name="projectId">系统ID</param>
        /// Author  : Napoleon
        /// Created : 2015-01-10 15:15:22
        public DataTable GetMenuTable(string ruleId, string projectId)
        {
            string sql = "SELECT DISTINCT mu.Id,mu.Name,mu.ParentId,mu.Url,mu.Icon FROM dbo.System_MenuAndRule AS md LEFT JOIN dbo.System_Menu AS mu ON md.MenuId=mu.Id  WHERE md.RuleId=@RuleId and mu.ProjectId=@ProjectId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@RuleId",ruleId), 
                new SqlParameter("@ProjectId",projectId) 
            };
            return DbHelper.GetDataTable(sql, parameters);
        }

        /// <summary>
        ///  获取
        /// </summary>
        /// <param name="ruleId">角色Id</param>
        /// <param name="menuId">menuId</param>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-12 20:03:14
        public List<SystemMenu> GetOperation(string ruleId, string menuId, string projectId)
        {
            string sql = "SELECT * FROM dbo.System_Menu WHERE id IN (SELECT OperationId FROM dbo.System_MenuAndRule WHERE RuleId=@RuleId AND MenuId=@MenuId AND ProjectId=@ProjectId) AND ParentId != @ParentId ORDER BY Sort";
            return DbHelper.GetEnumerables<SystemMenu>(sql, new { RuleId = ruleId, MenuId = menuId, ProjectId = projectId, ParentId = PublicFields.RuleParentId });
        }

        /// <summary>
        ///  获取拥有的权限
        /// </summary>
        /// <param name="ruleId">权限ID</param>
        /// <param name="projectId">系统ID</param>
        /// Author  : Napoleon
        /// Created : 2015-01-10 15:15:22
        public DataTable GetOperatorTable(string projectId, string ruleId)
        {
            string sql = "SELECT OperationId FROM dbo.System_MenuAndRule WHERE ProjectId=@ProjectId and RuleId=@RuleId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@ProjectId",projectId),
                new SqlParameter("@RuleId",ruleId) 
            };
            return DbHelper.GetDataTable(sql, parameters);
        }

        /// <summary>
        ///  需要先删除原先的权限，再新增权限
        /// </summary>
        /// <param name="ruleId">The rule identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="list">The list.</param>
        /// Author  : Napoleon
        /// Created : 2015-02-04 20:22:08
        public int UpdateRuleMenu(string ruleId, string projectId, List<SystemMenuAndRule> list)
        {
            using (IDbConnection conn = DbHelper.OpenConnection())
            {
                int count;
                using (IDbTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = string.Format("DELETE dbo.System_MenuAndRule WHERE RuleId=@RuleId AND ProjectId=@ProjectId");
                        conn.Execute(sql, new { RuleId = ruleId, ProjectId = projectId }, transaction, 30, CommandType.Text);
                        sql = string.Format("INSERT dbo.System_MenuAndRule( RuleId ,MenuId ,OperationId ,ProjectId) VALUES(@RuleId ,@MenuId ,@OperationId ,@ProjectId)");
                        count = conn.Execute(sql, list, transaction, 30, CommandType.Text);
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

    }
}
