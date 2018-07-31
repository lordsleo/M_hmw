namespace ServiceInterface.Model
{
    /// <summary>
    /// 帮助图片
    /// </summary>
    public class HmwHelpPicture
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
        public int VersionCode { get; set; }

        public string ToXmlString()
        {
            return string.Format("<hmwhelppicture><name>{0}</name><url>{1}</url><versioncode>{2}</versioncode></hmwhelppicture>", Name, Url, VersionCode);
        }
    }
}