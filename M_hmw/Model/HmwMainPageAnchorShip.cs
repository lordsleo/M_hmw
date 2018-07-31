namespace ServiceInterface.Model
{
    /// <summary>
    /// 锚地船舶
    /// </summary>
    public class HmwMainPageAnchorShip : IXmlData
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
        /// 吃水
        /// </summary>
        public string Draft { get; set; }
        /// <summary>
        /// 贸别
        /// </summary>
        public string Trade { get; set; }
        /// <summary>
        /// 抵锚时间
        /// </summary>
        public string ArrivalTime { get; set; }
        /// <summary>
        /// 引水标识
        /// </summary>
        public string Pilot { get; set; }

        public string ToXmlString()
        {
            return
                string.Format(
                    "<hmwmainpageanchorship><shipid>{0}</shipid><agentname>{1}</agentname><shipnamecn>{2}</shipnamecn><draft>{3}</draft><trade>{4}</trade><arrivaltime>{5}</arrivaltime><pilot>{6}</pilot></hmwmainpageanchorship>",
                    ShipId, AgentName, ShipNameCn, Draft, Trade, ArrivalTime, Pilot);
        }
    }
}