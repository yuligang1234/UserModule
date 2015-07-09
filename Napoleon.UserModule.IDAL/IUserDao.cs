
using System.Data;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.IDAL
{
    public interface IUserDao
    {

        /// <summary>
        ///  校验登录
        /// </summary>
        /// <param name="userName">用户账号</param>
        /// <param name="passWord">用户密码</param>
        /// <param name="projectId">项目ID</param>
        /// Author  : Napoleon
        /// Created : 2015-01-05 19:49:26
        SystemUser CheckUser(string userName, string passWord, string projectId);

        /// <summary>
        ///  保存密码
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="password">password</param>
        /// <param name="newPw">newPw</param>
        /// Author  : Napoleon
        /// Created : 2015-01-07 10:44:41
        int SaveUser(string id, string password, string newPw);

        /// <summary>
        ///  用户列表总数
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="realName">Name of the real.</param>
        /// <param name="mobilePhone">The mobile phone.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-17 14:54:04
        int GetUserCount(string userName, string realName, string mobilePhone);

        /// <summary>
        ///  用户列表
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="realName">Name of the real.</param>
        /// <param name="mobilePhone">The mobile phone.</param>
        /// <param name="strarCount">The rows.</param>
        /// <param name="endCount">The page.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-17 14:54:04
        DataTable GetUserTable(string userName, string realName, string mobilePhone, int strarCount, int endCount);

        /// <summary>
        ///  根据ID删除用户信息
        /// </summary>
        /// <param name="id">{'1','2'}</param>
        /// Author  : Napoleon
        /// Created : 2015-01-17 15:01:07
        int DeleteUser(string id);

        /// <summary>
        ///  新增用户信息
        /// </summary>
        /// <param name="user">The user.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-19 10:43:24
        int SaveAddUser(SystemUser user);

        /// <summary>
        ///  根据ID查询数据
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-19 15:16:10
        SystemUser GetUserById(string id);

        /// <summary>
        ///  更新用户信息
        /// </summary>
        /// <param name="user">The user.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-19 16:24:22
        int UpdateUser(SystemUser user);

        /// <summary>
        ///  初始化密码
        /// </summary>
        /// <param name="passWord">The pass word.</param>
        /// <param name="ids">The ids.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-19 20:34:54
        int UpdatePassWord(string passWord, string ids);


    }
}
