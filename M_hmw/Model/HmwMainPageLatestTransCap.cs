namespace ServiceInterface.Model
{
    /// <summary>
    /// 最新运力
    /// </summary>
    public class HmwMainPageLatestTransCap : IXmlData
    {
        /// <summary>
        /// 记录主键
        /// </summary>
        public string Pkid { get; set; }
        /// <summary>
        /// 车型
        /// </summary>
        public string VehicleType { get; set; }
        /// <summary>
        /// 车号
        /// </summary>
        public string VehicleNo { get; set; }
        /// <summary>
        /// 车重
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 车长
        /// </summary>
        public string Length { get; set; }
        /// <summary>
        /// 起始
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 到止
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Tel { get; set; }

        public string ToXmlString()
        {
            return
                string.Format(
                    "<hmwmainpagelatesttranscap><pkid>{0}</pkid><vehicletype>{1}</vehicletype><vehicleno>{2}</vehicleno><weight>{3}</weight><length>{4}</length><from>{5}</from><to>{6}</to><tel>{7}</tel></hmwmainpagelatesttranscap>",
                    Pkid, VehicleType, VehicleNo, Weight, Length, From, To, Tel
                    );
        }
    }
}