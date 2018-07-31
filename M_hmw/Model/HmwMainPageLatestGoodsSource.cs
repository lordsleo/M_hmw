namespace ServiceInterface.Model
{
    /// <summary>
    /// 最新货源
    /// </summary>
    public class HmwMainPageLatestGoodsSource : IXmlData
    {
        /// <summary>
        /// 记录主键
        /// </summary>
        public string Pkid { get; set; }
        /// <summary>
        /// 货名
        /// </summary>
        public string CargoName { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 起发地
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 到止地
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// 截止日期
        /// </summary>
        public string EndDate { get; set; }

        public string ToXmlString()
        {
            return
                string.Format(
                    "<hmwmainpagelatestgoodssource><pkid>{0}</pkid><cargoname>{1}</cargoname><weight>{2}</weight><from>{3}</from><to>{4}</to><enddate>{5}</enddate></hmwmainpagelatestgoodssource>",
                    Pkid, CargoName, Weight, From, To, EndDate);
        }
    }
}