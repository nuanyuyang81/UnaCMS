/*
 * Developed by Weiwei Yin (nuanyuyang81@qq.com)
 */
namespace UnaUtility
{
    public class UserGroup
    {
        private int _id;
        private string _title = string.Empty;
        private int _grade = 0;
        private int _exp = 0;
        private bool _is_default = false;

        /// <summary>
        /// 用户组Id
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 用户组名称
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 用户组等级
        /// </summary>
        public int Grade
        {
            get { return _grade; }
            set { _grade = value; }
        }
        /// <summary>
        /// 用户组经验值
        /// </summary>
        public int Exp
        {
            get { return _exp; }
            set { _exp = value; }
        }
        /// <summary>
        /// 是否是默认注册组
        /// </summary>
        public bool IsDefault
        {
            get { return _is_default; }
            set { _is_default = value; }
        }
    }
}
