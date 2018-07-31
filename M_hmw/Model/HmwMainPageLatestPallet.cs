namespace ServiceInterface.Model
{
    /// <summary>
    /// 最新货盘
    /// </summary>
    public class HmwMainPageLatestPallet : IXmlData
    {
        /// <summary>
        /// 记录主键
        /// </summary>
        public string Pkid { get; set; }
        /// <summary>
        /// 来港
        /// </summary>
        public string FromPort { get; set; }
        /// <summary>
        /// 去港
        /// </summary>
        public string ToPort { get; set; }
        /// <summary>
        /// F20
        /// </summary>
        public string F20 { get; set; }
        /// <summary>
        /// F40
        /// </summary>
        public string F40 { get; set; }
        /// <summary>
        /// 物流公司
        /// </summary>
        public string LogisticsCompany { get; set; }

        public string ToXmlString()
        {
            return
                string.Format(
                    "<hmwmainpagelatestpallet><pkid>{0}</pkid><fromport>{1}</fromport><toport>{2}</toport><f20>{3}</f20><f40>{4}</f40><logisticscompany>{5}</logisticscompany></hmwmainpagelatestpallet>",
                    Pkid, FromPort, ToPort, F20, F40, LogisticsCompany);
        }
    }
}