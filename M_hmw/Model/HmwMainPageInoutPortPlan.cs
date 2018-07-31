namespace ServiceInterface.Model
{
    /// <summary>
    /// 进出港计划
    /// </summary>
    public class HmwMainPageInoutPortPlan : IXmlData
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
        /// 属性
        /// </summary>
        public string Nature { get; set; }
        /// <summary>
        /// 引水标识
        /// </summary>
        public string Pilot { get; set; }

        public string ToXmlString()
        {
            return
                string.Format(
                    "<hmwmainpageinoutportplan><shipid>{0}</shipid><agentname>{1}</agentname><shipnamecn>{2}</shipnamecn><berthdesc>{3}</berthdesc><nature>{4}</nature><pilot>{5}</pilot></hmwmainpageinoutportplan>",
                    ShipId, AgentName, ShipNameCn, BerthDesc, Nature, Pilot);
        }
    }
}