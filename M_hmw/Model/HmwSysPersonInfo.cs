namespace ServiceInterface.Model
{
    /// <summary>
    /// 获取个人信息
    /// </summary>
    public class HmwSysPersonInfo : IXmlData
    {
        /// <summary>
        /// 固定电话
        /// </summary>
        public string Phone { set; get; }
        /// <summary>
        /// 移动电话
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        public string ToXmlString()
        {
            return
                string.Format(
                     "<hmwsyspersoninfo><phone>{0}</phone><mobile>{1}</mobile><email>{2}</email></hmwsyspersoninfo>",
                     Phone,
                     Mobile,
                     Email
                );
        }
    }
}