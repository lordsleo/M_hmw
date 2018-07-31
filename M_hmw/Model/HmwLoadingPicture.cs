namespace ServiceInterface.Model
{
    /// <summary>
    /// 启动图片
    /// </summary>
    public class HmwLoadingPicture : IXmlData
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
            return string.Format("<hmwloadingpicture><name>{0}</name><url>{1}</url><versioncode>{2}</versioncode></hmwloadingpicture>", Name, Url, VersionCode);
        }
    }
}