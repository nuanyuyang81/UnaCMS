/*
 * Developed by Weiwei Yin (nuanyuyang81@qq.com)
 */
using System;

namespace UnaUtility
{
    public class Navigation
    {
        private int _id;
        private int _parent_id = 0;
        private int _channel_id = 0;
        private string _nav_type = string.Empty;
        private string _name = string.Empty;
        private string _icon_url = string.Empty;
        private string _link_url = string.Empty;
        private string _remark = string.Empty;
        private DateTime _add_time = DateTime.Now;

        /// <summary>
        /// 导航Id
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 父级导航Id
        /// </summary>
        public int ParentId
        {
            get { return _parent_id; }
            set { _parent_id = value; }
        }
        /// <summary>
        /// 频道Id
        /// </summary>
        public int ChannelId
        {
            get { return _channel_id; }
            set { _channel_id = value; }
        }
        /// <summary>
        /// 导航类型
        /// </summary>
        public string NavType
        {
            get { return _nav_type; }
            set { _nav_type = value; }
        }
        /// <summary>
        /// 导航名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 导航图标Url
        /// </summary>
        public string IconUrl
        {
            get { return _icon_url; }
            set { _icon_url = value; }
        }
        /// <summary>
        /// 导航站内短连接
        /// </summary>
        public string LinkUrl
        {
            get { return _link_url; }
            set { _link_url = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime
        {
            get { return _add_time; }
            set { _add_time = value; }
        }
    }
}
