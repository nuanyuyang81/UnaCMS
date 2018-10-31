/*
 * Developed by Weiwei Yin (nuanyuyang81@qq.com)
 */
using System;

namespace UnaUtility
{
    public class ArticleImages
    {
        private Guid _id;
        private Guid _article_id = Guid.NewGuid();
        private string _tumb_path = string.Empty;
        private string _original_path = string.Empty;
        private DateTime _add_time = DateTime.Now;

        /// <summary>
        /// 图片Id
        /// </summary>
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 文章Id
        /// </summary>
        public Guid ArticleId
        {
            get { return _article_id; }
            set { _article_id = value; }
        }
        /// <summary>
        /// 缩略图地址
        /// </summary>
        public string TumbPath
        {
            get { return _tumb_path; }
            set { _tumb_path = value; }
        }
        /// <summary>
        /// 原图片地址
        /// </summary>
        public string OriginalPath
        {
            get { return _original_path; }
            set { _original_path = value; }
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
