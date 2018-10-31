/*
 * Developed by Weiwei Yin (nuanyuyang81@qq.com)
 */
using System;

namespace UnaUtility
{
    public class ArticleTag
    {
        private int _id;
        private string _title;
        private int _rank = 0;
        private DateTime _add_time = DateTime.Now;

        /// <summary>
        /// 文章标签Id
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 标签标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 权值，0为最大，数越大，权值越小
        /// </summary>
        public int Rank
        {
            get { return _rank; }
            set { _rank = 0; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddTime
        {
            get { return _add_time; }
            set { _add_time = value; }
        }
    }
}
