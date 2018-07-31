using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;


namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取货物进港明细数据列表
    /// </summary>
    public class GetHuoWuJingGangDetailList190 : BaseServiceH
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetHuoWuJingGangDetailList190, this.Params[0]);
        }
    }
}