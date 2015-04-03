using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Napoleon.UserModule.BLL;
using Napoleon.UserModule.DAL;
using Napoleon.UserModule.IBLL;
using Napoleon.UserModule.IDAL;

namespace Napoleon.UserModule.Web
{
    public static class AuthConfig
    {

        public static void InitAutofc()
        {
            ContainerBuilder builder = new ContainerBuilder();
            SetupResolveRules(builder);//注册类
            builder.RegisterControllers(Assembly.GetExecutingAssembly());//注册控制器
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
        }

        /// <summary>
        ///  需要用到的类进行注册
        /// </summary>
        /// <param name="builder"></param>
        /// Author  : 俞立钢
        /// Company : 绍兴标点电子技术有限公司
        /// Created : 2014-09-02 16:27:25
        private static void SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterType<UserDao>().As<IUserDao>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<MenuAndRuleDao>().As<IMenuAndRuleDao>();
            builder.RegisterType<MenuAndRuleService>().As<IMenuAndRuleService>();
            builder.RegisterType<CodeDao>().As<ICodeDao>();
            builder.RegisterType<CodeService>().As<ICodeService>();
            builder.RegisterType<UserAndRuleDao>().As<IUserAndRuleDao>();
            builder.RegisterType<UserAndRuleService>().As<IUserAndRuleService>();
            builder.RegisterType<RuleDao>().As<IRuleDao>();
            builder.RegisterType<RuleService>().As<IRuleService>();
            builder.RegisterType<ProjectDao>().As<IProjectDao>();
            builder.RegisterType<ProjectService>().As<IProjectService>();
            builder.RegisterType<MenuDao>().As<IMenuDao>();
            builder.RegisterType<MenuService>().As<IMenuService>();
        }


    }
}