using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using Napoleon.PublicCommon.Cryptography;
using Napoleon.PublicCommon.Frame;
using Napoleon.UserModule.BLL;
using Napoleon.UserModule.Common;
using Napoleon.UserModule.DAL;
using Napoleon.UserModule.IBLL;
using Napoleon.UserModule.Model;
using Newtonsoft.Json;

namespace Napoleon.UserModule.Web
{
    /// <summary>
    /// UserModuleService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行
    // [System.Web.Script.Services.ScriptService]
    public class UserModuleService : WebService
    {

        IUserService _userService = new UserService(new UserDao());
        IUserAndRuleService _userAndRuleService = new UserAndRuleService(new UserAndRuleDao());
        IMenuAndRuleService _menuAndRuleService = new MenuAndRuleService(new MenuAndRuleDao());

        #region 用户校验，获取用户信息

        /// <summary>
        ///  返回xml
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="passWord">passWord</param>
        /// Author  : Napoleon
        /// Created : 2015-02-12 16:20:23
        [WebMethod]
        public SystemUser CheckUserXml(string userName, string passWord)
        {
            SystemUser user = _userService.CheckUser(userName, passWord.EncrypteRc2(PublicFields.Rc2Key));
            return user;
        }

        /// <summary>
        ///  返回json,查询不到返回null
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="passWord">passWord</param>
        /// Author  : Napoleon
        /// Created : 2015-02-12 16:20:46
        [WebMethod]
        public string CheckUserJson(string userName, string passWord)
        {
            SystemUser user = _userService.CheckUser(userName, passWord.EncrypteRc2(PublicFields.Rc2Key));
            return JsonConvert.SerializeObject(user);
        }

        /// <summary>
        ///  返回ajax使用的json,查询不到返回null
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="passWord">passWord</param>
        /// Author  : Napoleon
        /// Created : 2015-02-12 16:21:12
        [WebMethod]
        public void CheckUserAjaxJson(string userName, string passWord)
        {
            SystemUser user = _userService.CheckUser(userName, passWord.EncrypteRc2(PublicFields.Rc2Key));
            Context.Response.Write(JsonConvert.SerializeObject(user));
            Context.Response.End();
        }

        #endregion

        #region 修改密码,-1表示老密码错误，0表示修改密码失败，1表示修改密码成功

        [WebMethod]
        public int ChangePwXml(string id, string password, string newPw)
        {
            return _userService.SaveUser(id, password.EncrypteRc2(PublicFields.Rc2Key), newPw.EncrypteRc2(PublicFields.Rc2Key));
        }

        [WebMethod]
        public void ChangePwAjaxJson(string id, string password, string newPw)
        {
            int i = _userService.SaveUser(id, password.EncrypteRc2(PublicFields.Rc2Key), newPw.EncrypteRc2(PublicFields.Rc2Key));
            Context.Response.Write(i);
            Context.Response.End();
        }

        #endregion

        #region 初始化密码(可以操作多个)，0表示失败，>0表示成功

        [WebMethod]
        public int UpdatePwXml(string passWord, string ids)
        {
            return _userService.UpdatePassWord(passWord.EncrypteRc2(PublicFields.Rc2Key), ids);
        }

        [WebMethod]
        public void UpdatePwAjaxJson(string passWord, string ids)
        {
            int i = _userService.UpdatePassWord(passWord.EncrypteRc2(PublicFields.Rc2Key), ids);
            Context.Response.Write(i);
            Context.Response.End();
        }

        #endregion

        #region 根据系统代码和用户ID,获取用户权限对应表

        [WebMethod]
        public SystemUserAndRule GetRuleXml(string userId, string projectId)
        {
            SystemUserAndRule rule = _userAndRuleService.GetRule(userId, projectId);
            return rule;
        }

        [WebMethod]
        public string GetRuleJson(string userId, string projectId)
        {
            SystemUserAndRule rule = _userAndRuleService.GetRule(userId, projectId);
            return JsonConvert.SerializeObject(rule);
        }

        [WebMethod]
        public void GetRuleAjaxJson(string userId, string projectId)
        {
            SystemUserAndRule rule = _userAndRuleService.GetRule(userId, projectId);
            Context.Response.Write(JsonConvert.SerializeObject(rule));
            Context.Response.End();
        }

        #endregion

        #region 根据系统代码和权限ID获取左菜单数据

        [WebMethod]
        public DataTable GetMenuXml(string ruleId, string projectId)
        {
            DataTable dt = _menuAndRuleService.GetMenuTable(ruleId, projectId);
            return dt;
        }

        [WebMethod]
        public string GetMenuJson(string ruleId, string projectId)
        {
            DataTable dt = _menuAndRuleService.GetMenuTable(ruleId, projectId);
            string json = dt.ConvertToTreeJson(false);
            return json;
        }

        [WebMethod]
        public void GetMenuAjaxJson(string ruleId, string projectId)
        {
            DataTable dt = _menuAndRuleService.GetMenuTable(ruleId, projectId);
            string json = dt.ConvertToTreeJson(false);
            Context.Response.Write(json);
            Context.Response.End();
        }

        #endregion

        #region 根据系统代码、权限ID、菜单ID查询菜单的具体操作

        [WebMethod]
        public List<SystemMenu> GetOperationXml(string ruleId, string menuId, string projectId)
        {
            List<SystemMenu> menus = _menuAndRuleService.GetOperation(ruleId, menuId, projectId);
            return menus;
        }

        [WebMethod]
        public string GetOperationJson(string ruleId, string menuId, string projectId)
        {
            List<SystemMenu> menus = _menuAndRuleService.GetOperation(ruleId, menuId, projectId);
            string json = JsonConvert.SerializeObject(menus);
            return json;
        }

        [WebMethod]
        public void GetOperationAjaxJson(string ruleId, string menuId, string projectId)
        {
            List<SystemMenu> menus = _menuAndRuleService.GetOperation(ruleId, menuId, projectId);
            string json = JsonConvert.SerializeObject(menus);
            Context.Response.Write(json);
            Context.Response.End();
        }

        #endregion


    }
}
