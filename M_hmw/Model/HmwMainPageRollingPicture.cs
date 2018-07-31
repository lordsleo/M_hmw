namespace ServiceInterface.Model
{
    /// <summary>
    /// 首页滚动图片
    /// </summary>
    public class HmwMainPageRollingPicture : IXmlData
    {
        /// <summary>
        /// 图片名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 图片URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 版本代码
        /// </summary>
        public string VersionCode { get; set; }

        public string ToXmlString()
        {
            return string.Format("<hmwmainpagerollingpicture><name>{0}</name><url>{1}</url><versioncode>{2}</versioncode></hmwmainpagerollingpicture>", Name, Url, VersionCode);
        }
    }
}