namespace ServiceInterface.Model
{
    /// <summary>
    /// 矿石专区
    /// </summary>
    public class HmwMainPageOreZone : IXmlData
    {
        /// <summary>
        /// 记录主键
        /// </summary>
        public string Pkid { get; set; }
        /// <summary>
        /// 供需
        /// </summary>
        public string SupplyDemand { get; set; }
        /// <summary>
        /// 货名
        /// </summary>
        public string CargoName { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public string DeliveryDate { get; set; }

        public string ToXmlString()
        {
            return
                string.Format(
                    "<hmwmainpageorezone><pkid>{0}</pkid><supplydemand>{1}</supplydemand><cargoname>{2}</cargoname><spec>{3}</spec><weight>{4}</weight><product>{5}</product><deliverydate>{6}</deliverydate></hmwmainpageorezone>",
                    Pkid, SupplyDemand, CargoName, Spec, Weight, Product, DeliveryDate);
        }
    }
}