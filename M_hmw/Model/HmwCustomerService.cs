namespace ServiceInterface.Model
{
    /// <summary>
    /// 客服
    /// </summary>
    public class HmwCustomerService : IXmlData
    {
        /// <summary>
        /// 客服类型ID
        /// </summary>
        public int ServiceTypeId { get; set; }
        /// <summary>
        /// 客服类型名称
        /// </summary>
        public string ServiceTypeName { get; set; }
        /// <summary>
        /// 客服名称
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// 客服联系方式（QQ、电话、email等）
        /// </summary>
        public string ServerContact { get; set; }

        public string ToXmlString()
        {
            return string.Format("<hmwcustomerservice><servicetypeid>{0}</servicetypeid><servicetypename>{1}</servicetypename><servername>{2}</servername><servercontact>{3}</servercontact></hmwcustomerservice>",
                ServiceTypeId, ServiceTypeName, ServerName, ServerContact);
        }
    }
}