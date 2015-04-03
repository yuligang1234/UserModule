using System.Web.Mvc;
using Napoleon.UserModule.Common;
using Napoleon.UserModule.IBLL;
using Napoleon.UserModule.Model;
using Napoleon.PublicCommon;

namespace Napoleon.UserModule.Web.Controllers
{
    public class LoginController : Controller
    {

        private IUserService _userService;
        private IUserAndRuleService _userAndRuleService;

        public LoginController(IUserService userService, IUserAndRuleService userAndRuleService)
        {
            _userService = userService;
            _userAndRuleService = userAndRuleService;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  用户校验
        /// </summary>
        /// <param name="userName">用户账号</param>
        /// <param name="passWord">用户密码</param>
        /// Author  : Napoleon
        /// Created : 2015-01-07 10:04:36
        public ActionResult CheckUser(string userName, string passWord)
        {
            SystemUser user = _userService.CheckUser(userName, passWord.EncrypteRc2(PublicFields.Rc2Key));
            if (user != null)
            {
                if (user.IsUse.Equals(PublicFields.IsDefaultUse))
                {
                    //用户信息
                    user.WriteCookie(PublicFields.UserCookie);
                    //用户权限
                    SystemUserAndRule rule = _userAndRuleService.GetRule(user.Id, PublicFields.ProjectId);
                    rule.RuleId.WriteCookie(PublicFields.RuleIdCookies);
                    return Content("登录成功！");
                }
                return Content("登录失败，该账号已禁用，请联系管理员！");
            }
            return Content("登录失败！");
        }

        /// <summary>
        ///  清除记录
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-12 10:04:29
        public void LoginOut()
        {
            CookieSessionFunc.DeleteCookie(PublicFields.UserCookie);
            CookieSessionFunc.DeleteCookie(PublicFields.RuleIdCookies);
        }

    }
}
