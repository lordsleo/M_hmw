using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取有船作业明细数据列表
    /// </summary>
    public class GetYouChuanZuoYeDetailList186 : BaseServiceH
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetYouChuanZuoYeDetailList186, this.Params[0]);
        }
    }
}