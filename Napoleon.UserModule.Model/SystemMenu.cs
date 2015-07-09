
namespace Napoleon.UserModule.Model
{
    public class SystemMenu
    {

        private string _projectId;
        /// <summary>
        ///  用来区分菜单属于哪个系统
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-23 15:10:52
        public string ProjectId
        {
            get { return _projectId; }
            set { _projectId = value; }
        }

        private string _id;
        /// <summary>
        ///  形式: guid(10)+yyMMddhhmmssffffff
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-05 05:00:13
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        /// <summary>
        ///  菜单名称
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-05 05:00:13
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _parentId;
        /// <summary>
        ///  父节点
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-05 05:00:13
        public string ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        private string _htmlId;
        /// <summary>
        ///  元素ID
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-07 10:21:51
        public string HtmlId
        {
            get { return _htmlId; }
            set { _htmlId = value; }
        }

        private string _url;
        /// <summary>
        ///  链接
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-05 05:00:13
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        private string _icon;
        /// <summary>
        ///  图标
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-05 05:00:13
        public string Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        private decimal _sort;
        /// <summary>
        ///  排序
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-05 05:00:13
        public decimal Sort
        {
            get { return _sort; }
            set { _sort = value; }
        }

        private string _remark;
        /// <summary>
        ///  备注
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-05 05:00:13
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        private string _operator;
        /// <summary>
        ///  操作者
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-05 05:00:13
        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }



    }
}
