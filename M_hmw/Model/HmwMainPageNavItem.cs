namespace ServiceInterface.Model
{
    /// <summary>
    /// 通用频道
    /// </summary>
    public class HmwMainPageNavItem : IXmlData
    {
        /// <summary>
        /// 频道名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 频道页面URL
        /// </summary>
        public string Url { get; set; }

        public string ToXmlString()
        {
            return string.Format("<hmwmainpagenavitem><name>{0}</name><url>{1}</url></hmwmainpagenavitem>", Name, Url);
        }
    }
}