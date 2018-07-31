namespace ServiceInterface.Model
{
    /// <summary>
    /// 常用功能
    /// </summary>
    public class HmwMainPageCommonFunc : IXmlData
    {
        /// <summary>
        /// 功能名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 功能页面URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 版本代码
        /// </summary>
        public string VersionCode { get; set; }

        public string ToXmlString()
        {
            return string.Format("<hmwmainpagecommonfunc><name>{0}</name><url>{1}</url><versioncode>{2}</versioncode></hmwmainpagecommonfunc>", Name, Url, VersionCode);
        }
    }
}