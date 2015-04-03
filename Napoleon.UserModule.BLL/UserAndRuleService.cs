using System.Data;
using Napoleon.UserModule.IBLL;
using Napoleon.UserModule.IDAL;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.BLL
{
    public class UserAndRuleService : IUserAndRuleService
    {

        private IUserAndRuleDao _userAndRuleDao;

        public UserAndRuleService(IUserAndRuleDao userAndRuleDao)
        {
            _userAndRuleDao = userAndRuleDao;
        }

        /// <summary>
        ///  获取权限ID
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="projectId">系统代码</param>
        /// Author  : Napoleon
        /// Created : 2015-01-20 13:24:49
        public SystemUserAndRule GetRule(string userId, string projectId)
        {
            return _userAndRuleDao.GetRule(userId, projectId);
        }

        /// <summary>
        ///  获取用户所在角色的列表
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="rows">The start count.</param>
        /// <param name="page">The end count.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-21 15:38:44
        public DataTable GetRuleTable(string userId, int rows, int page)
        {
            return _userAndRuleDao.GetRuleTable(userId, rows * (page - 1), rows * page);
        }

        /// <summary>
        ///  用户所在角色的总数
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-21 15:41:22
        public int GetRuleCount(string userId)
        {
            return _userAndRuleDao.GetRuleCount(userId);
        }

        /// <summary>
        ///  根据ID删除用户对应的权限数据
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-21 15:56:01
        public int DeleteRuleById(string id)
        {
            return _userAndRuleDao.DeleteRuleById(id);
        }

        /// <summary>
        ///  新增用户权限
        /// </summary>
        /// <param name="rules">rules</param>
        /// Author  : Napoleon
        /// Created : 2015-01-23 10:04:00
        public int InsertUserRule(SystemUserAndRule rules)
        {
            return _userAndRuleDao.InsertUserRule(rules);
        }

    }
}
