namespace ServiceInterface.Model
{
    /// <summary>
    /// 在港船舶
    /// </summary>
    public class HmwMainPagePortShip : IXmlData
    {
        /// <summary>
        /// 船舶ID
        /// </summary>
        public string ShipId { get; set; }
        /// <summary>
        /// 船代名称
        /// </summary>
        public string AgentName { get; set; }
        /// <summary>
        /// 中文船名
        /// </summary>
        public string ShipNameCn { get; set; }
        /// <summary>
        /// 泊位
        /// </summary>
        public string BerthDesc { get; set; }
        /// <summary>
        /// 作业公司
        /// </summary>
        public string JobCompany { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 余吨
        /// </summary>
        public string RestTon { get; set; }

        public string ToXmlString()
        {
            return
                string.Format(
                    "<hmwmainpageportship><shipid>{0}</shipid><agentname>{1}</agentname><shipnamecn>{2}</shipnamecn><berthdesc>{3}</berthdesc><jobcompany>{4}</jobcompany><state>{5}</state><restton>{6}</restton></hmwmainpageportship>",
                    ShipId, AgentName, ShipNameCn, BerthDesc, JobCompany, State, RestTon);
        }
    }
}