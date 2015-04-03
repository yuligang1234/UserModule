
using System.Data;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.IBLL
{
    public interface IRuleService
    {

        /// <summary>
        ///  获取角色权限
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-21 21:24:14
        DataTable GetRuleTable(string projectId, string id);

        /// <summary>
        ///  获取角色列表
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 14:33:15
        DataTable GetRuleTable(string projectId);

        /// <summary>
        ///  获取父节点下拉框
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="parentId">The parent identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 14:41:25
        DataTable GetRuleCombobox(string projectId, string parentId = "");

        /// <summary>
        ///  新增角色
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 15:29:05
        int InsertRule(SystemRule rule);

        /// <summary>
        ///  根据ID查询
        /// </summary>
        /// <param name="id">ID</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 16:24:30
        SystemRule GetRule(string id);

        /// <summary>
        ///  更新
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 16:28:34
        int UpdateRule(SystemRule rule);

        /// <summary>
        ///  根据ID删除
        /// </summary>
        /// <param name="id">id</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 10:43:31
        int DeleteRule(string id);

    }
}
