namespace ServiceInterface.Model
{
    /// <summary>
    /// 客服类型
    /// </summary>
    public class HmwCustomerServiceType : IXmlData
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Pkid { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        public string ToXmlString()
        {
            return string.Format("<hmwcustomerservicetype><pkid>{0}</pkid><name>{1}</name></hmwcustomerservicetype>",
                Pkid, Name);
        }
    }
}