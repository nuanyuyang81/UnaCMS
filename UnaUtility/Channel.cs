/*
 * Developed by Weiwei Yin (nuanyuyang81@qq.com)
 */

namespace UnaUtility
{
    public class Channel
    {
        private int _id;
        private bool _status = true;
        private string _name = string.Empty;
        private string _title = string.Empty;


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public bool Status
        {
            get { return _status; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
    }
}
