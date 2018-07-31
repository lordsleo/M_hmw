using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using M_hmw.Common;
using M_hmw.Service.BaseUtil;

namespace M_hmw.Service.DetailService
{
    /// <summary>
    /// 获取在船舶明细数据列表
    /// </summary>
    public class GetZaiChuanBoDetailList163 : BaseServiceBS
    {
        protected override string getSql()
        {
            return String.Format(SQL.GetZaiChuanBoDetailList163, this.Params[0]);
        }
    }
}