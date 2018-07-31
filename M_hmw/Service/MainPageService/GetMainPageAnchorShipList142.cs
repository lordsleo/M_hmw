using M_hmw.Common;
using M_hmw.Service.BaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M_hmw.Service.MainPageService
{
    /// <summary>
    /// 获取首页锚地船舶数据列表
    /// </summary>
    public class GetMainPageAnchorShipList142 : BaseServiceBS
    {
        protected override string getSql()
        {
            //当前时间
            DateTime nowTime = System.DateTime.Now;
            //默认查询5条
            return String.Format(SQL.GetMainPageAnchorShipList142, nowTime.AddDays(-6).ToString(), nowTime.AddDays(-3).ToString(), nowTime.AddDays(-1).ToString(), this.Params.Length > 0 ? this.Params[0] : "5");
        }
    }
}