using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取煤焦供应明细数据列表
    /// </summary>
    public class GetMeiJiaoGongYingDetailList175 : BaseServiceLYG
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetMeiJiaoGongYingDetailList175, this.Params[0]);
        }
    }
}