using System;
using System.Data;
using System.Data.SqlClient;
using Napoleon.Db;
using Napoleon.UserModule.IDAL;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.DAL
{
    public class UserDao : IUserDao
    {

        /// <summary>
        ///  校验登录
        /// </summary>
        /// <param name="userName">用户账号</param>
        /// <param name="passWord">用户密码</param>
        /// Author  : Napoleon
        /// Created : 2015-01-05 19:49:26
        public SystemUser CheckUser(string userName, string passWord)
        {
            string sql = "select Id,UserName,PassWords,RealName,MobilePhone,IsUse,UserAddress,Sort,Remark,Operator FROM [System_User] AS su where UserName=@UserName and PassWords=@PassWords";
            SystemUser user = DbHelper.GetEnumerable<SystemUser>(sql, new { UserName = userName, PassWords = passWord });
            return user;
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
            string sql = "select count(*) from dbo.[System_User] where Id=@Id and PassWords=@PassWords";
            int count = DbHelper.QueryCount(sql, new { Id = id, PassWords = password });
            if (count > 0)
            {
                sql = "UPDATE dbo.[System_User] SET PassWords=@PassWords where Id=@Id";
                return DbHelper.ExecuteSql(sql, new { PassWords = newPw, Id = id });
            }
            return -1;
        }

        /// <summary>
        /// 用户列表总数
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="realName">Name of the real.</param>
        /// <param name="mobilePhone">The mobile phone.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-17 14:54:04
        public int GetUserCount(string userName, string realName, string mobilePhone)
        {
            string sql = "select count(*) FROM dbo.[System_User] AS su LEFT JOIN dbo.System_Code AS sc ON sc.Id=su.IsUse WHERE UserName LIKE @UserName AND RealName LIKE @RealName AND MobilePhone LIKE @MobilePhone";
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName",string.Format("%{0}%",userName)),
                new SqlParameter("@RealName",string.Format("%{0}%",realName)),
                new SqlParameter("@MobilePhone",string.Format("%{0}%",mobilePhone)) 
            };
            DataTable dt = DbHelper.GetDataTable(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }

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
        public DataTable GetUserTable(string userName, string realName, string mobilePhone, int strarCount, int endCount)
        {
            string sql = "SELECT * FROM (select ROW_NUMBER() OVER(ORDER BY su.Id) AS number,su.Id,su.UserName,su.RealName,su.MobilePhone,sc.Name AS IsUse,su.UserAddress,su.Sort,su.Remark,su.Operator FROM dbo.[System_User] AS su LEFT JOIN dbo.System_Code AS sc ON sc.Id=su.IsUse WHERE UserName LIKE @UserName AND RealName LIKE @RealName AND MobilePhone LIKE @MobilePhone) AS news WHERE news.number>@StrarCount AND news.number<=@EndCount";
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName",string.Format("%{0}%",userName)),
                new SqlParameter("@RealName",string.Format("%{0}%",realName)),
                new SqlParameter("@MobilePhone",string.Format("%{0}%",mobilePhone)) ,
                new SqlParameter("@StrarCount",strarCount),
                new SqlParameter("@EndCount",endCount) 
            };
            return DbHelper.GetDataTable(sql, parameters);
        }

        /// <summary>
        ///  根据ID删除用户信息
        /// </summary>
        /// <param name="id">{'1','2'}</param>
        /// Author  : Napoleon
        /// Created : 2015-01-17 15:01:07
        public int DeleteUser(string id)
        {
            string sql = string.Format("SELECT * FROM dbo.System_UserAndRule WHERE UserId IN ({0})", id);
            DataTable dt = DbHelper.GetDataTable(sql);
            int count;
            if (dt.Rows.Count > 0)
            {
                count = -1;
            }
            else
            {
                sql = string.Format("delete from dbo.[System_User] where Id in ({0})", id);
                count = DbHelper.ExecuteSql(sql, null);
            }
            return count;
        }

        /// <summary>
        ///  新增用户信息
        /// </summary>
        /// <param name="user">The user.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-19 10:43:24
        public int SaveAddUser(SystemUser user)
        {
            string sql = "select Id from System_User where UserName=@UserName";
            int count = DbHelper.QueryCount(sql, user);
            if (count > 0)
            {
                count = -1;
            }
            else
            {
                sql = "Insert into dbo.[System_User](Id,UserName ,PassWords ,RealName ,MobilePhone ,IsUse ,UserAddress ,Sort ,Remark ,Operator) values(@Id,@UserName,@PassWords,@RealName,@MobilePhone,@IsUse,@UserAddress,@Sort,@Remark,@Operator)";
                count = DbHelper.ExecuteSql(sql, user);
            }
            return count;
        }

        /// <summary>
        ///  根据ID查询数据
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-19 15:16:10
        public SystemUser GetUserById(string id)
        {
            string sql = "SELECT Id,UserName,PassWords,RealName,MobilePhone,IsUse,UserAddress,Sort,Remark,Operator FROM dbo.[System_User] Where Id=@Id";
            return DbHelper.GetEnumerable<SystemUser>(sql, new { Id = id });
        }

        /// <summary>
        ///  更新用户信息
        /// </summary>
        /// <param name="user">The user.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-19 16:24:22
        public int UpdateUser(SystemUser user)
        {
            string sql = "SELECT Id FROM dbo.[System_User] WHERE UserName=@UserName";
            SqlParameter[] parameters = { new SqlParameter("@UserName", user.UserName) };
            DataTable dt = DbHelper.GetDataTable(sql, parameters);
            if (dt.Rows.Count > 0 && !dt.Rows[0][0].ToString().Equals(user.Id))
            {
                return -1;//账号已经存在
            }
            sql = "UPDATE dbo.[System_User] SET UserName=@UserName,RealName=@RealName,MobilePhone=@MobilePhone,IsUse=@IsUse,UserAddress=@UserAddress,Sort=@Sort,Remark=@Remark,Operator=@Operator where Id=@Id";
            return DbHelper.ExecuteSql(sql, user);
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
            string sql = string.Format("UPDATE dbo.[System_User] SET PassWords='{0}' WHERE Id IN ({1})", passWord, ids);
            return DbHelper.ExecuteSql(sql, null);
        }




    }
}
