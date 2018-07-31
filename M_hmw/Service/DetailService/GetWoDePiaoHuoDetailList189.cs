using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取我的票货明细数据列表
    /// </summary>
    public class GetWoDePiaoHuoDetailList189 : BaseServiceH
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetWoDePiaoHuoDetailList189, this.Params[0]);
        }
    }
}