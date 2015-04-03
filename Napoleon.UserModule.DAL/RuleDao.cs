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
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT Id,Name FROM dbo.System_Rule WHERE ParentId=@ParentId");
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@ParentId", id));
            if (!string.IsNullOrWhiteSpace(projectId))
            {
                sb.AppendFormat(" AND ProjectId=@ProjectId");
                list.Add(new SqlParameter("@ProjectId", projectId));
            }
            return DbHelper.GetDataTable(sb.ToString(), list.ToArray());
        }

        /// <summary>
        ///  获取角色列表
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 14:33:15
        public DataTable GetRuleTable(string projectId)
        {
            string sql = "SELECT ProjectId,ParentId,Id,Name,Person,TelPhone,Address,Sort,Remark FROM dbo.System_Rule WHERE ProjectId=@ProjectId ORDER BY Sort";
            SqlParameter[] parameters = { new SqlParameter("@ProjectId", projectId) };
            return DbHelper.GetDataTable(sql, parameters);
        }

        /// <summary>
        ///  获取父节点下拉框
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="parentId">The parent identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 14:41:25
        public DataTable GetRuleCombobox(string projectId, string parentId = "")
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
            return DbHelper.GetDataTable(sb.ToString(), list.ToArray());
        }

        /// <summary>
        ///  新增角色
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 15:29:05
        public int InsertRule(SystemRule rule)
        {
            string sql = "SELECT Id FROM dbo.System_Rule WHERE ProjectId=@ProjectId AND Name=@Name";
            SqlParameter[] parameters = { new SqlParameter("@Name", rule.Name) };
            DataTable dt = DbHelper.GetDataTable(sql, parameters);
            int count;
            if (dt.Rows.Count > 0)
            {
                count = -1;
            }
            else
            {
                sql = "INSERT INTO dbo.System_Rule (ProjectId,Id,Name,ParentId,Sort,Remark,Operator) VALUES (@ProjectId,@Id,@Name,@ParentId,@Sort,@Remark,@Operator)";
                count = DbHelper.ExecuteSql(sql, rule);
            }
            return count;
        }

        /// <summary>
        ///  根据ID查询
        /// </summary>
        /// <param name="id">ID</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 16:24:30
        public SystemRule GetRule(string id)
        {
            string sql = "SELECT  ProjectId ,Id ,Name ,ParentId,Person,Address,TelPhone ,Sort ,Remark ,Operator FROM dbo.System_Rule WHERE Id=@Id";
            return DbHelper.GetEnumerable<SystemRule>(sql, new { Id = id });
        }

        /// <summary>
        ///  更新
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 16:28:34
        public int UpdateRule(SystemRule rule)
        {
            string sql = "SELECT Id FROM dbo.System_Rule WHERE ProjectId=@ProjectId AND Name=@Name";
            SqlParameter[] parameters =
            {
                new SqlParameter("@ProjectId",rule.ProjectId),
                new SqlParameter("@Name",rule.Name) 
            };
            DataTable dt = DbHelper.GetDataTable(sql, parameters);
            int count;
            if (dt.Rows.Count == 0 || dt.Rows[0][0].ToString().Equals(rule.Id))
            {
                sql = "UPDATE dbo.System_Rule SET Name=@Name,ParentId=@ParentId,Sort=@Sort,Remark=@Remark,Operator=@Operator WHERE Id=@Id";
                count = DbHelper.ExecuteSql(sql, rule);
            }
            else
            {
                count = -1;
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
            string sql = "SELECT * FROM dbo.System_Rule WHERE ParentId=@ParentId";
            SqlParameter[] parameters = { new SqlParameter("@ParentId", id) };
            DataTable dt = DbHelper.GetDataTable(sql, parameters);
            int count;
            if (dt.Rows.Count > 0)
            {
                count = -1;
            }
            else
            {
                sql = "DELETE FROM dbo.System_Rule WHERE Id=@Id";
                count = DbHelper.ExecuteSql(sql, new { Id = id });
            }
            return count;
        }



    }
}
