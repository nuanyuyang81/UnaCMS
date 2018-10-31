/*
 * Developed by Weiwei Yin (nuanyuyang81@qq.com)
 */
using System;

namespace UnaUtility
{
    class Article
    {
        private Guid _id = Guid.NewGuid();
        private int _channel_id = 0;
        private int _category_id = 0;
        private string _call_index = string.Empty;
        private string _title;
        private string _img_url = string.Empty;
        private string _tags = string.Empty;
        private string _summary = string.Empty;
        private string _content = string.Empty;
        private ArticleStatus _status = ArticleStatus.Normal;
        private RecommendStatus _recommend = RecommendStatus.Normal;
        private int _view_count = 0;
        private int _like_count = 0;
        private Guid _user_id;
        private string _user_name;
        private DateTime _add_time = DateTime.Now;
        private DateTime? _update_time;

        /// <summary>
        /// 文章唯一编码
        /// </summary>
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 文章频道Id
        /// </summary>
        public int ChannelId
        {
            get { return _channel_id; }
            set { _channel_id = value; }
        }
        /// <summary>
        /// 文章类别Id
        /// </summary>
        public int CategoryId
        {
            get { return _category_id; }
            set { _category_id = value; }
        }
        /// <summary>
        /// 文章调用别名
        /// </summary>
        public string CallIndex
        {
            get { return _call_index; }
            set { _call_index = value; }
        }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 文章展示图片地址
        /// </summary>
        public string ImageUrl
        {
            get { return _img_url; }
            set { _img_url = value; }
        }
        /// <summary>
        /// 文章标签
        /// </summary>
        public string Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }
        /// <summary>
        /// 文章摘要
        /// </summary>
        public string Summary
        {
            get { return _summary; }
            set { _summary = value; }
        }
        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        /// <summary>
        /// 文章状态
        /// </summary>
        public ArticleStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// 文章推荐状态
        /// </summary>
        public RecommendStatus Recommend
        {
            get { return _recommend; }
            set { _recommend = value; }
        }
        /// <summary>
        /// 文章浏览量
        /// </summary>
        public int ViewCount
        {
            get { return _view_count; }
            set { _view_count = value; }
        }
        /// <summary>
        /// 文章点赞数
        /// </summary>
        public int LikeCount
        {
            get { return _like_count; }
            set { _like_count = value; }
        }
        /// <summary>
        /// 作者唯一Id
        /// </summary>
        public Guid UserId
        {
            get { return _user_id; }
            set { _user_id = value; }
        }
        /// <summary>
        /// 作者名
        /// </summary>
        public string UserName
        {
            get { return _user_name; }
            set { _user_name = value; }
        }
        /// <summary>
        /// 文章添加时间
        /// </summary>
        public DateTime AddTime
        {
            get { return _add_time; }
            set { _add_time = value; }
        }
        /// <summary>
        /// 文章修改时间
        /// </summary>
        public DateTime? UpdateTime
        {
            get { return _update_time; }
            set { _update_time = value; }
        }
    }
}
