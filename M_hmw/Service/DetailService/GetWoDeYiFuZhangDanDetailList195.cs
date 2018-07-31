using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取我的已付账单明细数据列表
    /// </summary>
    public class GetWoDeYiFuZhangDanDetailList195 : BaseService
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetWoDeYiFuZhangDanDetailList195, this.Params[0]);
        }
    }
}