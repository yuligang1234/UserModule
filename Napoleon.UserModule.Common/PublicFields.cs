
using System;

namespace Napoleon.UserModule.Common
{
    public class PublicFields
    {

        /// <summary>
        ///  用户信息Cookie
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-10 11:20:50
        public static string UserCookie = "User";

        /// <summary>
        ///  Rc2密钥
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-10 16:20:38
        public static string Rc2Key = "Napoleon";

        /// <summary>
        ///  日志导出Excel文件名
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-15 14:41:10
        public static string LogExcelName = DateTime.Now.ToString("yyyyMMddhhmmssffff") + ".xls";

        /// <summary>
        ///  是否启用
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-17 13:38:03
        public static string IsUse = "d33f281234201307311605187265211";

        /// <summary>
        ///  启用
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-20 14:48:53
        public static string IsDefaultUse = "d33f281234201307311605187265212";

        /// <summary>
        ///  默认密码
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-19 10:39:11
        public static string DefaultPw = "123456";

        /// <summary>
        ///  当前系统的父节点
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-19 10:39:25
        //public static string RuleParentId = "d33f281234201307311605187265221";

        /// <summary>
        ///  用户权限ID
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-20 14:49:09
        public static string RuleIdCookies = "RuleId";

        /// <summary>
        ///  用来区分菜单属于哪个系统
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-23 15:05:04
        public static string ProjectId = "YHQXXT";

        /// <summary>
        ///  菜单父节点
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-24 16:33:20
        public static string MenuParentId = "0";

        /// <summary>
        ///  权限父节点
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-27 15:32:39
        public static string RuleParentId = "0";

    }
}
