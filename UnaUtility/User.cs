/*
 * Developed by Weiwei Yin (nuanyuyang81@qq.com)
 */
using System;

namespace UnaUtility
{
    public class User
    {
        private Guid _id;
        private int _group_id = 0;
        private string _user_name = string.Empty;
        private string _salt = string.Empty;
        private string _password = string.Empty;
        private string _mobile = string.Empty;
        private string _email = string.Empty;
        private string _avatar = string.Empty;
        private string _nick_name = string.Empty;
        private int _sex = 0;
        private DateTime _add_time = DateTime.Now;
        private string _reg_ip = string.Empty;

        /// <summary>
        /// 用户唯一Id
        /// </summary>
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 用户组Id
        /// </summary>
        public int GroupId
        {
            get { return _group_id; }
            set { _group_id = value; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _user_name; }
            set { _user_name = value; }
        }
        /// <summary>
        /// 加密盐
        /// </summary>
        public string Salt
        {
            get { return _salt; }
            set { _salt = value; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
        /// <summary>
        /// 头地像址
        /// </summary>
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }
        public string NickName
        {
            get { return _nick_name; }
            set { _nick_name = value; }
        }
        public int Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        public DateTime AddTime
        {
            get { return _add_time; }
            set { _add_time = value; }
        }
        public string RegIp
        {
            get { return _reg_ip; }
            set { _reg_ip = value; }
        }
    }
}
