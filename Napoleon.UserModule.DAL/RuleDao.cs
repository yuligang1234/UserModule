using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Napoleon.Db;
using Napoleon.UserModule.IDAL;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.DAL
{
    public class RuleDao : IRuleDao
    {

        /// <summary>
        ///  获取角色权限
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-21 21:24:14
        public DataTable GetRuleTable(string projectId, string id)
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT Id,Name FROM dbo.System_Rule WHERE ParentId=@ParentId");
                List<SqlParameter> list = new List<SqlParameter>();
                list.Add(new SqlParameter("@ParentId", id));
                if (!string.IsNullOrWhiteSpace(projectId))
                {
                    sb.AppendFormat(" AND ProjectId=@ProjectId");
                    list.Add(new SqlParameter("@ProjectId", projectId));
                }
                dt = DbHelper.GetDataTable(sb.ToString(), list.ToArray());
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return dt;
        }

        /// <summary>
        ///  获取角色列表
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 14:33:15
        public DataTable GetRuleTable(string projectId)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = string.Format("SELECT ProjectId,ParentId,Id,Name,Person,TelPhone,Address,Sort,Remark FROM dbo.System_Rule WHERE ProjectId=@ProjectId ORDER BY Sort");
                SqlParameter[] parameters = { new SqlParameter("@ProjectId", projectId) };
                dt = DbHelper.GetDataTable(sql, parameters);
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return dt;
        }

        /// <summary>
        ///  获取父节点下拉框
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// <param name="parentId">parentId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 14:41:25
        public DataTable GetRuleCombobox(string projectId, string parentId = "")
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> list = new List<SqlParameter>();
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT Id,Name FROM dbo.System_Rule WHERE ProjectId=@ProjectId ");
                list.Add(new SqlParameter("@ProjectId", projectId));
                if (!string.IsNullOrWhiteSpace(parentId))
                {
                    sb.AppendFormat("AND ParentId=@ParentId ");
                    list.Add(new SqlParameter("@ParentId", parentId));
                }
                sb.Append("ORDER BY Sort");
                dt = DbHelper.GetDataTable(sb.ToString(), list.ToArray());
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return dt;
        }

        /// <summary>
        ///  新增角色
        /// </summary>
        /// <param name="rule">rule</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 15:29:05
        public int InsertRule(SystemRule rule)
        {
            int count;
            try
            {
                string sql = "SELECT Id FROM dbo.System_Rule WHERE ProjectId=@ProjectId AND Name=@Name";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ProjectId",rule.ProjectId), 
                    new SqlParameter("@Name", rule.Name)
                };
                DataTable dt = DbHelper.GetDataTable(sql, parameters);
                if (dt.Rows.Count > 0)
                {
                    count = -1;
                }
                else
                {
                    sql = "INSERT INTO dbo.System_Rule (ProjectId,Id,Name,ParentId,Sort,Remark,Operator) VALUES (@ProjectId,@Id,@Name,@ParentId,@Sort,@Remark,@Operator)";
                    count = DbHelper.ExecuteSql(sql, rule);
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
        /// <param name="id">id</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 16:24:30
        public SystemRule GetRule(string id)
        {
            SystemRule rule = new SystemRule();
            try
            {
                string sql = "SELECT  ProjectId ,Id ,Name ,ParentId,Person,Address,TelPhone ,Sort ,Remark ,Operator FROM dbo.System_Rule WHERE Id=@Id";
                rule = DbHelper.GetEnumerable<SystemRule>(sql, new { @Id = id });
            }
            catch (Exception exception)
            {
                Log4Dao.InsertLog4(exception.Message);
            }
            return rule;
        }

        /// <summary>
        ///  更新
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 16:28:34
        public int UpdateRule(SystemRule rule)
        {
            int count;
            try
            {
                string sql = "SELECT Id FROM dbo.System_Rule WHERE ProjectId=@ProjectId AND Name=@Name";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@ProjectId",rule.ProjectId),
                    new SqlParameter("@Name",rule.Name) 
                };
                DataTable dt = DbHelper.GetDataTable(sql, parameters);
                if (dt.Rows.Count == 0 || dt.Rows[0][0].ToString().Equals(rule.Id))
                {
                    sql = "UPDATE dbo.System_Rule SET Name=@Name,ParentId=@ParentId,Sort=@Sort,Remark=@Remark,Operator=@Operator WHERE Id=@Id";
                    count = DbHelper.ExecuteSql(sql, rule);
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

        /// <summary>
        ///  根据ID删除
        /// </summary>
        /// <param name="id">id</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 10:43:31
        public int DeleteRule(string id)
        {
            int count;
            try
            {
                string sql = "SELECT * FROM dbo.System_Rule WHERE ParentId=@ParentId";
                SqlParameter[] parameters = { new SqlParameter("@ParentId", id) };
                DataTable dt = DbHelper.GetDataTable(sql, parameters);
                if (dt.Rows.Count > 0)
                {
                    count = -1;
                }
                else
                {
                    sql = "DELETE FROM dbo.System_Rule WHERE Id=@Id";
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
