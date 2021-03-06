﻿
using System.Data;
using System.Web.Mvc;
using Napoleon.PublicCommon.Base;
using Napoleon.PublicCommon.Cryptography;
using Napoleon.PublicCommon.Frame;
using Napoleon.PublicCommon.Http;
using Napoleon.UserModule.Common;
using Napoleon.UserModule.IBLL;
using Napoleon.UserModule.Model;

namespace Napoleon.UserModule.Web.Controllers
{
    public class UserController : BaseController
    {

        private IUserService _userService;
        private ICodeService _codeService;
        private IUserAndRuleService _userAndRuleService;
        private IRuleService _ruleService;
        private IProjectService _projectService;


        public UserController(IUserService userService, ICodeService codeService, IUserAndRuleService userAndRuleService, IRuleService ruleService, IProjectService projectService)
        {
            _userService = userService;
            _codeService = codeService;
            _userAndRuleService = userAndRuleService;
            _ruleService = ruleService;
            _projectService = projectService;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  保存密码
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="password">password</param>
        /// <param name="newPw">newPw</param>
        /// Author  : Napoleon
        /// Created : 2015-01-17 13:30:42
        public ActionResult SaveUser(string id, string password, string newPw)
        {
            int count = _userService.SaveUser(id, password, newPw);
            string status = "failue", msg, json;
            switch (count)
            {
                case -1:
                    msg = "修改失败，请输入正确的原密码！";
                    break;
                case 1:
                    status = "success";
                    msg = "修改成功,请重新登陆系统";
                    break;
                default:
                    msg = "修改失败！";
                    break;
            }
            json = PublicFunc.ModelToJson(status, msg);
            return Content(json);
        }

        /// <summary>
        ///  获取用户列表
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="realName">Name of the real.</param>
        /// <param name="mobilePhone">The mobile phone.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="page">The page.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-17 15:04:12
        public ActionResult LoadDataGrid(string userName, string realName, string mobilePhone, int rows, int page)
        {
            DataTable dt = _userService.GetUserTable(userName, realName, mobilePhone, rows, page);
            int count = _userService.GetUserCount(userName, realName, mobilePhone);
            string json = dt.ConvertTableToGridJson(count);
            return Content(json);
        }

        /// <summary>
        ///  删除用户
        /// </summary>
        /// <param name="id">id</param>
        /// Author  : Napoleon
        /// Created : 2015-01-17 15:52:54
        public ActionResult DeleteUser(string id)
        {
            int count = _userService.DeleteUser(id);
            string status = "failue", msg, json;
            if (count > 0)
            {
                status = "success";
                msg = "删除成功！";
            }
            else
            {
                msg = count == -1 ? "删除失败，请先删除用户对应的权限！" : "删除失败！";
            }
            json = PublicFunc.ModelToJson(status, msg);
            return Content(json);
        }

        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        ///  是否启用
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-19 09:32:02
        public ActionResult LoadIsUse()
        {
            DataTable dt = _codeService.GetTableByParentId(PublicFields.IsUse);
            string json = dt.ConvertToComboboxJson("Id", "Name");
            return Content(json);
        }

        /// <summary>
        ///  项目列表
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// Author  : Napoleon
        /// Created : 2015-06-06 09:22:50
        public ActionResult LoadProject()
        {
            DataTable dt = _projectService.GetProjecTable();
            string json = dt.ConvertToComboboxJson("ProjectId", "ProjectName");
            return Content(json);
        }

        /// <summary>
        ///  保存新增的用户信息
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-19 10:46:59
        public ActionResult SaveAdd(string projectId, string userName, string realName, string mobilePhone, string isUse, string userAddress, decimal sort, string remark)
        {
            SystemUser user = new SystemUser();
            user.Id = CustomId.GetCustomId();
            user.UserName = userName;
            user.PassWords = PublicFields.DefaultPw.EncrypteRc2(PublicFields.Rc2Key);
            user.RealName = realName;
            user.MobilePhone = mobilePhone;
            user.IsUse = isUse;
            user.UserAddress = userAddress;
            user.Sort = sort;
            user.Remark = remark;
            user.Operator = PublicFields.UserCookie.ReadCookie<SystemUser>().UserName;
            user.ProjectId = projectId;
            int count = _userService.SaveAddUser(user);
            string status = "failue", msg, json;
            switch (count)
            {
                case -1:
                    msg = "保存失败，该账号已存在！";
                    break;
                case 1:
                    status = "success";
                    msg = "保存成功！";
                    break;
                default:
                    msg = "保存失败！";
                    break;
            }
            json = PublicFunc.ModelToJson(status, msg);
            return Content(json);
        }

        public ActionResult ViewInfo(string id)
        {
            ViewData["User"] = _userService.GetUserById(id);
            return View();
        }

        public ActionResult Edit(string id)
        {
            ViewData["User"] = _userService.GetUserById(id);
            return View();
        }

        /// <summary>
        ///  更新用户信息
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-19 15:53:14
        public ActionResult UpdateUser(string id, string projectId, string userName, string realName, string mobilePhone, string isUse, string userAddress, decimal sort, string remark)
        {
            SystemUser user = new SystemUser();
            user.Id = id;
            user.UserName = userName;
            user.RealName = realName;
            user.MobilePhone = mobilePhone;
            user.IsUse = isUse;
            user.UserAddress = userAddress;
            user.Sort = sort;
            user.Remark = remark;
            user.Operator = PublicFields.UserCookie.ReadCookie<SystemUser>().UserName;
            user.ProjectId = projectId;
            int count = _userService.UpdateUser(user);
            string status = "failue", msg, json;
            switch (count)
            {
                case -1:
                    msg = "更新失败，该账号已存在！";
                    break;
                case 1:
                    status = "success";
                    msg = "更新成功！";
                    break;
                default:
                    msg = "更新失败！";
                    break;
            }
            json = PublicFunc.ModelToJson(status, msg);
            return Content(json);
        }

        /// <summary>
        ///  初始化密码
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-19 20:39:21
        public ActionResult UpdatePassWord(string ids)
        {
            int count = _userService.UpdatePassWord(PublicFields.DefaultPw.GetMd5(), ids);
            string status = "failue", msg = "初始化失败", json;
            if (count > 0)
            {
                status = "success";
                msg = "初始化成功！";
            }
            json = PublicFunc.ModelToJson(status, msg);
            return Content(json);
        }

        public ActionResult Permit(string userId)
        {
            ViewData["UserId"] = userId;
            return View();
        }

        /// <summary>
        ///  获取用户拥有的权限列表
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="rows">The rows.</param>
        /// <param name="page">The page.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-21 10:18:54
        public ActionResult LoadPermitGrid(string userId, int rows, int page)
        {
            DataTable dt = _userAndRuleService.GetRuleTable(userId, rows, page);
            int count = _userAndRuleService.GetRuleCount(userId);
            string json = dt.ConvertTableToGridJson(count);
            return Content(json);
        }

        /// <summary>
        ///  根据ID删除用户权限数据
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-21 15:58:33
        public ActionResult DeletePermit(string id)
        {
            int count = _userAndRuleService.DeleteRuleById(id);
            string status = "failue", msg, json;
            switch (count)
            {
                case 1:
                    status = "success";
                    msg = "删除成功！";
                    break;
                default:
                    msg = "删除失败！";
                    break;
            }
            json = PublicFunc.ModelToJson(status, msg);
            return Content(json);
        }

        public ActionResult PermitAdd(string userId)
        {
            ViewData["UserId"] = userId;
            return View();
        }

        /// <summary>
        ///  权限系统下拉框
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-27 11:11:56
        public ActionResult PermitProject()
        {
            DataTable dt = _projectService.GetProjecTable();
            string json = dt.ConvertToComboboxJson("ProjectId", "ProjectName");
            return Content(json);
        }

        /// <summary>
        ///  权限下拉框
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// <param name="id">id</param>
        /// Author  : Napoleon
        /// Created : 2015-01-21 21:36:18
        public ActionResult PermitCompany(string projectId, string id)
        {
            DataTable dt = _ruleService.GetRuleTable(projectId, id);
            string json = dt.ConvertToComboboxJson("Id", "Name");
            return Content(json);
        }

        /// <summary>
        ///  新增用户权限
        /// </summary>
        /// <param name="projectId">projectId</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="company">The company.</param>
        /// <param name="rule">The rule.</param>
        /// Author  : Napoleon
        /// Created : 2015-01-23 09:48:45
        public ActionResult SavePermitAdd(string projectId, string userId, string company, string rule)
        {
            SystemUserAndRule rules = new SystemUserAndRule();
            rules.ProjectId = projectId;
            rules.Id = CustomId.GetCustomId();
            rules.UserId = userId;
            rules.RuleParentId = company;
            rules.RuleId = rule;
            int count = _userAndRuleService.InsertUserRule(rules);
            string status = "failue", msg, json;
            switch (count)
            {
                case -1:
                    msg = "添加失败,该用户已经拥有类似权限，不能重复添加！";
                    break;
                case 1:
                    status = "success";
                    msg = "添加成功！";
                    break;
                default:
                    msg = "添加失败！";
                    break;
            }
            json = PublicFunc.ModelToJson(status, msg);
            return Content(json);
        }

    }
}
