/*
 * Developed by Weiwei Yin (nuanyuyang81@qq.com)
 */
using System;

namespace UnaUtility
{
    public class ArticleReply
    {
        private Guid _id;
        private Guid _article_id;
        private Guid _parent_id;
        private Guid _user_id;
        private string _user_name = string.Empty;
        private string _content = string.Empty;
        private DateTime _add_time = DateTime.Now;

        /// <summary>
        /// 评论唯一Id
        /// </summary>
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 被评论文章Id
        /// </summary>
        public Guid ArticleId
        {
            get { return _article_id; }
            set { _article_id = value; }
        }
        /// <summary>
        /// 父级评论
        /// </summary>
        public Guid ParentId
        {
            get { return _parent_id; }
            set { _parent_id = value; }
        }
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId
        {
            get { return _user_id; }
            set { _user_id = value; }
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
        /// 评论内容
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime AddTime
        {
            get { return _add_time; }
            set { _add_time = value; }
        }
    }
}
