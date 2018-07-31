namespace ServiceInterface.Model
{
    /// <summary>
    /// 最新船期
    /// </summary>
    public class HmwMainPageLatestShipment : IXmlData
    {
        /// <summary>
        /// 记录主键
        /// </summary>
        public string Pkid { get; set; }
        /// <summary>
        /// 起始港
        /// </summary>
        public string FromPort { get; set; }
        /// <summary>
        /// 目的港
        /// </summary>
        public string ToPort { get; set; }
        /// <summary>
        /// 货名
        /// </summary>
        public string CargoName { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 开航日期
        /// </summary>
        public string SailingDate { get; set; }

        public string ToXmlString()
        {
            return
                string.Format(
                    "<hmwmainpagelatestshipment><pkid>{0}</pkid><fromport>{1}</fromport><toport>{2}</toport><cargoname>{3}</cargoname><weight>{4}</weight><sailingdate>{5}</sailingdate></hmwmainpagelatestshipment>",
                    Pkid, FromPort, ToPort, CargoName, Weight, SailingDate);
        }
    }
}