using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    ///  获取首页锚地船舶明细数据列表
    /// </summary>
    public class GetMainPageAnchorShipDetailList151 : BaseServiceBS
    {
        protected override string getSql()
        {
            //当前时间
            DateTime nowTime = System.DateTime.Now;
            return String.Format(SQL.GetMainPageAnchorShipDetailList151, nowTime.AddDays(-6).ToString(), nowTime.AddDays(-3).ToString(), nowTime.AddDays(-1).ToString(), this.Params[0]);
        }
    }
}