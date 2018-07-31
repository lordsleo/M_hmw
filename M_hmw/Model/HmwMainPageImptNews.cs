namespace ServiceInterface.Model
{
    /// <summary>
    /// 要闻咨询
    /// </summary>
    public class HmwMainPageImptNews :IXmlData
    {
        /// <summary>
        /// 新闻ID
        /// </summary>
        public string NewsId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string PostTime { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 新闻图片URL
        /// </summary>
        public string PictureUrl { get; set; }

        public string ToXmlString()
        {
            return
                string.Format(
                    "<hmwmainpageimptnews><newsid>{0}</newsid><topic>{1}</topic><posttime>{2}</posttime><content><![CDATA[{3}]]></content><pictureurl>{4}</pictureurl></hmwmainpageimptnews>",
                    NewsId, Topic, PostTime, Content, PictureUrl);
        }
    }
}