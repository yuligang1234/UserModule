
using System.Data;
using Napoleon.UserModule.IBLL;
using Napoleon.UserModule.IDAL;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.BLL
{
    public class UserService : IUserService
    {

        private IUserDao _userDao;

        public UserService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        /// <summary>
        ///  校验登录
        /// </summary>
        /// <param name="userName">用户账号</param>
        /// <param name="passWord">用户密码</param>
        /// <param name="projectId">项目ID</param>
        /// Author  : Napoleon
        /// Created : 2015-01-05 19:49:26
        public SystemUser CheckUser(string userName, string passWord, string projectId)
        {
            return _userDao.CheckUser(userName, passWord, projectId);
        }

        /// <summary>
        ///  保存密码
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="password">password</param>
        /// <param name="newPw">newPw</param>
        /// Author  : Napoleon
        /// Created : 2015-01-07 10:44:41
        public int SaveUser(string id, string password, string newPw)
        {
            return _userDao.SaveUser(id, password, newPw);
        }

        /// <summary>
        ///  用户列表总数
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="realName">Name of the real.</param>
        /// <param name="mobilePhone">The mobile phone.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-17 14:54:04
        public int GetUserCount(string userName, string realName, string mobilePhone)
        {
            return _userDao.GetUserCount(userName, realName, mobilePhone);
        }

        /// <summary>
        ///  用户列表
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="realName">Name of the real.</param>
        /// <param name="mobilePhone">The mobile phone.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="page">The page.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-17 14:54:04
        public DataTable GetUserTable(string userName, string realName, string mobilePhone, int rows, int page)
        {
            return _userDao.GetUserTable(userName, realName, mobilePhone, rows * (page - 1), rows * page);
        }

        /// <summary>
        ///  根据ID删除用户信息
        /// </summary>
        /// <param name="id">{'1','2'}</param>
        /// Author  : Napoleon
        /// Created : 2015-01-17 15:01:07
        public int DeleteUser(string id)
        {
            return _userDao.DeleteUser(id);
        }

        /// <summary>
        ///  新增用户信息
        /// </summary>
        /// <param name="user">The user.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-19 10:43:24
        public int SaveAddUser(SystemUser user)
        {
            return _userDao.SaveAddUser(user);
        }

        /// <summary>
        ///  根据ID查询数据
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-19 15:16:10
        public SystemUser GetUserById(string id)
        {
            return _userDao.GetUserById(id);
        }

        /// <summary>
        ///  更新用户信息
        /// </summary>
        /// <param name="user">The user.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-19 16:24:22
        public int UpdateUser(SystemUser user)
        {
            return _userDao.UpdateUser(user);
        }

        /// <summary>
        ///  初始化密码
        /// </summary>
        /// <param name="passWord">The pass word.</param>
        /// <param name="ids">The ids.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-19 20:34:54
        public int UpdatePassWord(string passWord, string ids)
        {
            return _userDao.UpdatePassWord(passWord, ids);
        }

    }
}
