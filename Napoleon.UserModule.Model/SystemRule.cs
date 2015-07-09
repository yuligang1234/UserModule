
namespace Napoleon.UserModule.Model
{
    public class SystemRule
    {

        private string _id;
        /// <summary>
        ///  形式: guid(10)+yyMMddhhmmssffffff
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-07 10:19:59
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _projectId;
        /// <summary>
        ///  系统代码
        /// </summary>
        /// Author  : Napoleon
        /// Created : 2015-01-27 15:18:27
        public string ProjectId
        {
            get { return _projectId; }
            set { _projectId = value; }
        }

        private string _name;
        /// <summary>
        ///  部门名称
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-07 10:19:59
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
        /// Created :2015-01-07 10:19:59
        public string ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        private string _person;
        /// <summary>
        ///  部门主管
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-07 10:19:59
        public string Person
        {
            get { return _person; }
            set { _person = value; }
        }

        private string _telPhone;
        /// <summary>
        ///  主管联系电话
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-07 10:19:59
        public string TelPhone
        {
            get { return _telPhone; }
            set { _telPhone = value; }
        }

        private string _address;
        /// <summary>
        ///  地址
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-07 10:19:59
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private decimal _sort;
        /// <summary>
        ///  排序
        /// </summary>
        /// Author  :Napoelon
        /// Created :2015-01-07 10:19:59
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
        /// Created :2015-01-07 10:19:59
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
        /// Created :2015-01-07 10:19:59
        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }





    }
}
