using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取我的引航费用明细数据列表
    /// </summary>
    public class GetWoDeYinHangFeiYongDetailList198 : BaseServiceMC
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetWoDeYinHangFeiYongDetailList198, this.Params[0]);
        }
    }
}