using System.Data;
using Napoleon.UserModule.IBLL;
using Napoleon.UserModule.IDAL;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.BLL
{
    public class RuleService : IRuleService
    {

        private IRuleDao _ruleDao;

        public RuleService(IRuleDao ruleDao)
        {
            _ruleDao = ruleDao;
        }

        /// <summary>
        ///  获取角色权限
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-21 21:24:14
        public DataTable GetRuleTable(string projectId, string id)
        {
            return _ruleDao.GetRuleTable(projectId, id);
        }

        /// <summary>
        ///  获取角色列表
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 14:33:15
        public DataTable GetRuleTable(string projectId)
        {
            return _ruleDao.GetRuleTable(projectId);
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
            return _ruleDao.GetRuleCombobox(projectId, parentId);
        }

        /// <summary>
        ///  新增角色
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 15:29:05
        public int InsertRule(SystemRule rule)
        {
            return _ruleDao.InsertRule(rule);
        }

        /// <summary>
        ///  根据ID查询
        /// </summary>
        /// <param name="id">ID</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 16:24:30
        public SystemRule GetRule(string id)
        {
            return _ruleDao.GetRule(id);
        }

        /// <summary>
        ///  更新
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-27 16:28:34
        public int UpdateRule(SystemRule rule)
        {
            return _ruleDao.UpdateRule(rule);
        }

        /// <summary>
        ///  根据ID删除
        /// </summary>
        /// <param name="id">id</param>
        /// Author  : Napoleon
        /// Created : 2015-01-24 10:43:31
        public int DeleteRule(string id)
        {
            return _ruleDao.DeleteRule(id);
        }

    }
}
