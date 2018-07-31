using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取首页在港船舶明细数据列表
    /// </summary>
    public class GetMainPagePortShipDetailList150 : BaseServiceBS
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetMainPagePortShipDetailList150, this.Params[0]);
        }
    }
}